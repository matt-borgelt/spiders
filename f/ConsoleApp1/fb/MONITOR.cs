using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spider
{
    public static class MONITOR
    {



        internal static DateTime START_TIME;
        public static List<string> visitedpages = new List<string>();
        public static List<string> listOfUniqueUsers = new List<string>();
        public static List<string> listOfUnvisitedPages = new List<string>();


        public static int pages_visited;
        public static int objects_disposed;

        internal static int column_truncate_size = 50;


        public static void LogDiagnostic(string data)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            MONITOR.Log(data);
            Console.ResetColor();
        }
        public static void LogError(string data)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            MONITOR.Log(data);
            Console.ResetColor();
        }
        public static void Log(string data)
        {

            Console.WriteLine(
                DateTime.Now.ToString().ToUpper().PadRight(20) + "\t" + (data.PadRight(Console.BufferWidth - data.Length)).Replace(Environment.NewLine, "")

                );

        }
        public static void LogMulti(string data)
        {
            string ret = "";
            ret += DateTime.Now.ToString().ToUpper().PadRight(20) + " ";
            var s = data.Split(',').ToArray<string>();
            foreach (var column in s)
            {

                ret += (column.PadRight(20));
            }
            Console.WriteLine(ret);

        }
    }
}
