﻿using System;

namespace Analyse
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			List<Measurment> list = new List<Measurment>();

			Beacon b1 = new Beacon("b1",723,635,92);
			Beacon b2 = new Beacon("b2",1003,150,78);
			Beacon b3 = new Beacon("b3",167,950,173);
			Beacon b4 = new Beacon("b4",484,411,26);
			Beacon b5 = new Beacon("b5",9,253,106);

			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\A_2023.5.24_21-49-10_94__data.csv", "A", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\B_2023.5.24_22-1-31_266__data.csv", "B", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\C_2023.5.24_22-16-36_450__data.csv", "C", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\D_2023.5.24_22-29-45_167__data.csv", "D", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\E_2023.5.24_22-42-36_318__data.csv", "E", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\F_2023.5.24_22-55-55_614__data.csv", "F", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\G_2023.5.24_23-10-38_928__data.csv", "G", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\H_2023.5.24_23-21-58_988__data.csv", "H", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\I_2023.5.24_23-33-24_307__data.csv", "I", b1, b2, b3, b4, b5));
			list.Add(new Measurment(@"C:\Uni_zeug\ss23\IOT\Measurements\J_2023.5.24_23-44-35_834__data.csv", "J", b1, b2, b3, b4, b5));
		}
	}
}