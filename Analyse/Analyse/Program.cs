using ScottPlot;
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
			rows.Add("Receiver;No weigth;Weighted");

			int n = 1;
			foreach (Measurment measurment in list)
			{
				measurment.CalculatePresicion(beacons, list,true);
				double T = measurment.Presicion;
				measurment.CalculatePresicion(beacons, list, false);
				double F = measurment.Presicion;
				rows.Add(n + ";" + F.ToString(CultureInfo.InvariantCulture) + ";" + T.ToString(CultureInfo.InvariantCulture));

				n++;
			}

			File.WriteAllLines(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\presicion.csv", rows.ToArray()) ;
			Console.WriteLine("LOL!");
		}
		internal static void Image0(List<Measurment> list)
		{
			BeaconMeasurments min = null;
			BeaconMeasurments max = null;
			double minV = double.MaxValue;
			double maxV = double.MinValue;

			foreach (Measurment m in list)
			{
				foreach (BeaconMeasurments b in m.Values)
				{
					if (b.MeanDeviationRSSI<minV)
					{ 
						minV = b.MeanDeviationRSSI;
						min = b;
					}
					if (b.MeanDeviationRSSI > maxV)
					{
						maxV = b.MeanDeviationRSSI;
						max = b;
					}
				}
			}
			int width = 800;
			int height = 600;

			var plt = new ScottPlot.Plot(width, height);
			plt.YAxis.AxisLabel.Label = "RSSI";
			plt.XAxis.AxisLabel.Label = "Measurement";

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
			Font font = new Font("Times New Roman", 10);
			Bitmap whole = new Bitmap(width * 2, height * 3);
			List<Bitmap> images = new List<Bitmap>();
			for (int i = 0; i < 5; i++)
			{
				beacons[i].CalculateFunction(list, new List<Measurment>());

				var plt = new ScottPlot.Plot(width, height);
				plt.YAxis.AxisLabel.Label = "Distance in cm";
				plt.XAxis.AxisLabel.Label = "RSSI in dBm";

				plt.XAxis.SetBoundary(-100, 0);
				plt.YAxis.SetBoundary(0, 1300);

				//plt.AddPoint(0,1300);

				List<double> xs = new List<double>();
				List<double> ys = new List<double>();
				for (double n = 0; n >= -100; n -= 1)
				{
					//if (beacons[i].GetDistance(n) <= 1300)
					//{
					xs.Add(n);
					ys.Add(beacons[i].GetDistance(n));
					//}
				}

				plt.AddScatter(xs.ToArray(), ys.ToArray());

				foreach (Measurment m in list)
				{
					//xs.Add(m.Values[i].Distance);
					//ys.Add(m.Values[i].AverageRSSI);
					plt.AddPoint(m.Values[i].AverageRSSI, m.Values[i].Distance,null,5,ScottPlot.MarkerShape.filledCircle,"higfdhdfghdfgh");
				}

				//var plt = new ScottPlot.Plot(400, 300);
				//plt.AddPoint(,);
				//plt.AddScatter(xs.ToArray(), ys.ToArray());
				string s = @"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Remove\" + i + ".png";
				plt.SaveFig(s);
				images.Add(new Bitmap(s));

				deviation.Add(((double)Math.Round(beacons[i].Diviation * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));
				N.Add(((double)Math.Round(beacons[i].N * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));
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
				g.DrawString(s, font, new SolidBrush(Color.Black), new PointF(width + 10, 2 * height + 10));
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
			Font font = new Font("Times New Roman", 10);
			Bitmap whole = new Bitmap(width * 2, height);
			List<Bitmap> images = new List<Bitmap>();
			for (int i = 0; i < 5; i++)
			{
				List<Measurment> ign = new List<Measurment> { list[4], list[5], list[6], list[7], list[9] };
				beacons[i].CalculateFunction(list, ign);

				var plt = new ScottPlot.Plot(width, height);
				plt.YAxis.AxisLabel.Label = "Distance in cm";
				plt.XAxis.AxisLabel.Label = "RSSI in dBm";

				plt.XAxis.SetBoundary(-100, 0);
				plt.YAxis.SetBoundary(0, 1300);

				//plt.AddPoint(0,1300);

				List<double> xs = new List<double>();
				List<double> ys = new List<double>();
				for (double n = 0; n >= -100; n -= 1)
				{
					//if (beacons[i].GetDistance(n) <= 1300)
					//{
					xs.Add(n);
					ys.Add(beacons[i].GetDistance(n));
					//}
				}

				plt.AddScatter(xs.ToArray(), ys.ToArray());

				foreach (Measurment m in list)
				{
					bool found = false;
					foreach(Measurment m2 in ign)
						if(m==m2)
							found = true;

					//xs.Add(m.Values[i].Distance);
					//ys.Add(m.Values[i].AverageRSSI);
					if(!found)
						plt.AddPoint(m.Values[i].AverageRSSI, m.Values[i].Distance, null, 5, ScottPlot.MarkerShape.filledCircle, "higfdhdfghdfgh");
				}

				//var plt = new ScottPlot.Plot(400, 300);
				//plt.AddPoint(,);
				//plt.AddScatter(xs.ToArray(), ys.ToArray());
				string s = @"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Remove\t" + i + ".png";
				plt.SaveFig(s);
				images.Add(new Bitmap(s));

				deviation.Add(((double)Math.Round(beacons[i].Diviation * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));
				N.Add(((double)Math.Round(beacons[i].N * 100.0) / 100.0).ToString(CultureInfo.InvariantCulture));
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