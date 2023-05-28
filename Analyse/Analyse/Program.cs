using System;
using System.Diagnostics.CodeAnalysis;

namespace Analyse
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			List<Measurment> list = new List<Measurment>();

			Beacon b1 = new Beacon("b1",723,635,92);
			Beacon b2 = new Beacon("b2",1003,150,78);
			Beacon b3 = new Beacon("b3",167,950,173);
			Beacon b4 = new Beacon("b4",484,411,26);
			Beacon b5 = new Beacon("b5",9,253,106);
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
					file.Add(m.Name + ";" + m.Values[i].Distance +";"+ m.Values[i].MedianRSSI + ";" + m.Values[i].AverageRSSI+";"+ m.Values[i].MeanDeviationRSSI + ";" + m.Values[i].MedianStrength + ";" + m.Values[i].AverageStrength+";"+ m.Values[i].MeanDeviationStrength);
				}
				file.Add("");
				file.Add("");
				file.Add("");
			}

			File.WriteAllLines(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Temp.csv",file.ToArray());

			for (int i = 0; i < 5; i++)
			{
				beacons[i].CalculateFunction(list,new List<Measurment>());

				var plt = new ScottPlot.Plot(1000, 800);
				plt.YAxis.AxisLabel.Label = "Distance in cm";
				plt.XAxis.AxisLabel.Label = "RSSI in dBm";

				plt.XAxis.SetBoundary(-100,0);
				plt.YAxis.SetBoundary(0, 1300);
				//plt.AddPoint(0,1300);

				List<double> xs = new List<double>();
				List<double> ys = new List<double>();
				for (double n = 0; n >= -100; n-=1)
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
					plt.AddPoint(m.Values[i].AverageRSSI, m.Values[i].Distance);
				}

				//var plt = new ScottPlot.Plot(400, 300);
				//plt.AddPoint(,);
				//plt.AddScatter(xs.ToArray(), ys.ToArray());
				string s = @"C:\Uni_zeug\ss23\IOT\Measurements\Temp\" + names[i] + "_Deviation_"+beacons[i].Diviation + "plot.png";
				plt.SaveFig(s);
			}

			foreach (Measurment measurment in list)
			{
				measurment.CalculatePresicion(beacons,list);
			}
			Console.WriteLine("LOL!");
		}
	}
}