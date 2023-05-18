using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidBluetooth
{
	internal class Beacon
	{
		internal Vector Pos { get; set; }
		internal string Address { get; private set; }
		internal Beacon(Vector pos, string address)
		{ 
			Pos = pos;
			Address = address;
		}
	}
}
