using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyse
{
	internal class Value
	{
		internal bool Valid { get; private set; }
		internal int RSSI { get; private set; } = int.MinValue;
		internal double Strength { get; private set; } = double.NaN;
		internal DateTime Time { get; private set; }
		
		internal Value(string rssi,string time)
		{
			if (rssi == "noVal")
				Valid = false;
			else
			{ 
				Valid = true;
				RSSI = int.Parse(rssi);
			}
				

			int year = int.Parse(time.Split('.')[0]);
			int month = int.Parse(time.Split('.')[1]);
			int day = int.Parse(time.Split('.')[2].Split('_')[0]);

			int hour = int.Parse(time.Split('_')[1].Split('-')[0]);
			int minute = int.Parse(time.Split('-')[1]);
			int second = int.Parse(time.Split('-')[2].Split('_')[0]);

			int milisecond = int.Parse(time.Split('_')[2]);

			Time = new DateTime(year,month,day,hour,minute,second,milisecond);
			Strength  = TODO;
		}
	}
}
