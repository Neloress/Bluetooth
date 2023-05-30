using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analyse
{
	internal class Measurment
	{
		internal double X { get; private set; }
		internal double Y { get; private set; }
		internal double Z { get; private set; }
		internal string Name { get; private set; }
		internal double ScanTime { get; private set; }
		internal double Presicion { get; private set; }
		internal double Xt { get; private set; }
		internal double Yt { get; private set; }
		internal double Zt { get; private set; }

		internal BeaconMeasurments[] Values { get; private set; }
		internal Measurment(string path, string name, Beacon b1, Beacon b2, Beacon b3, Beacon b4, Beacon b5)
		{
			Name = name;

			string[] lines = File.ReadAllLines(path);
			ScanTime = double.Parse(lines[0].Split(' ')[1]);

			string[] temp1 = lines[1].Split(' ');
			X = double.Parse(temp1[1]);
			Y = double.Parse(temp1[2]);
			Z = double.Parse(temp1[3]);

			Values = new BeaconMeasurments[5];
			Values[0] = new BeaconMeasurments(X, Y, Z, b1);
			Values[1] = new BeaconMeasurments(X, Y, Z, b2);
			Values[2] = new BeaconMeasurments(X, Y, Z, b3);
			Values[3] = new BeaconMeasurments(X, Y, Z, b4);
			Values[4] = new BeaconMeasurments(X, Y, Z, b5);

			for (int i = 4; i < lines.Count() - 1; i++)
			{
				string temp2 = lines[i];
				string[] temp3 = temp2.Split(';');
				Values[0].Values.Add(new Value(temp3[1], temp3[0]));
				Values[1].Values.Add(new Value(temp3[2], temp3[0]));
				Values[2].Values.Add(new Value(temp3[3], temp3[0]));
				Values[3].Values.Add(new Value(temp3[4], temp3[0]));
				Values[4].Values.Add(new Value(temp3[5], temp3[0]));
			}
		}
		internal void CalculatePresicion(List<Beacon> beacons, List<Measurment> measurments, Beacon ignore)
		{
			List<List<Tuple<double, double, double, double>>> listList = new List<List<Tuple<double, double, double, double>>>();
			

			double sum = 0;
			List<double> deviations = new List<double>();

			for (int n = 0; n < 12; n++)
			{
				List<Tuple<double, double, double, double>> loc = new List<Tuple<double, double, double, double>>();
				foreach (Beacon beacon in beacons)
				{
					beacon.CalculateFunction(measurments, new List<Measurment> { this });
					deviations.Add(beacon.Diviation);
					sum += beacon.Diviation;
					for (int i = 0; i < 5; i++)
					{
						if (Values[i].Source == beacon)
						{
							if (beacon != ignore)
							{
								double dis = beacon.GetDistance(Values[i].AverageRSSI);
								loc.Add(new Tuple<double, double, double, double>(beacon.X, beacon.Y, beacon.Z, dis));
							}
						}
					}
				}
				listList.Add(loc);
			}
			

			//for (int i = 0; i < deviations.Count; i++)
			//{
			//	deviations[i] = 1.0 - (deviations[i] / sum);
			//}


			//Tuple<double, double, double> pos = Trilate(loc, 1, -1000, 1200, -1000, 1100, -100, 500);

			//step = 1
			//Tuple<double, double, double> pos = Trilate(loc, 1, -1000, 2100, -1000, 2000, -1000, 1300);//Trilate(loc,100,-1000,1200,-1000,1100,-100,500);

			double minX = -1000;
			double maxX = 2100;
			double minY = -1000;
			double maxY = 2000;
			double minZ = -1000;
			double maxZ = 1300;
			double step = 1;


			//double rangeX = maxX - minX;
			//double xTest = Math.Round(rangeX / 12.0);
			//List<Thread> threads = new List<Thread>();
			//Results = new List<Tuple<double, double, double,double>>();
			//for (int i = 0; i < 12; i++)
			//{
			//	Results.Add(new Tuple<double, double, double,double>(0,0,0,0));
			//	int index = i*2-i;
			//	double maxXt = minX + (double)(index + 1) * xTest + step;
			//	if (i == 1)
			//		maxXt = maxX;
			//	Thread thread = new Thread(() => Trilate(listList[index], step, minX+(double)index * xTest-step, maxXt, minY, maxY, minZ, maxZ,index));
			//	threads.Add(thread);
			//	thread.Start();
			//}


			//foreach (Thread t in threads)
			//{
			//	t.Join();
			//}

			//Presicion = double.MaxValue;
			//double dist = double.MaxValue;
			//foreach (Tuple<double, double, double,double> t in Results)
			//{
			//	if (t.Item4 < dist)
			//	{
			//		dist = t.Item4;
			//		Xt = t.Item1;
			//		Yt = t.Item2;
			//		Zt = t.Item3;

			//		double x = t.Item1 - X;
			//		double y = t.Item2 - Y;
			//		double z = t.Item3 - Z;

			//		Presicion = Math.Sqrt(x * x + y * y + z * z);
			//	}
			//}

			Results = new List<Tuple<double, double, double, double>>();
			Results.Add(new Tuple<double, double, double, double>(0,0,0,0));

			Trilate(listList[0], 10, -1000, 2100, -1000, 2000, -1000, 1300,0);

			Trilate(listList[0],1, Results[0].Item1-100, Results[0].Item1+100, Results[0].Item2-100, Results[0].Item2+100, Results[0].Item3-100, Results[0].Item3+100,0);


			double x = Results[0].Item1 - X;
			double y = Results[0].Item2 - Y;
			double z = Results[0].Item3 - Z;

			Xt = Results[0].Item1;
			Yt = Results[0].Item2;
			Zt = Results[0].Item3;

			Presicion = Math.Sqrt(x * x + y * y + z * z);

			//pos = Trilate(loc, 10, pos.Item1-120, pos.Item1+120, pos.Item2 - 120, pos.Item2 + 120, pos.Item3 - 120, pos.Item3 + 120);
			//pos = Trilate(loc, 1, pos.Item1 - 12, pos.Item1 + 12, pos.Item2 - 12, pos.Item2 + 12, pos.Item3 - 12, pos.Item3 + 12);
			//pos = Trilate(loc, 0.1, pos.Item1 - 1.2, pos.Item1 + 1.2, pos.Item2 - 1.2, pos.Item2 + 1.2, pos.Item3 - 1.2, pos.Item3 + 1.2);

			//double x = pos.Item1 - X;
			//double y = pos.Item2 - Y;
			//double z = pos.Item3 - Z;

			//Xt = pos.Item1;
			//Yt = pos.Item2;
			//Zt = pos.Item3;

			//Presicion = Math.Sqrt(x*x+y*y+z*z);

		}
		private List<Tuple<double, double, double,double>> Results;
		//Tuple<double, double, double>
		private void Trilate(List<Tuple<double, double, double, double>> loc, double step, double minX, double maxX, double minY, double maxY, double minZ, double maxZ,int index)
		{
			double minDis = double.MaxValue;
			double xVal = -1;
			double yVal = -1;
			double zVal = -1;
			//double whole = ((maxX-minX)/step) * ((maxY-minY)/step) * ((maxZ-minZ)/step);
			//double count = 0;
			for (double x = minX; x <= maxX; x += step)
			{
				for (double y = minY; y <= maxY; y += step)
				{
					for (double z = minZ; z <= maxZ; z += step)
					{
						double dis = 0;
						int i = 0;
						foreach (Tuple<double, double, double, double> tuple in loc)
						{
							double xT = tuple.Item1 - x;
							double yT = tuple.Item2 - y;
							double zT = tuple.Item3 - z;
							double tempD = Math.Sqrt(xT * xT + yT * yT + zT * zT);
							dis += Math.Abs(tuple.Item4 - tempD);
							i++;
						}
						if (dis < minDis)
						{
							minDis = dis;
							xVal = x;
							yVal = y;
							zVal = z;
						}

						//if (count%10000000 == 0)
						//{
						//	double prog = count / whole;

						//	//Console.WriteLine(prog);
						//}

						//count++;
					}
				}
			}
			Results[index] = new Tuple<double, double, double,double>(xVal, yVal, zVal,minDis);
			//return new Tuple<double, double, double>(xVal, yVal, zVal);
		}
		internal void CalculateValues()
		{
			foreach (BeaconMeasurments beacon in Values)
			{
				beacon.CalculateValues();
			}
		}
	}
}
