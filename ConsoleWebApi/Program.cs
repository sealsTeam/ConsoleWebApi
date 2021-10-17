using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace ConsoleWebApi
{
    class Program
    {
        static void Main(string[] args)
        {


            while (true)
            {
                Console.WriteLine("Получить Или Добавить get/add");

                string answer = Console.ReadLine();

                if (answer == "get")
                {
                    Get();
                }
                else if (answer == "add")
                {
                    Console.WriteLine("Введите имя категории");
                    string name = Console.ReadLine();

                    Console.WriteLine("Введите описании категории");
                    string desc = Console.ReadLine();

                    Post(name, desc);

                }
            }
            
        }

        public static void Get ()
        {
            WebRequest req = WebRequest.Create("https://localhost:44361/api/categories");
            WebResponse myResponse = req.GetResponse();

            string response;

            using (Stream responseStream = myResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                response = reader.ReadToEnd();
            }
            myResponse.Close();



            var json = JsonConvert.DeserializeObject(response);

            JArray jArray = (JArray)json;


            Console.WriteLine("ID\t|\tName\t|\tDescription");
            foreach (var items in jArray)
            {
                Console.WriteLine($"|{items["id"]} \t |{items["name"]}\t|{items["desc"]}");
            }
        }

        public static void Post (string name, string desc)
        {
            JObject json = new JObject();

            json["CategoryName"] = name;
            json["Description"] = desc;


            WebRequest req = WebRequest.Create("https://localhost:44361/api/categories");
            req.Method = "POST";

            req.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {

                streamWriter.Write(json.ToString());
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }

        }
    }
}
