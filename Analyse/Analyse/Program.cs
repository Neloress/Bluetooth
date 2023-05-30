using ScottPlot;
using ScottPlot.Drawing;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Analyse
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			List<Measurment> list = new List<Measurment>();

			Beacon b1 = new Beacon("b1", 723, 635, 92);
			Beacon b2 = new Beacon("b2", 1003, 150, 78);
			Beacon b3 = new Beacon("b3", 167, 950, 173);
			Beacon b4 = new Beacon("b4", 484, 411, 26);
			Beacon b5 = new Beacon("b5", 9, 253, 106);
			List<Beacon> beacons = new List<Beacon>();
			beacons.Add(b1);
			beacons.Add(b2);
			beacons.Add(b3);
			beacons.Add(b4);
			beacons.Add(b5);

			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\A_2023.5.24_21-49-10_94__data.csv", "A", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\B_2023.5.24_22-1-31_266__data.csv", "B", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\C_2023.5.24_22-16-36_450__data.csv", "C", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\D_2023.5.24_22-29-45_167__data.csv", "D", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\E_2023.5.24_22-42-36_318__data.csv", "E", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\F_2023.5.24_22-55-55_614__data.csv", "F", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\G_2023.5.24_23-10-38_928__data.csv", "G", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\H_2023.5.24_23-21-58_988__data.csv", "H", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\I_2023.5.24_23-33-24_307__data.csv", "I", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\J_2023.5.24_23-44-35_834__data.csv", "J", b1, b2, b3, b4, b5));

			foreach (Measurment measurment in list)
			{
				measurment.CalculateValues();
			}

			List<string> names = new List<string>();
			names.Add("b1_Nuki");
			names.Add("b2_Printer");
			names.Add("b3_SmartTag");
			names.Add("b4_Petkit");
			names.Add("b5_TV");

			List<string> file = new List<string>();
			for (int i = 0; i < 5; i++)
			{
				file.Add("New Measurment");
				file.Add(names[i]);
				file.Add("Location;Distance;RSSI_Median;RSSI_Average;RSSI_Mean_Deviation;Strength_Median;Strength_Average;Strength_Mean_Deviation");
				foreach (Measurment m in list)
				{
					file.Add(m.Name + ";" + m.Values[i].Distance + ";" + m.Values[i].MedianRSSI + ";" + m.Values[i].AverageRSSI + ";" + m.Values[i].MeanDeviationRSSI + ";" + m.Values[i].MedianStrength + ";" + m.Values[i].AverageStrength + ";" + m.Values[i].MeanDeviationStrength);
				}
				file.Add("");
				file.Add("");
				file.Add("");
			}

			File.WriteAllLines(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Temp.csv", file.ToArray());

			Image0(list);
			Image1(list, beacons);
			Image2(list, beacons);

			//for (int i = 0; i < 5; i++)
			//{
			//	beacons[i].CalculateFunction(list, new List<Measurment> { list[4], list[5], list[6], list[7], list[9] });

			//	var plt = new ScottPlot.Plot(1000, 800);
			//	plt.YAxis.AxisLabel.Label = "Distance in cm";
			//	plt.XAxis.AxisLabel.Label = "RSSI in dBm";

			//	plt.XAxis.SetBoundary(-100, 0);
			//	plt.YAxis.SetBoundary(0, 1300);
			//	//plt.AddPoint(0,1300);

			//	List<double> xs = new List<double>();
			//	List<double> ys = new List<double>();
			//	for (double n = 0; n >= -100; n -= 1)
			//	{
			//		//if (beacons[i].GetDistance(n) <= 1300)
			//		//{
			//		xs.Add(n);
			//		ys.Add(beacons[i].GetDistance(n));
			//		//}
			//	}

			//	plt.AddScatter(xs.ToArray(), ys.ToArray());

			//	foreach (Measurment m in list)
			//	{
			//		//xs.Add(m.Values[i].Distance);
			//		//ys.Add(m.Values[i].AverageRSSI);
			//		plt.AddPoint(m.Values[i].AverageRSSI, m.Values[i].Distance);
			//	}

			//	//var plt = new ScottPlot.Plot(400, 300);
			//	//plt.AddPoint(,);
			//	//plt.AddScatter(xs.ToArray(), ys.ToArray());
			//	string s = @"C:\Uni_zeug\ss23\IOT\Measurements\Temp\optimal_" + names[i] + "_Deviation_" + beacons[i].Diviation + "_N_" + beacons[i].N + "_M_" + beacons[i].MeasuredPower + "_plot.png";
			//	plt.SaveFig(s);

			//}


			beacons[2].CalculateFunction(list, new List<Measurment> { list[0], list[4], list[5], list[6], list[7], list[9] });
			double Atest1 = Math.Abs(list[0].Values[2].Distance - beacons[2].GetDistance(list[0].Values[2].AverageRSSI));

			beacons[4].CalculateFunction(list, new List<Measurment> { list[0], list[4], list[5], list[6], list[7], list[9] });
			double Atest2 = Math.Abs(list[0].Values[4].Distance - beacons[4].GetDistance(list[0].Values[4].AverageRSSI));

			beacons[2].CalculateFunction(list, new List<Measurment> { list[1], list[4], list[5], list[6], list[7], list[9] });
			double Btest1 = Math.Abs(list[1].Values[2].Distance - beacons[2].GetDistance(list[1].Values[2].AverageRSSI));

			beacons[4].CalculateFunction(list, new List<Measurment> { list[1], list[4], list[5], list[6], list[7], list[9] });
			double Btest2 = Math.Abs(list[1].Values[4].Distance - beacons[4].GetDistance(list[1].Values[4].AverageRSSI));

			beacons[2].CalculateFunction(list, new List<Measurment> { list[2], list[4], list[5], list[6], list[7], list[9] });
			double Ctest1 = Math.Abs(list[2].Values[2].Distance - beacons[2].GetDistance(list[2].Values[2].AverageRSSI));

			beacons[4].CalculateFunction(list, new List<Measurment> { list[2], list[4], list[5], list[6], list[7], list[9] });
			double Ctest2 = Math.Abs(list[2].Values[4].Distance - beacons[4].GetDistance(list[2].Values[4].AverageRSSI));

			beacons[2].CalculateFunction(list, new List<Measurment> { list[3], list[4], list[5], list[6], list[7], list[9] });
			double Dtest1 = Math.Abs(list[3].Values[2].Distance - beacons[2].GetDistance(list[3].Values[2].AverageRSSI));

			beacons[4].CalculateFunction(list, new List<Measurment> { list[3], list[4], list[5], list[6], list[7], list[9] });
			double Dtest2 = Math.Abs(list[3].Values[4].Distance - beacons[4].GetDistance(list[3].Values[4].AverageRSSI));

			beacons[2].CalculateFunction(list, new List<Measurment> { list[8], list[4], list[5], list[6], list[7], list[9] });
			double Itest1 = Math.Abs(list[8].Values[2].Distance - beacons[2].GetDistance(list[8].Values[2].AverageRSSI));

			beacons[4].CalculateFunction(list, new List<Measurment> { list[8], list[4], list[5], list[6], list[7], list[9] });
			double Itest2 = Math.Abs(list[8].Values[4].Distance - beacons[4].GetDistance(list[8].Values[4].AverageRSSI));


			List<string> rows = new List<string>();
			rows.Add("Receiver;All;x;y;z;2345;x;y;z;1345;x;y;z;1245;x;y;z;1235;x;y;z;1234;x;y;z");

			//list[9].CalculatePresicion(beacons, list, beacons[0]);

			int n = 1;
			foreach (Measurment measurment in list)
			{
				Console.WriteLine("1");
				Console.WriteLine(n+":10");

				measurment.CalculatePresicion(beacons, list,null);
				string All = Math.Round(measurment.Presicion).ToString();
				All += ";"+Math.Round(measurment.Xt).ToString();
				All += ";" + Math.Round(measurment.Yt).ToString();
				All += ";" + Math.Round(measurment.Zt).ToString();

				Console.WriteLine("2");
				Console.WriteLine(n + ":10");

				measurment.CalculatePresicion(beacons, list, beacons[0]);
				string v2345 = Math.Round(measurment.Presicion).ToString();
				v2345 += ";" + Math.Round(measurment.Xt).ToString();
				v2345 += ";" + Math.Round(measurment.Yt).ToString();
				v2345 += ";" + Math.Round(measurment.Zt).ToString();

				Console.WriteLine("3");
				Console.WriteLine(n + ":10");

				measurment.CalculatePresicion(beacons, list, beacons[1]);
				string v1345 = Math.Round(measurment.Presicion).ToString();
				v1345 += ";" + Math.Round(measurment.Xt).ToString();
				v1345 += ";" + Math.Round(measurment.Yt).ToString();
				v1345 += ";" + Math.Round(measurment.Zt).ToString();

				Console.WriteLine("4");
				Console.WriteLine(n + ":10");

				measurment.CalculatePresicion(beacons, list, beacons[2]);
				string v1245 = Math.Round(measurment.Presicion).ToString();
				v1245 += ";" + Math.Round(measurment.Xt).ToString();
				v1245 += ";" + Math.Round(measurment.Yt).ToString();
				v1245 += ";" + Math.Round(measurment.Zt).ToString();

				Console.WriteLine("5");
				Console.WriteLine(n + ":10");

				measurment.CalculatePresicion(beacons, list, beacons[3]);
				string v1235 = Math.Round(measurment.Presicion).ToString();
				v1235 += ";" + Math.Round(measurment.Xt).ToString();
				v1235 += ";" + Math.Round(measurment.Yt).ToString();
				v1235 += ";" + Math.Round(measurment.Zt).ToString();

				Console.WriteLine("6");
				Console.WriteLine(n + ":10");

				measurment.CalculatePresicion(beacons, list, beacons[4]);
				string v1234 = Math.Round(measurment.Presicion).ToString();
				v1234 += ";" + Math.Round(measurment.Xt).ToString();
				v1234 += ";" + Math.Round(measurment.Yt).ToString();
				v1234 += ";" + Math.Round(measurment.Zt).ToString();

				Console.WriteLine("7");
				Console.WriteLine(n + ":10");

				rows.Add(n + ";" + 
					All + ";" +
					v2345+ ";"+
					v1345 + ";" +
					v1245 + ";" +
					v1235 + ";" +
					v1234);

				n++;
			}

			File.WriteAllLines(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\presicion.csv", rows.ToArray()) ;
			Console.WriteLine("LOL!");
		}
		internal static void Image0(List<Measurment> list)
		{
			List<string> names = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

			BeaconMeasurments min = null;
			BeaconMeasurments max = null;
			double minV = double.MaxValue;
			double maxV = double.MinValue;

			string minBeacon = "";
			string maxBeacon = "";
			string minReceiver = "";
			string maxReceiver = "";

			int i1 = 0;
			foreach (Measurment m in list)
			{
				int i2 = 0;
				foreach (BeaconMeasurments b in m.Values)
				{
					if (b.MeanDeviationRSSI<minV)
					{ 
						minV = b.MeanDeviationRSSI;
						min = b;

						minBeacon = b.Source.Name;
						minReceiver = names[i1];
					}
					if (b.MeanDeviationRSSI > maxV)
					{
						maxV = b.MeanDeviationRSSI;
						max = b;

						maxBeacon = b.Source.Name;
						maxReceiver = names[i1];
					}
					i2++;
				}
				i1++;
			}
			int width = 800;
			int height = 600;

			var plt = new ScottPlot.Plot(width, height);
			plt.YAxis.AxisLabel.Label = "RSSI in dBm";
			plt.XAxis.AxisLabel.Label = "Measurement";

			plt.AddPoint(0, -55, Color.FromArgb(0, 0, 0, 0));

			string s1 = ((double)Math.Round(minV * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture);
			string s2 = ((double)Math.Round(maxV * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture);

			plt.AddAnnotation("Beacon: "+minBeacon+"\nReceiver: "+minReceiver+"\nMean deviation: "+s1,Alignment.UpperLeft);
			plt.AddAnnotation("Beacon: " + maxBeacon + "\nReceiver: " + maxReceiver + "\nMean deviation: " + s2, Alignment.LowerLeft);

			//pltMin.XAxis.SetBoundary(-100, 0);
			//pltMin.YAxis.SetBoundary(0, 1300);

			List<double> xsMin = new List<double>();
			List<double> ysMin = new List<double>();
			int countMin = 0;
			foreach (Value v in min.Values)
			{
				if (v.Valid)
				{
					xsMin.Add(countMin);
					ysMin.Add(v.RSSI);
					countMin++;
				}
			}
			plt.AddScatter(xsMin.ToArray(), ysMin.ToArray());

			//pltMin.SaveFig(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\result0min.png");



			//var pltMax = new ScottPlot.Plot(width, height);
			//pltMax.YAxis.AxisLabel.Label = "RSSI";
			//pltMax.XAxis.AxisLabel.Label = "Measurement";

			//pltMax.XAxis.SetBoundary(-100, 0);
			//pltMax.YAxis.SetBoundary(0, 1300);

			List<double> xsMax = new List<double>();
			List<double> ysMax = new List<double>();
			int countMax = 0;
			foreach (Value v in max.Values)
			{
				if (v.Valid)
				{
					xsMax.Add(countMax);
					ysMax.Add(v.RSSI);
					countMax++;
				}
			}
			plt.AddScatter(xsMax.ToArray(), ysMax.ToArray());

			plt.SaveFig(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\result0.png");

		}
		internal static void Image1(List<Measurment> list, List<Beacon> beacons)
		{
			List<string> names = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
			List<string> deviation = new List<string>();
			List<string> N = new List<string>();

			int width = 400;
			int height = 300;
			//Font font = new Font("Times New Roman", 10);
			Bitmap whole = new Bitmap(width * 2, height * 3);
			List<Bitmap> images = new List<Bitmap>();
			for (int i = 0; i < 5; i++)
			{
				beacons[i].CalculateFunction(list, new List<Measurment>());

				deviation.Add(((double)Math.Round(beacons[i].Diviation * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));
				N.Add(((double)Math.Round(beacons[i].N * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));

				Func<double, double?> func = delegate (double x)
				{
					return Beacon.GetDistanceS(x, beacons[i].N, beacons[i].MeasuredPower);
				};
			
				var plt = new ScottPlot.Plot(width, height);

				plt.YAxis.AxisLabel.Label = "Distance in cm";
				plt.XAxis.AxisLabel.Label = "RSSI in dBm";

				plt.XAxis.SetBoundary(-100, 0);
				plt.YAxis.SetBoundary(0, 1300);

				plt.AddPoint(-100,1300,Color.FromArgb(0,0,0,0));
				plt.AddPoint(0, 0, Color.FromArgb(0, 0, 0, 0));

				string anno = "Beacon: b" + (i + 1) + "\nN: " + N[i] + "\nDeviation: Ø" + deviation[i]+"cm";
				plt.AddAnnotation(anno, Alignment.UpperRight);

				plt.AddFunction(func, Color.FromArgb(31, 118, 180));

				int count = 0;
				foreach (Measurment m in list)
				{
					Color color = plt.GetNextColor();
					plt.AddText(names[count], m.Values[i].AverageRSSI, m.Values[i].Distance,12,color);
					plt.AddPoint(m.Values[i].AverageRSSI, m.Values[i].Distance,color);
					count++;
				}

				string s = @"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Remove\" + i + ".png";
				plt.SaveFig(s);
				images.Add(new Bitmap(s));

				
			}

			using (Graphics g = Graphics.FromImage(whole))
			{
				g.Clear(Color.White);
			}
			for (int y = 0; y < 3; y++)
			{
				for (int x = 0; x < 2; x++)
				{
					int id = y * 2 + x;
					if (id < 5)
					{
						using (Graphics g = Graphics.FromImage(whole))
						{
							g.DrawImage(images[id], new Point(x * width, y * height));
						}
					}
				}
			}
			using (Graphics g = Graphics.FromImage(whole))
			{
				string s = "";
				s += "b1: N=" + N[0] + "\n";
				s += "   Ø Deviation=" + deviation[0] + "cm\n";
				s += "\n";

				s += "b2: N=" + N[1] + "\n";
				s += "   Ø Deviation=" + deviation[1] + "cm\n";
				s += "\n";

				s += "b3: N=" + N[2] + "\n";
				s += "   Ø Deviation=" + deviation[2] + "cm\n";
				s += "\n";

				s += "b4: N=" + N[3] + "\n";
				s += "   Ø Deviation=" + deviation[3] + "cm\n";
				s += "\n";

				s += "b5: N=" + N[4] + "\n";
				s += "   Ø Deviation=" + deviation[4] + "cm\n";
				s += "\n";
				//g.DrawString(s, new System.Drawing.Font("Times New Roman", 10), new SolidBrush(Color.Black), new PointF(width + 10, 2 * height + 10));
			}
			whole.Save(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\result1.png");
		}
		internal static void Image2(List<Measurment> list, List<Beacon> beacons)
		{
			List<string> names = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
			List<string> deviation = new List<string>();
			List<string> N = new List<string>();

			int width = 400;
			int height = 300;
			//Font font = new Font("Times New Roman", 10);
			Bitmap whole = new Bitmap(width * 2, height);
			List<Bitmap> images = new List<Bitmap>();
			for (int i = 0; i < 5; i++)
			{
				List<Measurment> ign = new List<Measurment> { list[4], list[5], list[6], list[7], list[9] };
				beacons[i].CalculateFunction(list, ign);

				deviation.Add(((double)Math.Round(beacons[i].Diviation * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));
				N.Add(((double)Math.Round(beacons[i].N * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));

				Func<double, double?> func = delegate (double x)
				{
					return Beacon.GetDistanceS(x, beacons[i].N, beacons[i].MeasuredPower);
				};

				var plt = new ScottPlot.Plot(width, height);
				plt.YAxis.AxisLabel.Label = "Distance in cm";
				plt.XAxis.AxisLabel.Label = "RSSI in dBm";

				plt.XAxis.SetBoundary(-100, 0);
				plt.YAxis.SetBoundary(0, 1300);

				plt.AddPoint(-100, 1300, Color.FromArgb(0, 0, 0, 0));
				plt.AddPoint(0, 0, Color.FromArgb(0, 0, 0, 0));

				string anno = "Beacon: b" + (i + 1) + "\nN: " +N[i]+ "\nDeviation: Ø"+deviation[i]+"cm";
				plt.AddAnnotation(anno, Alignment.UpperRight);

				plt.AddFunction(func, Color.FromArgb(31, 118, 180));

				int count = 0;
				foreach (Measurment m in list)
				{
					bool found = false;
					foreach(Measurment m2 in ign)
						if(m==m2)
							found = true;

					//xs.Add(m.Values[i].Distance);
					//ys.Add(m.Values[i].AverageRSSI);
					Color color = plt.GetNextColor();
					if (!found)
					{
						
						plt.AddText(names[count], m.Values[i].AverageRSSI, m.Values[i].Distance, 12, color);
						plt.AddPoint(m.Values[i].AverageRSSI, m.Values[i].Distance, color);
						count++;
					}}

				//var plt = new ScottPlot.Plot(400, 300);
				//plt.AddPoint(,);
				//plt.AddScatter(xs.ToArray(), ys.ToArray());
				string s = @"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Remove\t" + i + ".png";
				plt.SaveFig(s);
				images.Add(new Bitmap(s));

				
			}

			using (Graphics g = Graphics.FromImage(whole))
			{
				g.Clear(Color.White);
			}
			using (Graphics g = Graphics.FromImage(whole))
			{
				g.DrawImage(images[2], new Point(0, 0));
				g.DrawImage(images[4], new Point(width, 0));
			}
			whole.Save(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\result2.png");
		}
	}
}