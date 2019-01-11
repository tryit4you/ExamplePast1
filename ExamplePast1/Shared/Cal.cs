using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamplePast1.Shared
{
    public class Cal
    {
        //hàm tính tọa độ của điểm
        public static double TinhKhoangCach(double x1,double y1,double x2,double y2)
        {
            var result = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            return result;
        }
        public static double[] DegToRad(double a,double b)
        {
            double x, y;
            x = a * Math.PI / 180;
            y = b * Math.PI / 180;
            return new double[] { x,y};
        }
        public static double SingleDegToRad(double a)
        {
            return a * Math.PI / 180;
        }
     }
}