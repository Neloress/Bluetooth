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
		internal List<Value>[] Values { get; private set; }
		internal Measurment(string path, string name, Beacon b1, Beacon b2, Beacon b3, Beacon b4, Beacon b5)
		{
			Name = name;

			string[] lines = File.ReadAllLines(path);
			ScanTime = double.Parse(lines[0].Split(' ')[1]);

			string[] temp1 = lines[1].Split(' ');
			X = double.Parse(temp1[1]);
			Y = double.Parse(temp1[2]);
			Z = double.Parse(temp1[3]);

			Values = new List<Value>[5];
			for (int i = 0; i < 5; i++)
				Values[i] = new List<Value>();

			for (int i = 4; i < lines.Count(); i++)
			{
				string temp2 = lines[i];
				string[] temp3 = temp2.Split(';');
				Values[0].Add(new Value(temp3[1], temp3[0], b1));
				Values[1].Add(new Value(temp3[2], temp3[0], b2));
				Values[2].Add(new Value(temp3[3], temp3[0], b3));
				Values[3].Add(new Value(temp3[4], temp3[0], b4));
				Values[4].Add(new Value(temp3[5], temp3[0], b5));
			}

		}
	}
}
