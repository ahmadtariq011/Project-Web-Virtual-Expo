using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VirtualExpo.Bll.Helpers
{
    public static class Utility
    {
        public static string GetJson(string url)
        {
            string result = "";
            using (HttpClient client = new HttpClient())
            {
                // Add an Accept header for JSON format.    
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List all Names.    
                HttpResponseMessage res = client.GetAsync(url).Result;  // Blocking call!    
                if (res.IsSuccessStatusCode)
                {
                    result = res.Content.ReadAsStringAsync().Result;
                    //response = JsonConvert.DeserializeObject<TResponse>(result);
                }
            }

            return result;
        }

        public static List<string> ExtractEmails(string textToScrape)
        {
            Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
            Match match;

            List<string> results = new List<string>();
            for (match = reg.Match(textToScrape); match.Success; match = match.NextMatch())
            {
                if (!(results.Contains(match.Value)))
                    results.Add(match.Value);
            }

            return results;
        }







        public static string GetPublicIPAddress()
        {
            string ipAddress = "";
            try
            {
                using (var client = new WebClient())
                {
                    ipAddress = client.DownloadString("http://ifconfig.me").Replace("\n", "");
                }
            }
            catch
            {
                using (var client = new WebClient())
                {
                    ipAddress = client.DownloadString("http://checkip.dyndns.org/");
                    ipAddress = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                                  .Matches(ipAddress)[0].ToString();
                }
            }

            return ipAddress;
        }
    }
}
