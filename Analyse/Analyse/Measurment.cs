using System;
using System.Collections.Generic;
using System.Linq;
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
		internal void CalculatePresicion(List<Beacon> beacons, List<Measurment> measurments)
		{
			List<Tuple<double, double, double, double>> loc = new List<Tuple<double, double, double, double>>();
			foreach (Beacon beacon in beacons)
			{
				beacon.CalculateFunction(measurments,new List<Measurment> {this });
				for (int i = 0; i < 5; i++)
				{
					if (Values[i].Source == beacon)
					{
						double dis = beacon.GetDistance(Values[i].AverageRSSI);
						loc.Add(new Tuple<double, double, double, double>(beacon.X,beacon.Y,beacon.Z,dis));
					}
				}
			}


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
