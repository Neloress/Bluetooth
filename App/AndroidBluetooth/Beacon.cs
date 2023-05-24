using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidBluetooth
{
	internal class Beacon
	{
		internal Vector Pos { get; private set; }
		internal string AddressOrName { get; private set; }
		internal string Name { get; private set; }
		internal Beacon(Vector pos, string address, string name)
		{ 
			Pos = pos;
			AddressOrName = address;
			Name = name;
		}
		internal Beacon(string address, string x, string y, string z, string name) 
		{
			//if (address.Length != 17)
			//	throw new ArgumentException("Mac address is wrong");

			//if(address.Split(':').Length!=6)
			//	throw new ArgumentException("Mac address is wrong");

			AddressOrName = address;

			Pos = new Vector();

			if (double.TryParse(x, out double X))
				Pos.X = X;
			else
				throw new ArgumentException("X is wrong");

			if (double.TryParse(y, out double Y))
				Pos.Y = Y;
			else
				throw new ArgumentException("Y is wrong");

			if (double.TryParse(z, out double Z))
				Pos.Z = Z;
			else
				throw new ArgumentException("Z is wrong");

			Name = name;
		}
		public override string ToString()
		{
			return AddressOrName+"_"+Pos.X.ToString(CultureInfo.InvariantCulture) +"_"+ Pos.Y.ToString(CultureInfo.InvariantCulture) + "_"+ Pos.Z.ToString(CultureInfo.InvariantCulture) + "_"+Name;
		}
	}
}
