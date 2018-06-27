using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Threading;
using System.Data.SQLite;

namespace spider
{

    //public static class client
    //{
    //    public static string get(string url)
    //    {
    //        WebRequest request = WebRequest.Create(url);
    //        WebResponse response = request.GetResponse();
    //        Stream data = response.GetResponseStream();
    //        string html = String.Empty;
    //        using (StreamReader sr = new StreamReader(data))
    //        {
    //            html = sr.ReadToEnd();
    //        }
    //        return html;
    //    }
    //}

    class Program
    {
        //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;

        static void Main(string[] args)
        {
            Console.SetBufferSize(480, 160);
            string url = @"https://www.quora.com/Can-social-networks-be-scraped-for-data";
            string url2 = @"https://www.instagram.com/angiesuco/";

            MONITOR.START_TIME = DateTime.Now;
            MONITOR.pages_visited = 0;
            MONITOR.objects_disposed = 0;

            //using (spider s = new spider())
            //{
            //    s.spawn(url);
            //}

            //foreach(var child in MONITOR.listOfUnvisitedPages)
            //{
            //    using (spider s = new spider())
            //    {
            //        s.spawn(child);
            //    }
            //}

            var stack = new Stack<List<string>>();
            using (spider n = new spider())
            {
                stack.Push(n.spawn(url));
                while (stack.Any())
                {
                    try
                    {
                        var current = stack.Pop();
                        foreach (var child in current) 
                        {
                            try
                            {
                                using (spider n2 = new spider())
                                {
                                    stack.Push(n2.spawn(child));
                                }
                            }
                            catch (Exception e)
                            {
                                MONITOR.LogError(e.Message);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MONITOR.LogError(e.Message);
                    }
  
                }
            }




            Console.WriteLine("PAGES VISITED: " + MONITOR.pages_visited + ". OBJECTS DISPOSED: " + MONITOR.objects_disposed);

            //using (spider n = new spider())
            //{
            //    Parallel.ForEach( n.spawn(url), page =>{
            //        using (spider n2 = new spider())
            //        {
            //            n2.spawn(page);
            //        }

            //    });
            //}



        }
    }
}
