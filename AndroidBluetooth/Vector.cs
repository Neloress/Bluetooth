using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidBluetooth
{
	internal class Vector
	{
		internal double X { get; set; }
		internal double Y { get; set; }
		internal double Z { get; set; }
		internal Vector(double x, double y, double z)
		{ 
			X = x; 
			Y = y; 
			Z = z;
		}
	}
}
