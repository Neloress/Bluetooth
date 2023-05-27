using System;
using System.Collections.Generic;
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
		internal Beacon(string name, double x, double y, double z) 
		{ 
			Name = name;
			X = x;
			Y = y;
			Z = z;
		}
	}
}
