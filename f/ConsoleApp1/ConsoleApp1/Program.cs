using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;

namespace spider
{


    public static class MONITOR
    {
        public static List<string> visitedpages = new List<string>();
        public static List<string> listOfUniqueUsers = new List<string>();

        internal static int column_truncate_size = 50;

        public static void Log(string data)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(DateTime.Now.ToString().ToUpper().PadRight(20) + "\t");
            Console.ResetColor();
            Console.Write(data.PadRight(Console.BufferWidth - data.Length) + Environment.NewLine );
 

        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            Console.SetBufferSize(480, 160);

            string url = @"http://stackoverflow.com/questions/4510212/how-i-can-get-web-pages-content-and-save-it-into-the-string-variable";
            string url2 = @"http://stackoverflow.com/questions/39008135/c-sharp-webbrowser-stuck-on-navigating-when-used-in-for-loop";


            using (N n = new N())
            {
                n.R(url2);
            }



        }
    }
}
//HtmlWeb hw = new HtmlWeb();
//HtmlDocument doc = hw.Load(url);
//List<string> hrefTags = new List<string>();
//foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
//{
//    HtmlAttribute att = link.Attributes["href"];
//    hrefTags.Add(att.Value);
//}

//List<string> userTags = new List<string>();
//List<string> uniqueUsers = new List<string>();
//foreach (var tag in hrefTags)
//{
//    if (Regex.IsMatch(tag, @"users/\d+"))
//    {
//        var user = tag.Split(("/").ToCharArray()).Last();
//        userTags.Add(user);
//        if (!uniqueUsers.Contains(user))
//        {
//            uniqueUsers.Add(user);
//        }
//    }
//}


//List<string> questionTags = new List<string>();
//foreach (var tag in hrefTags)
//{
//    if (Regex.IsMatch(tag, @"questions/\d+"))
//    {
//        questionTags.Add(tag);
//    }
//}