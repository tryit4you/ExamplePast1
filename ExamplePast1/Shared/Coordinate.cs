using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamplePast1.Shared
{
    public class Coordinate
    {
        public static double[] xyvn2000_2_blwgs84(double x, double y, int pdo, int pphut, int zone)
        {
            double[] numArray2 = convertCoord(x, y, pdo, pphut, zone);
            double num = (Degree_2_Rad(numArray2[0]) * 180.0) / Math.PI;
            double num2 = (Degree_2_Rad(numArray2[1]) * 180.0) / Math.PI;

            return new[] { num, num2 };
        }
        private static double[] BLH_vn2000_2_wgs84(double B, double L, double H)
        {
            double[] numArray = BLH2XYZ(B, L, H);
            double[] numArray2 = VN2000_2_WGS84_second(numArray[0], numArray[1], numArray[2]);
            double[] numArray3 = XYZ2BLH(numArray2[0], numArray2[1], numArray2[2]);
            numArray3[0] = goc2ddmmss((numArray3[0] * 180.0) / Math.PI);
            numArray3[1] = goc2ddmmss((numArray3[1] * 180.0) / Math.PI);
            return numArray3;
        }

        private static double[] BLH2XYZ(double B, double L, double H)
        {
            B = Degree_2_Rad(B);
            L = Degree_2_Rad(L);
            double num = 6378137.0;
            double num2 = 0.00669437999013;
            double num3 = num / Math.Sqrt(1.0 - ((num2 * Math.Sin(B)) * Math.Sin(B)));
            double num4 = ((num3 + H) * Math.Cos(B)) * Math.Cos(L);
            double num5 = ((num3 + H) * Math.Cos(B)) * Math.Sin(L);
            double num6 = ((num3 * (1.0 - num2)) + H) * Math.Sin(B);
            return new double[] { num4, num5, num6 };
        }

        private static double[] convertCoord(double X, double Y, int mdo, int mphut, int zone)
        {
            double[] numArray = new double[2];
            double kTtruc = XLL0_2(mdo.ToString(), mphut.ToString());
            double[] numArray2 = xyvn_to_bl84(X, Y, kTtruc, zone);
            numArray[0] = numArray2[0];
            numArray[1] = numArray2[1];
            return numArray;
        }

        private static double Degree_2_Rad(double B)
        {
            int num2 = (int)(B / 10000.0);
            double num3 = B - (num2 * 0x2710);
            int num4 = (int)(num3 / 100.0);
            double num5 = num3 - (num4 * 100);
            double num6 = num2;
            double num7 = num4;
            double num = (num6 + (num7 / 60.0)) + (num5 / 3600.0);
            return ((num * Math.PI) / 180.0);
        }

        private static double goc2ddmmss(double goc)
        {
            int num = (int)goc;
            double num2 = goc - num;
            double num3 = num2 * 60.0;
            int num4 = (int)num3;
            double num5 = (num3 - num4) * 60.0;
            return (((num * 0x2710) + (num4 * 100)) + num5);
        }

        private static double[] VN2000_2_WGS84_second(double X1, double Y1, double Z1)
        {
            double num = 4.84813681109536E-06;
            double num2 = -191.90441429;
            double num3 = -39.30318279;
            double num4 = -111.45032835;
            double num5 = -0.00928836 * num;
            double num6 = 0.01975479 * num;
            double num7 = -0.00427372 * num;
            double num8 = 1.0000002529062779;
            double num9 = num2 + (num8 * ((X1 + (num7 * Y1)) - (num6 * Z1)));
            double num10 = num3 + (num8 * (((-num7 * X1) + Y1) + (num5 * Z1)));
            double num11 = num4 + (num8 * (((num6 * X1) - (num5 * Y1)) + Z1));
            return new double[] { num9, num10, num11 };
        }

        private static double XLL0_2(string pdo, string pph)
        {
            int num = short.Parse(pdo);
            int num2 = short.Parse(pph);
            int num3 = (num * 0x2710) + (num2 * 100);
            return (double)num3;
        }

        private static double[] XY2BL(double X, double Y, double KTtruc, double zone)
        {
            KTtruc = Degree_2_Rad(KTtruc);
            double num = 1.0;
            if (zone == 3.0)
            {
                num = 0.9999;
            }
            else
            {
                num = 0.9996;
            }
            X /= num;
            Y = (Y - 500000.0) / num;
            double num2 = 6378137.0;
            double num8 = 0.00669437999013;
            double d = 0.00673949674228;
            double num3 = num2 * (1.0 - num8);
            double num4 = ((3.0 * num8) * num3) / 2.0;
            double num5 = ((5.0 * num8) * num4) / 4.0;
            double num6 = ((7.0 * num8) * num5) / 6.0;
            double num7 = ((9.0 * num8) * num6) / 8.0;
            double num10 = (((num3 + (num4 / 2.0)) + ((3.0 * num5) / 8.0)) + ((5.0 * num6) / 16.0)) + ((35.0 * num7) / 128.0);
            double num11 = (((num4 / 2.0) + (num5 / 2.0)) + ((15.0 * num6) / 32.0)) + ((7.0 * num7) / 16.0);
            double num12 = ((num5 / 8.0) + ((3.0 * num6) / 16.0)) + ((7.0 * num7) / 32.0);
            double num13 = (num6 / 32.0) + (num7 / 16.0);
            double num14 = num7 / 128.0;
            double num15 = X / num10;
            double num16 = num15 + ((((((num11 * Math.Sin(2.0 * num15)) / 2.0) - ((num12 * Math.Sin(4.0 * num15)) / 4.0)) + ((num13 * Math.Sin(6.0 * num15)) / 6.0)) - ((num14 * Math.Sin(8.0 * num15)) / 8.0)) / num10);
            double num17 = num15 + ((((((num11 * Math.Sin(2.0 * num16)) / 2.0) - ((num12 * Math.Sin(4.0 * num16)) / 4.0)) + ((num13 * Math.Sin(6.0 * num16)) / 6.0)) - ((num14 * Math.Sin(8.0 * num16)) / 8.0)) / num10);
            while (Math.Abs((double)(num17 - num16)) > Math.Pow(10.0, -14.0))
            {
                num16 = num17;
                num17 = num15 + ((((((num11 * Math.Sin(2.0 * num16)) / 2.0) - ((num12 * Math.Sin(4.0 * num16)) / 4.0)) + ((num13 * Math.Sin(6.0 * num16)) / 6.0)) - ((num14 * Math.Sin(8.0 * num16)) / 8.0)) / num10);
            }
            double a = num17;
            double num28 = Math.Sin(a);
            double num20 = num2 / Math.Sqrt(1.0 - ((num8 * num28) * num28));
            double num38 = num20 * num20;
            double num39 = num38 * num38;
            double num40 = num39 * num38;
            double num19 = Math.Sqrt(d) * Math.Cos(a);
            double num22 = num19 * num19;
            double num23 = num22 * num22;
            double x = Math.Tan(a);
            double num24 = x * x;
            double num25 = Math.Pow(x, 4.0);
            double num26 = Math.Pow(x, 6.0);
            double num27 = Math.Cos(a);
            Math.Pow(num27, 3.0);
            Math.Pow(num27, 5.0);
            Math.Pow(num27, 7.0);
            double num37 = 1.0 + num22;
            double num29 = (-num37 * x) / (2.0 * num38);
            double num30 = (-num29 * ((((5.0 + (3.0 * num24)) + num22) - ((9.0 * num22) * num24)) - (4.0 * num23))) / (12.0 * num38);
            double num31 = (num29 * (((((61.0 + (90.0 * num24)) + (45.0 * num25)) + (46.0 * num22)) - ((252.0 * num22) * num24)) - ((90.0 * num22) * num25))) / (360.0 * num39);
            double num32 = (-num29 * ((1385.0 + ((3633.0 * num24) * a)) + ((4095.0 * num26) * a))) / (20160.0 * num40);
            double num33 = 1.0 / (num20 * num27);
            double num34 = (-num33 * ((1.0 + (2.0 * num24)) + num22)) / (6.0 * num38);
            double num35 = (-num33 * (((5.0 + (28.0 * num24)) + (6.0 * num22)) + ((8.0 * num22) * num24))) / (120.0 * num39);
            double num36 = (-num33 * (((61.0 + (662.0 * num24)) + (1320.0 * num25)) + (720.0 * num26))) / (5040.0 * num40);
            double num41 = (((a + (num29 * Math.Pow(Y, 2.0))) + (num30 * Math.Pow(Y, 4.0))) + (num31 * Math.Pow(Y, 6.0))) + (num32 * Math.Pow(Y, 8.0));
            double num43 = (((num33 * Y) + (num34 * Math.Pow(Y, 3.0))) + (num35 * Math.Pow(Y, 5.0))) + (num36 * Math.Pow(Y, 7.0));
            double num42 = KTtruc + num43;
            return new double[] { num41, num42 };
        }

        private static double[] xyvn_to_bl84(double X, double Y, double KTtruc, double zone)
        {
            double[] numArray = XY2BL(X, Y, KTtruc, zone);
            numArray[0] = goc2ddmmss((numArray[0] * 180.0) / Math.PI);
            numArray[1] = goc2ddmmss((numArray[1] * 180.0) / Math.PI);
            return BLH_vn2000_2_wgs84(numArray[0], numArray[1], 0.0);
        }

        private static double[] XYZ2BLH(double X, double Y, double Z)
        {
            double num = 6378137.0;
            double num4 = 6356752.3142;
            double num2 = 0.00669437999013;
            double num3 = 0.00673949674228;
            double num7 = Math.Atan(Y / X);
            if (num7 < 0.0)
            {
                num7 += Math.PI;
            }
            double num12 = Math.Sqrt((X * X) + (Y * Y));
            double a = Math.Atan((Z * num) / (num12 * num4));
            double num10 = Z + ((num3 * num4) * Math.Pow(Math.Sin(a), 3.0));
            double num11 = num12 - ((num2 * num) * Math.Pow(Math.Cos(a), 3.0));
            double num6 = Math.Atan(num10 / num11);
            double num5 = num / Math.Sqrt(1.0 - ((num2 * Math.Sin(num6)) * Math.Sin(num6)));
            double num8 = (num12 - (num5 * Math.Cos(num6))) / Math.Cos(num6);
            return new double[] { num6, num7, num8 };
        }
    }
}