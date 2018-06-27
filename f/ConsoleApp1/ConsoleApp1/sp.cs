using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace spider
{
    internal class N : IDisposable
    {
        public void Dispose()
        {
            MONITOR.Log("object has been disposed.");
        }

        public void R(string url)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);
            List<string> hrefTags = new List<string>();
            List<string> text = new List<string>();
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
            {
                if(node.InnerText.Length > 10)
                {
                    text.Add(node.InnerText);

                }
            }
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);


            }
            List<string> uniqueUsers = new List<string>();
            List<string> userTags = new List<string>();
            foreach (var tag in hrefTags)
            {
                if (Regex.IsMatch(tag, @"users/\d+"))
                {
                    var user = tag.Split(("/").ToCharArray()).Last();
                    userTags.Add(user);
                    if (!uniqueUsers.Contains(user) && !MONITOR.listOfUniqueUsers.Contains(user))
                    {
                        uniqueUsers.Add(user);
                        MONITOR.listOfUniqueUsers.Add(user);
                       
                        using (StreamWriter sw = File.AppendText(@"D:/cache/cache-so-usernames.txt"))
                        {
                            sw.WriteLine(user);
                            //MONITOR.Log(user);
                        }
                    }
                }
            }


            foreach (var tag in hrefTags)
            {
                if (Regex.IsMatch(tag, @"http.*?questions/\d+"))
                {
                    if (!tag.Contains("promotion"))
                    {
                        if (!MONITOR.visitedpages.Contains(tag))
                        {
                            MONITOR.Log(tag);
                            System.Threading.Thread.Sleep(60000);
                            MONITOR.visitedpages.Add(tag);
                            using (N n = new N())
                            {
                                n.R(tag);


                            }


                        }
                    }
                }
            }


        }
    }
}
