using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using Serilog;

namespace PLCNextAutoConfigMQTTClient
{
    public static class ConsoleLogger
    {

        public static void log(string a)
        {
            string s = DateTime.Now.ToString("MM-dd-yyyy");
            if (!File.Exists("data" + s + ".log"))
            {
                StreamWriter sw = File.CreateText("data" + s + ".log");
                sw.WriteLine("[" + DateTime.Now.ToString() + "]" + " -> " + a);
            }
            else
            {
                File.Delete("data" + s + ".log");

                File.AppendAllText("data" + s + ".log", "[" + DateTime.Now.ToString() + "]" + " -> " + a);
            }

            Console.WriteLine("[" + DateTime.Now.ToString() + "]" + " -> " + a);
        }


        public static void WriteToLogFile(string[] arr)
        {
            string a = "";

            foreach (string b in arr)
            {
                a += b + "\n";
            }

            File.WriteAllText(@"data.log", a);

        }

    }

    class Program
    {
        private static List<string> ReadFile(string path)
        {
            List<string> data = new List<string>();

            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                byte[] b = new byte[64];

                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    data.Add(temp.GetString(b));
                }
            }

            return data;
        }

        private static Dictionary<string, string> ReadJson(string Json)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            using (FileStream fs = File.Open(Json, FileMode.Open))
            {
                byte[] b = new byte[64];

                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    string t = temp.GetString(b);
                    Console.WriteLine(t);
                }
            }

            return ret;
        }

        static void DownloadFile()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(delegate (object sender, DownloadProgressChangedEventArgs e)
                {
                    ConsoleLogger.log("Downloaded:" + e.ProgressPercentage.ToString());
                    Console.WriteLine("Downloaded:" + e.ProgressPercentage.ToString());
                });

                webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler
                    (delegate (object sender, System.ComponentModel.AsyncCompletedEventArgs e)
                    {
                        if (e.Error == null && !e.Cancelled)
                        {
                            Console.WriteLine("Download completed!");
                        }
                    });
                webClient.DownloadFileAsync(new Uri("http://www.example.com/file/test.jpg"), "test.jpg");
            }
        }



        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("log.log").CreateLogger();

            string jsonString = @"
                            {
                                ""Sensors"":
                                {
                                    ""items"": 
                                    [
                                        {
                                            ""Name"":""Sensor1"",
                                            ""Ip_address"":""192.168.0.100"",
                                            ""Port"":""28""
                                        },
                                        
                                        {
                                            ""Name"":""Sensor2"",
                                            ""Ip_address"":""192.168.3.100"",
                                            ""Port"":""28"" 
                                        }
                                    ]
                                }
 
                            }";



            MQTTClient.Connect_Client(jsonString, "data/modbus");

           
            

            //List<string> data = ReadFile(jsonString);
            
            //foreach(var a in data)
            //{
            //    Console.WriteLine(a);
            //}
        }

        
    }
}
