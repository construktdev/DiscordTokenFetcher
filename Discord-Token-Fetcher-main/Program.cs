using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Discord_Token_Fetcher
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Password: ");
            string pass = Console.ReadLine();
            tokenRequest(email, pass);
        }

        async static Task tokenRequest(string email, string pass)
        {
            string url = "https://www.discord.com/api/v9/auth/login";

            var payload = new Dictionary<string, string>(){
                {"login", email},
                {"password", pass}
                
                };
            string jsonPayload = JsonConvert.SerializeObject(payload);
            HttpClient client = new HttpClient();
            StringContent stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nConnection Succesfull");
                string res = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(res);
                Console.WriteLine($"UserID: {json["user_id"]}");
                Console.WriteLine($"UserID: {json["token"]}");
                Console.ReadLine();
            } else
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.ReadLine();
            }



        }
    }
}
