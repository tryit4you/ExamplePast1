using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamplePast1.Shared
{
    public class MapParams
    {
        public static int pdo { get; set; } = 107;
        public static int pphut { get; set; } = 45;
        public static int zone { get; set; } = 6;
    }
    public class Postion
    {
        public static decimal lat { get; set; }
        public static decimal log { get; set; }
    }
}