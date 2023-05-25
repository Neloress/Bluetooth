using System.Windows.Forms;
using System;

namespace Imager
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			//ApplicationConfiguration.Initialize();
			//Application.Run(new Form1());

			Bitmap bitmap = new Bitmap(@"C:\Uni\SS23\IOT\BluetoothLocalisation\Plan\SourceStreched.png");
			int pixX0 = 211;
			int pixY0 = 715;
			int pixYE = 47;

			List<Tuple<double, double, double, string>> beacons = new List<Tuple<double, double, double, string>>();
			List<Tuple<double, double, double, string>> points = new List<Tuple<double, double, double, string>>();

			beacons.Add(new Tuple<double, double, double, string>(723, 635, 92, "b1"));//Nuki
			beacons.Add(new Tuple<double, double, double, string>(1003, 150, 78, "b2"));//Drucker
			beacons.Add(new Tuple<double, double, double, string>(167, 950, 173, "b3"));//SmartTag
			beacons.Add(new Tuple<double, double, double, string>(484, 411, 26, "b4"));//Petkit
			beacons.Add(new Tuple<double, double, double, string>(9, 253, 106, "b5"));//TV

			points.Add(new Tuple<double, double, double, string>(234, 704, 96, "A"));
			points.Add(new Tuple<double, double, double, string>(343, 367, 130, "B"));
			points.Add(new Tuple<double, double, double, string>(37, 186, 96, "C"));
			points.Add(new Tuple<double, double, double, string>(88, 921, 176, "D"));
			points.Add(new Tuple<double, double, double, string>(517, 587, 54, "E"));
			points.Add(new Tuple<double, double, double, string>(712, 450, 94, "F"));
			points.Add(new Tuple<double, double, double, string>(569, 76, 81, "G"));
			points.Add(new Tuple<double, double, double, string>(1042, 247, 231, "H"));
			points.Add(new Tuple<double, double, double, string>(107, 575, 78, "I"));
			points.Add(new Tuple<double, double, double, string>(660, 968, 123, "J"));

			double pixelsize = (double)(pixY0 - pixYE) / 990.2144;

			int size = 5;
			float textSize = 20;
			int textOffsetX = 4;
			int textOffsetY = -10;

			using (Graphics g = Graphics.FromImage(bitmap))
			{
				foreach (Tuple<double, double, double, string> tuple in points)
				{
					Tuple<int, int> pixel = Convert(pixelsize, pixX0, pixY0, tuple.Item1, tuple.Item2);
					g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(pixel.Item1 - size, pixel.Item2 - size, size * 2, size * 2));
					g.DrawString(tuple.Item4,new Font("Times New Roman", textSize),new SolidBrush(Color.Red),new PointF(pixel.Item1+textOffsetX,pixel.Item2+textOffsetY));
				}
				foreach (Tuple<double, double, double, string> tuple in beacons)
				{
					Tuple<int, int> pixel = Convert(pixelsize, pixX0, pixY0, tuple.Item1, tuple.Item2);
					g.FillEllipse(new SolidBrush(Color.Green), new Rectangle(pixel.Item1 - size, pixel.Item2 - size, size * 2, size * 2));
					g.DrawString(tuple.Item4, new Font("Times New Roman", textSize), new SolidBrush(Color.Green), new PointF(pixel.Item1 + textOffsetX, pixel.Item2 + textOffsetY));
				}
				string info = "A: (234/704/96)\n";
				info += "B: (343/367/130)\n";
				info += "C: (37/186/96)\n";
				info += "D: (88/921/179)\n";
				info += "E: (517/587/54)\n";
				info += "F: (712/450/94)\n";
				info += "G: (569/76/81)\n";
				info += "H: (1042/247/231)\n";
				info += "I: (107/575/78)\n";
				info += "J: (660/968/123)\n";
				g.DrawString(info, new Font("Times New Roman", textSize), new SolidBrush(Color.Red), new PointF(740,10));

				string info2 = "";
				info2 += "b1\n";//Nuk
				info2 += "x: 723, +4 ,-4\n";
				info2 += "y: 635, +4 ,-4\n";
				info2 += "z: 92, +4 ,-4\n";
				info2 += "\n";

				info2 += "b2\n";//Drucker
				info2 += "x: 1003, +17 ,-17\n";
				info2 += "y: 150, +32 ,-4\n";
				info2 += "z: 78, +2 ,-16\n";
				info2 += "\n";

				info2 += "b3\n";//SmartTag
				info2 += "x: 167, +2 ,-2\n";
				info2 += "y: 950, +1 ,-1\n";
				info2 += "z: 173, +2 ,-2\n";
				info2 += "\n";

				info2 += "b4\n";//Petkit
				info2 += "x: 484, +2 ,-45\n";
				info2 += "y: 411, +12 ,-43\n";
				info2 += "z: 26, +30 ,-25\n";
				info2 += "\n";

				info2 += "b5\n";//TV
				info2 += "x: 9, +12 ,-0\n";
				info2 += "y: 253, +60 ,-60\n";
				info2 += "z: 106, +70 ,-0\n";
				info2 += "\n";

				g.DrawString(info2, new Font("Times New Roman", textSize), new SolidBrush(Color.Green), new PointF(10, 10));
			}

			beacons.Add(new Tuple<double, double, double, string>(723, 635, 92, "b1"));//Nuki
			beacons.Add(new Tuple<double, double, double, string>(1003, 150, 78, "b2"));//Drucker
			beacons.Add(new Tuple<double, double, double, string>(167, 950, 173, "b3"));//SmartTag
			beacons.Add(new Tuple<double, double, double, string>(484, 411, 26, "b4"));//Petkit
			beacons.Add(new Tuple<double, double, double, string>(9, 253, 106, "b5"));//TV

			bitmap.Save(@"C:\Uni\SS23\IOT\BluetoothLocalisation\Plan\Changed.jpg");
		}
		private static Tuple<int, int> Convert(double pixelsize, int x0, int y0, double x, double y)
		{
			double xR = (double)x0 + x * pixelsize;
			double yR = (double)y0 - y * pixelsize;
			return new Tuple<int, int>((int)Math.Round(xR), (int)Math.Round(yR));
		}
	}
}