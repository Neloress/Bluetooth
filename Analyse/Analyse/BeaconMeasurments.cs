using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyse
{
	internal class BeaconMeasurments
	{
		internal double Distance { get; private set; } = double.NaN;
		internal Beacon Source { get; private set; }
		internal List<Value> Values { get; private set; } = new List<Value>();
		internal double AverageRSSI { get; private set; }
		internal double MedianRSSI { get; private set; }
		internal double MeanDeviationRSSI { get; private set; }
		internal double AverageStrength { get; private set; }
		internal double MedianStrength { get; private set; }
		internal double MeanDeviationStrength { get; private set; }
		internal int Count { get; private set; }
		internal BeaconMeasurments(double x, double y, double z, Beacon source)
		{ 
			Source = source;
			Distance = Math.Sqrt((x-source.X)* (x - source.X) + (y - source.Y) * (y - source.Y) + (z - source.Z) * (z - source.Z));
		}
		internal void CalculateValues()
		{
			int count = 0;
			double strengthSum = 0;
			double rssiSum = 0;
			double meanDeviationRSSI = 0;
			double meanDeviationStrength = 0;
			List<Value> valuesTemp1 = new List<Value>();
			List<Value> valuesTemp2 = new List<Value>();
			foreach (Value value in Values) 
			{
				valuesTemp1.Add(value);
				valuesTemp2.Add(value);
				if (value.Valid)
				{ 
					count++;
					strengthSum += value.Strength;
					rssiSum += value.RSSI;
				}
			}
			Count = count;
			AverageStrength = strengthSum / (double)count;
			AverageRSSI = rssiSum/(double)count;

			foreach (Value value in Values)
			{
				if (value.Valid)
				{
					meanDeviationStrength += Math.Abs(AverageStrength - value.Strength);
					meanDeviationRSSI += Math.Abs(AverageRSSI-value.RSSI);
				}
			}

			MeanDeviationStrength = meanDeviationStrength / (double)count;
			MeanDeviationRSSI = meanDeviationRSSI / (double)count;

			valuesTemp1.Sort((x, y) => x.RSSI.CompareTo(y.RSSI));
			valuesTemp2.Sort((x, y) => x.Strength.CompareTo(y.Strength));

			MedianRSSI = valuesTemp1[valuesTemp1.Count / 2].RSSI;
			MedianStrength = valuesTemp2[valuesTemp2.Count / 2].Strength;
		}
	}
}
