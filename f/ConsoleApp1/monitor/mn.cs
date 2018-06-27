using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitor
{
    class mn
    {

        internal static int column_truncate_size = 20;

        public static void Do(List<dynamic> col)
        {
            foreach (string line in col)
            {
                foreach (string column in line.Split(("\t").ToCharArray()))
                {
                    Console.Write(column.Substring(Math.Max(0, column.Length - column_truncate_size)).PadLeft(column_truncate_size) + " ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        public static void DoCsv(string data)
        {
            foreach (var row in data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                foreach (var column in row.Split(','))
                {
                    Console.Write(column.Substring(Math.Max(0, column.Length - 20)).PadLeft(20));
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
