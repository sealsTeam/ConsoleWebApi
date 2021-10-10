using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest myRequest = WebRequest.Create("https://localhost:44361/api/Categories");

            
            WebResponse myResponse = myRequest.GetResponse();

            string response;

            using (Stream responseStream = myResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                response = reader.ReadToEnd();
            }
            myResponse.Close();



            var json = JsonConvert.DeserializeObject(response);

            JArray jArray = (JArray)json;


            Console.WriteLine("|\tName\t|\tDescription");
            foreach (var items in jArray)
            {
                Console.WriteLine($"|{items["name"]}\t|{items["desc"]}");
            }
        }
    }
}
