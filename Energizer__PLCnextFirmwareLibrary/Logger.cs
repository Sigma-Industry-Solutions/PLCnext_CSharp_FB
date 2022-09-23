using System;

using System.IO;


//Initialize IecString with "logger.ctor();"
//Use "log()" to log to a output for a FunctionBlock

namespace Energizer__PLCnextFirmwareLibrary
{

    public class Logger
    {


        public class FileLogger
        {
            public string logger;

            public void log(string a)
            {
                logger = a;
            }
            public void log(int a)
            {
                logger = (a.ToString());
            }
            public void log(bool a)
            {
                logger = (a.ToString());
            }
            public void log(char a)
            {
                logger = (a.ToString());
            }
            public void log(double a)
            {
                logger = (a.ToString());
            }
        }

        public static class ConsoleLogger
        {

            public static void log(string a)
            {
                string s = DateTime.Now.ToString("MM-dd-yyyy");
                if (!File.Exists("data" + s + ".log"))
                {
                    StreamWriter sw = File.CreateText("data" + s + ".log");
                    sw.AutoFlush = true;
                    sw.WriteLine("[" + DateTime.Now.ToString() + "]" + " -> " + a);
                }
                else
                {
                    File.Delete("data" + s + ".log");
                    StreamWriter sw = new StreamWriter("data" + s + ".log");
                    sw.AutoFlush = true;
                    sw.Flush();
                    sw.Dispose();
                    sw.Close();
                    File.Delete("data" + s + ".log");
                    sw = File.CreateText("data" + s + ".log");
                    sw.WriteLine("[" + DateTime.Now.ToString() + "]" + " -> " + a);
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
    }
}
