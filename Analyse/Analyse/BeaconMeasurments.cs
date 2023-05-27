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

		internal BeaconMeasurments(double x, double y, double z, Beacon source)
		{ 
			Source = source;
			Distance = Math.Sqrt((x-source.X)* (x - source.X) + (y - source.Y) * (y - source.Y) + (z - source.Z) * (z - source.Z));
		}
		internal void CalculateValues()
		{ 
		
		}
	}
}
