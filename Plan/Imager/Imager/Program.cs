using System.Windows.Forms;
using System;
using ScottPlot.Drawing.Colormaps;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;
using ScottPlot;

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




			Bitmap bitmap = new Bitmap(@"C:\Uni_zeug\ss23\IOT\repo\BluetoothLocalisation\Plan\Source.jpg");
			//int pixX0 = 211;
			//int pixY0 = 715;
			//int pixYE = 47;

			int w = 800;
			int h = 819;
			var plt = new ScottPlot.Plot(w, h);

			plt.XAxis.AxisLabel.Label = "X in cm";
			plt.YAxis.AxisLabel.Label = "Y in cm";

			plt.XAxis.SetBoundary(-100, 1100);
			plt.YAxis.SetBoundary(-100, 1100);


			plt.AddPoint(-100, -100, Color.FromArgb(0, 0, 0, 0));
			plt.AddPoint(1100, 1100, Color.FromArgb(0, 0, 0, 0));


			List<Tuple<double, double, double, string>> beacons = new List<Tuple<double, double, double, string>>();
			List<Tuple<double, double, double, string>> points = new List<Tuple<double, double, double, string>>();
			List<Tuple<double, double, double, string>> pointsT = new List<Tuple<double, double, double, string>>();

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

			pointsT.Add(new Tuple<double, double, double, string>(492, 591, 116, "At"));
			pointsT.Add(new Tuple<double, double, double, string>(189, 401, 91, "Bt"));
			pointsT.Add(new Tuple<double, double, double, string>(154, 501, 172, "Ct"));
			pointsT.Add(new Tuple<double, double, double, string>(33, 818, 199, "Dt"));
			pointsT.Add(new Tuple<double, double, double, string>(525, 507, 87, "Et"));
			pointsT.Add(new Tuple<double, double, double, string>(889, 395, 64, "Ft"));
			pointsT.Add(new Tuple<double, double, double, string>(767, 276, -187, "Gt"));
			pointsT.Add(new Tuple<double, double, double, string>(820, 111, 51, "Ht"));
			pointsT.Add(new Tuple<double, double, double, string>(247, 650, 108, "It"));
			pointsT.Add(new Tuple<double, double, double, string>(802, 681, -259, "Jt"));




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



			string info2 = "";
			info2 += "b1:\n";//Nuk
			info2 += "   x=(723 | -4 → +4)\n";
			info2 += "   y=(635 | -4 → +4)\n";
			info2 += "   z=(92 | -4 → +4)\n";
			info2 += "\n";

			info2 += "b2:\n";//Drucker
			info2 += "   x=(1003 | -17 → +17)\n";
			info2 += "   y=(150 | -4 → +32)\n";
			info2 += "   z=(78 | -16 → +2)\n";
			info2 += "\n";

			info2 += "b3:\n";//SmartTag
			info2 += "   x=(167 | -2 → +2)\n";
			info2 += "   y=(950 | -1 → +1)\n";
			info2 += "   z=(173 | -2 → +2)\n";
			info2 += "\n";

			info2 += "b4:\n";//Petkit
			info2 += "   x=(484 | -45 → +2)\n";
			info2 += "   y=(411 | -43 → +12)\n";
			info2 += "   z=(26 | -25 → +30)\n";
			info2 += "\n";

			info2 += "b5:\n";//TV
			info2 += "   x=(9, -0 → +12)\n";
			info2 += "   y=(253, -60 → +60)\n";
			info2 += "   z=(106, -0 → +70)\n";
			info2 += "\n";



			double xMax = plt.XAxis.Dims.Max;
			double xMin = plt.XAxis.Dims.Min;


			plt.GetBitmap();
			float xPix0 = plt.GetPixelX(0);
			float xPix;
			int pixX0 = 211;
			int pixXE = 729;
			int pixY0 = bitmap.Height - 752 - 1;
			int pixYE = bitmap.Height - 46 - 1;

			double pixelsizeIx = (double)(pixXE - pixX0) / 730.36;
			double pixelsizeIy = (double)(pixYE - pixY0) / 990.2144;

			double pYmin = plt.YAxis.Dims.Min;
			double pYmax = plt.YAxis.Dims.Max;

			double pSizeRy = pYmax - pYmin;
			double pSizePy = plt.GetPixelY(pYmin) - plt.GetPixelY(pYmax);
			double pixelsizePy = pSizePy / pSizeRy;

			double pXmin = plt.XAxis.Dims.Min;
			double pXmax = plt.XAxis.Dims.Max;

			double pSizeRx = pXmax - pXmin;
			double pSizePx = plt.GetPixelX(pXmax) - plt.GetPixelX(pXmin);
			double pixelsizePx = pSizePx / pSizeRx;

			double scaleX = pixelsizePx / pixelsizeIx;
			double scaleY = pixelsizePy / pixelsizeIy;

			//double llX = -((double)pixX0 * pixelsizeIy);// * pixelsizeIy) ;
			//double llY = -((double)pixY0 * scaleY);// * pixelsizeIy);

			//float mWidth = 730.36f;
			//float mHeight = 990.21f;

			int newWidth = (int)Math.Round((double)bitmap.Width * scaleX);
			int newHeight = (int)Math.Round((double)bitmap.Height * scaleY);

			double dif = scaleX / scaleY;

			Bitmap streched = new Bitmap(newWidth, newHeight);
			using (Graphics g = Graphics.FromImage(streched))
			{
				g.DrawImage(bitmap, 0, 0, streched.Width, streched.Height);
			}

			for (int x = 0; x < streched.Width; x++)
			{
				for (int y = 0; y < streched.Height; y++)
				{
					float damp = 0.5f;
					Color t = streched.GetPixel(x, y);
					int val = (int)Math.Round(damp * (255.0f - (float)t.R));
					val = 255 - val;
					Color t2 = Color.FromArgb(val, val, val);
					streched.SetPixel(x, y, t2);

				}
			}


			//streched.Save(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Plan.png");

			//float offsetX = (float)plt.GetCoordinateX((float)llX);// (float)llX;// (float)plt.GetCoordinateX((float)llX);
			//float offsetY = (float)plt.GetCoordinateY(0); // (float)plt.GetCoordinateY((float)llY);

			plt.AddImage(streched, -295, -95, 0, 1, Alignment.LowerLeft);

			bool drawTri = false;

			//plt.AddAnnotation(info, Alignment.MiddleRight);
			plt.AddAnnotation(info2, Alignment.UpperRight);

			float fontSize = 20;

			Color[] colors = plt.Palette.GetColors(16);
			colors[7] = colors[15];

			int n = 0;
			for (int i = 0; i < 10; i++)
			{
				Tuple<double, double, double, string> point = points[i];
				Tuple<double, double, double, string> pointT = pointsT[i];

				Color color = colors[n];
				plt.AddPoint(point.Item1, point.Item2, color, 7);
				if (i < 9)
					plt.AddText(point.Item4, point.Item1, point.Item2, fontSize, color);
				else
					plt.AddText(""+point.Item4, point.Item1-10, point.Item2, fontSize, color);
				
				if (drawTri)
				{
					//plt.AddLine(point.Item1, point.Item2, pointT.Item1, pointT.Item2, color);
					plt.AddArrow(pointT.Item1, pointT.Item2, point.Item1, point.Item2, 2, color);

					//plt.AddPoint(pointT.Item1, pointT.Item2, color);
					//plt.AddText(pointT.Item4, pointT.Item1, pointT.Item2, fontSize, color);
				}

				n++;
			}


			foreach (Tuple<double, double, double, string> beacon in beacons)
			{
				Color color = colors[n];
				plt.AddPoint(beacon.Item1, beacon.Item2, color, 7);
				plt.AddText(beacon.Item4, beacon.Item1, beacon.Item2, fontSize, color);
				n++;
			}



			plt.XAxis.ManualTickSpacing(100);
			plt.YAxis.ManualTickSpacing(100);


			plt.Grid(false);

			plt.SaveFig(@"C:\Uni_zeug\ss23\IOT\Measurements\Temp\Plan.png");
			;
			//double pixelsize = (double)(pixY0 - pixYE) / 990.2144;

			//int size = 5;
			//float textSize = 20;
			//int textOffsetX = 4;
			//int textOffsetY = -10;

			//using (Graphics g = Graphics.FromImage(bitmap))
			//{
			//	foreach (Tuple<double, double, double, string> tuple in points)
			//	{
			//		Tuple<int, int> pixel = Convert(pixelsize, pixX0, pixY0, tuple.Item1, tuple.Item2);
			//		g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(pixel.Item1 - size, pixel.Item2 - size, size * 2, size * 2));
			//		g.DrawString(tuple.Item4,new Font("Times New Roman", textSize),new SolidBrush(Color.Red),new PointF(pixel.Item1+textOffsetX,pixel.Item2+textOffsetY));
			//	}
			//	foreach (Tuple<double, double, double, string> tuple in beacons)
			//	{
			//		Tuple<int, int> pixel = Convert(pixelsize, pixX0, pixY0, tuple.Item1, tuple.Item2);
			//		g.FillEllipse(new SolidBrush(Color.Green), new Rectangle(pixel.Item1 - size, pixel.Item2 - size, size * 2, size * 2));
			//		g.DrawString(tuple.Item4, new Font("Times New Roman", textSize), new SolidBrush(Color.Green), new PointF(pixel.Item1 + textOffsetX, pixel.Item2 + textOffsetY));
			//	}
			//	string info = "A: (234/704/96)\n";
			//	info += "B: (343/367/130)\n";
			//	info += "C: (37/186/96)\n";
			//	info += "D: (88/921/179)\n";
			//	info += "E: (517/587/54)\n";
			//	info += "F: (712/450/94)\n";
			//	info += "G: (569/76/81)\n";
			//	info += "H: (1042/247/231)\n";
			//	info += "I: (107/575/78)\n";
			//	info += "J: (660/968/123)\n";
			//	g.DrawString(info, new Font("Times New Roman", textSize), new SolidBrush(Color.Red), new PointF(740,10));

			//	string info2 = "";
			//	info2 += "b1\n";//Nuk
			//	info2 += "x: 723, +4 ,-4\n";
			//	info2 += "y: 635, +4 ,-4\n";
			//	info2 += "z: 92, +4 ,-4\n";
			//	info2 += "\n";

			//	info2 += "b2\n";//Drucker
			//	info2 += "x: 1003, +17 ,-17\n";
			//	info2 += "y: 150, +32 ,-4\n";
			//	info2 += "z: 78, +2 ,-16\n";
			//	info2 += "\n";

			//	info2 += "b3\n";//SmartTag
			//	info2 += "x: 167, +2 ,-2\n";
			//	info2 += "y: 950, +1 ,-1\n";
			//	info2 += "z: 173, +2 ,-2\n";
			//	info2 += "\n";

			//	info2 += "b4\n";//Petkit
			//	info2 += "x: 484, +2 ,-45\n";
			//	info2 += "y: 411, +12 ,-43\n";
			//	info2 += "z: 26, +30 ,-25\n";
			//	info2 += "\n";

			//	info2 += "b5\n";//TV
			//	info2 += "x: 9, +12 ,-0\n";
			//	info2 += "y: 253, +60 ,-60\n";
			//	info2 += "z: 106, +70 ,-0\n";
			//	info2 += "\n";

			//	g.DrawString(info2, new Font("Times New Roman", textSize), new SolidBrush(Color.Green), new PointF(10, 10));
			//}

			//beacons.Add(new Tuple<double, double, double, string>(723, 635, 92, "b1"));//Nuki
			//beacons.Add(new Tuple<double, double, double, string>(1003, 150, 78, "b2"));//Drucker
			//beacons.Add(new Tuple<double, double, double, string>(167, 950, 173, "b3"));//SmartTag
			//beacons.Add(new Tuple<double, double, double, string>(484, 411, 26, "b4"));//Petkit
			//beacons.Add(new Tuple<double, double, double, string>(9, 253, 106, "b5"));//TV

			//bitmap.Save(@"C:\Uni\SS23\IOT\BluetoothLocalisation\Plan\Changed.jpg");
		}
		private static Tuple<int, int> Convert(double pixelsize, int x0, int y0, double x, double y)
		{
			double xR = (double)x0 + x * pixelsize;
			double yR = (double)y0 - y * pixelsize;
			return new Tuple<int, int>((int)Math.Round(xR), (int)Math.Round(yR));
		}
	}
}