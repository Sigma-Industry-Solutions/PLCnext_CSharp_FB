using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace OPC_UA_ServerGateway
{

public static class ConsoleLogger
{

    public static void log(string a) // test
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