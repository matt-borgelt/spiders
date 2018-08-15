using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;

namespace spider_pastebin
{
    public static class spider
    {

        public static void GetPastes(List<dynamic> pastes)
        {


            foreach (var paste in pastes)
            {
                using (HttpClient client = new HttpClient())
                {
                    var key = paste.key;
                    client.BaseAddress = new Uri("https://scrape.pastebin.com/api_scrape_item.php?i=" + key);


                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    try
                    {
                        HttpResponseMessage response = client.GetAsync(string.Empty).Result;
                        string ret = "";
                        if (response.IsSuccessStatusCode)
                        {
                            ret = response.Content.ReadAsStringAsync().Result;
                            File.WriteAllText("./" + key + ".json", ret);
                            ;
                        }
                        else
                        {
                            //message = string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        }
                        //var y = JsonConvert.DeserializeObject<List<dynamic>>(ret);


                    }
                    catch (Exception e)
                    {

                    }

                }
            }
        }

        public static void Start()
        {
            List<dynamic> x = new List<dynamic>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://scrape.pastebin.com/api_scraping.php?limit=250");


                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "REST@aldridgegroup.com", "Qu7TgIDi9D37"))));

                try
                {
                    HttpResponseMessage response = client.GetAsync(string.Empty).Result;
                    string ret = "";
                    if (response.IsSuccessStatusCode)
                    {
                        ret = response.Content.ReadAsStringAsync().Result;
                        ;
                    }
                    else
                    {
                        //message = string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }
                    x = JsonConvert.DeserializeObject<List<dynamic>>(ret);
                    File.WriteAllText("./out.json", JsonConvert.SerializeObject(x));




                    ;
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
            GetPastes(x);
        }

    }
}
