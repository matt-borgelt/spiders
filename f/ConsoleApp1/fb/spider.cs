using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace spider
{
    internal class spider : IDisposable
    {
        public void Dispose()
        {

            MONITOR.objects_disposed++;

            MONITOR.LogDiagnostic("object has been disposed." + MONITOR.objects_disposed);
        }


        public List<string> spawn(string url)
        //public void spawn(string url)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            List<string> hrefTags = new List<string>();
            List<string> text = new List<string>();
            List<string> links_from_this_page = new List<string>();
            
            Random rnd = new Random();
            int wait_time = 5000 + rnd.Next(1000);
            //10000 + rnd.Next(1000);

            //MONITOR.Log( "Elapsed: " + (DateTime.Now.Subtract(MONITOR.START_TIME).TotalMinutes.ToString() +"  wt@"+wait_time + "mS " + url));
            MONITOR.LogMulti("Elapsed: " + (DateTime.Now.Subtract(MONITOR.START_TIME).TotalMinutes.ToString() + ",  wt@" + wait_time + "mS, " + url));

            try
            {
                System.Threading.Thread.Sleep(wait_time);
                doc = hw.Load(url);
            }
            catch (Exception e)
            {
                MONITOR.LogError(e.Message);
            }

            try
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
                {
                    text.Add(node.InnerText);
                }
            }
            catch (Exception e)
            {
                MONITOR.LogError(e.Message);
            }
            try
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    HtmlAttribute att = link.Attributes["href"];
                    hrefTags.Add(att.Value);
                }
            }
            catch (Exception e)
            {
                MONITOR.LogError(e.Message);
            }

            doc = null;
            GC.Collect();

            foreach (var tag in hrefTags)
            {
                if (Regex.IsMatch(tag, @"profile/"))
                {
                    var user = tag.Split(("/").ToCharArray()).Last();
                    if (!MONITOR.listOfUniqueUsers.Contains(user))
                    {
                        using (SQLiteConnection qdbconnection = new SQLiteConnection("Data Source=D:/cache/quora.sqlite;Version=3"))
                        {
                            qdbconnection.Open();
                            SQLiteCommand command = new SQLiteCommand(@"INSERT INTO quora (name, link) values ('" + user + "', '" + url + "')", qdbconnection);
                            command.ExecuteNonQuery();
                        }
                        MONITOR.listOfUniqueUsers.Add(user);
                    }
                }
            }

            //using (StreamWriter sw2 = File.AppendText(@"D:/cache/cache-quora-text.txt"))
            //{
            //    sw2.WriteLine(String.Join(String.Empty, text.ToArray()));
            //}
            //using (StreamWriter sw = new StreamWriter(@"D:/cache/cache-quora-usernames.txt"))
            //{
            //    sw.WriteLine(String.Join(Environment.NewLine, MONITOR.listOfUniqueUsers.ToArray()));
            //}
            //using (StreamWriter sw3 = new StreamWriter(@"D:/cache/cache-quora-visited-sites.txt"))
            //{
            //    sw3.WriteLine(String.Join(Environment.NewLine, MONITOR.visitedpages.ToArray()));
            //}


            Parallel.ForEach(hrefTags, hrefTag =>
            {
                if (!hrefTag.Contains("topic") && !hrefTag.Contains("profile") && !hrefTag.Contains("answer") && !hrefTag.Contains("careers/"))
                {
                    //if (!MONITOR.visitedpages.Contains(hrefTag))
                    //{
                        if (hrefTag[0].Equals('/') && hrefTag.Contains("-"))
                        {
                            
                            try
                            {
                                links_from_this_page.Add(@"https://www.quora.com" + hrefTag.Trim(Environment.NewLine.ToCharArray()));
                                MONITOR.listOfUnvisitedPages.Add(@"https://www.quora.com" + hrefTag.Trim(Environment.NewLine.ToCharArray()));
                            }
                            catch (Exception e)
                            {
                                MONITOR.LogError(e.Message + e.StackTrace);
                            }

                            //using (spider n = new spider())
                            //{
                            //    n.spawn(@"https://www.quora.com" + hrefTag);
                            //    Dispose();
                            //}

                        }
                    //}
                }
            });
            //MONITOR.visitedpages.Add(url);
            return links_from_this_page;
        }
    }
}
