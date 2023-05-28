using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyse
{
	internal class Beacon
	{
		internal string Name { get; private set; }
		internal double X { get; private set; }
		internal double Y { get; private set; }
		internal double Z { get; private set; }
		internal double N { get; private set; }
		internal double MeasuredPower { get; private set; }
		internal double Diviation { get; private set; }
		internal Beacon(string name, double x, double y, double z) 
		{ 
			Name = name;
			X = x;
			Y = y;
			Z = z;
		}
		internal double GetDistance(double rssi)
		{ 
			return Math.Pow(10, (MeasuredPower - rssi) / (10.0 * N));
		}
		internal void CalculateFunction(List<Measurment> measurments,List<Measurment> ignore)
		{
			double minMeasuredPower = 0;//-100;
			double maxMeasuredPower = 0;
			double stepMeasuredPower = 0.1;
			double bestMeasuredPower = -1;

			double minN = 0;
			double maxN = 10;
			double stepN = 0.01;
			double bestN = -1;

			double minDeviation = double.MaxValue;

			for (double n = minN; n <= maxN; n += stepN)
			{
				for (double measuredPower = minMeasuredPower; measuredPower <= maxMeasuredPower; measuredPower += stepMeasuredPower)
				{
					double count = 0;
					double deviation = 0;
					foreach (Measurment measurment in measurments)
					{
						bool ign = false;
						foreach (Measurment c in ignore)
							if (c == measurment)
							{
								//deviation = double.MaxValue;
								ign = true;
							}
								

						for (int i = 0; i < 5 && !ign; i++)
						{
							if (this == measurment.Values[i].Source)
							{
								double distance1 = Math.Pow(10, (measuredPower - measurment.Values[i].AverageRSSI) / (10.0 * n));
								double distance2 = measurment.Values[i].Distance;
								deviation += Math.Abs(distance1-distance2);
								count++;
							}
						}
					}
					if (deviation < minDeviation)
					{
						minDeviation = deviation;
						bestN = n;
						bestMeasuredPower = measuredPower;
						Diviation = minDeviation / count;
					}
				}
			}

			N = bestN;
			MeasuredPower = bestMeasuredPower;
		}
	}
}
