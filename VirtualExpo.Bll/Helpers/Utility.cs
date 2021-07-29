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






        public static async Task DeliverEmail(string to, string from, string senderName, string subject, string body)
        {
            try
            {
                from = "yourfriends@lillylifestyle.com";

                //var emailRequest = new SendPulseEmailRequest();
                //emailRequest.email.from.email = from;
                //emailRequest.email.from.name = senderName;

                //emailRequest.email.to.Add(new ToSendPulse
                //{
                //    email = to
                //});
                //emailRequest.email.subject = subject;
                //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(body);
                //emailRequest.email.html = Convert.ToBase64String(plainTextBytes);

                //HttpClient httpClient = new HttpClient();
                //var sendPulseTokenRequest = new SendPulseTokenRequest();
                //var response = await httpClient.PostAsJsonAsync("https://api.sendpulse.com/oauth/access_token", sendPulseTokenRequest);
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    var sendPulseTokenResponse = await response.Content.ReadFromJsonAsync<SendPulseTokenResponse>();
                //    httpClient = new HttpClient();
                //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sendPulseTokenResponse.access_token);
                //    var emailResponse = await httpClient.PostAsJsonAsync("https://api.sendpulse.com/smtp/emails", emailRequest);
                //}
            }
            catch (Exception ex)
            {

            }
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
