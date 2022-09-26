using System;
using System.Iec61131Lib;
using System.Threading;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
namespace Energizer__PLCnextFirmwareLibrary
{
    [Structure]
    public struct SensorsData
    {
        public BoolArrayFB Enable;
        public ShortArrayFB Id;
        public StringArrayFB Ip_adress;
        public ShortArrayFB Port;
        public IntArrayFB ReconnectDelay;
        public IntArrayFB Timeout;
    }
    [FunctionBlock]
    public class FileParser : Logger
    {
        [InOut]
        public bool startThread;
        [Output]
        public bool done;
        [Output]
        public int ErrorCode;
        [Output]
        public SensorsData sensors;
        [Output]
        public IecString80 ErrorMessage;
        public Thread StaticCaller;
        private bool m_startThread;
        [Initialization]
        public void __Init()
        {
            // Create the thread object, passing in the
            // serverObject.ThreadBody method using a
            // ThreadStart delegate.
            ThreadStart threadStarter = new ThreadStart(BackgroundServerThread.ThreadBody);
            StaticCaller = new Thread(threadStarter)
            {
                Priority = ThreadPriority.Lowest,
                Name = "backgroundThread"
            };
            ErrorMessage.ctor();
            StaticCaller.Start();
        }
        ~FileParser()
        {
            BackgroundServerThread.TerminateBackgroundThread = true;
        }
        [Execution]
        public void __Process()
        {
            if (BackgroundHelper.IsRisingEdge(startThread, ref m_startThread))
            {
                done = false;
                BackgroundServerThread.doSomething = true;
            }
            done = BackgroundServerThread.done;
            if (BackgroundHelper.IsFallingEdge(startThread, ref m_startThread))
            {
                BackgroundServerThread.doSomething = false;
                done = false;
                startThread = false;
            }
            if (done)
            {
                sensors = BackgroundServerThread.sensors;
            }
            ErrorMessage = BackgroundServerThread.ErrorMessage;
            ErrorCode = BackgroundServerThread.ErrorCode;
        }
        public class BackgroundServerThread
        {
            public static bool doSomething;
            public static bool done;
            public static bool TerminateBackgroundThread;
            public static SensorsData sensors;
            public static int SensorCount = 0;
            public static int ErrorCode = 0;
            public static IecString80 ErrorMessage;
            public static void ThreadBody()
            {
                while (!done)         // run so long as initiator exists.
                {
                    if (TerminateBackgroundThread == true)
                    {
                        break;       // terminates this thread if initiator is removed
                    }
                    if (doSomething)
                    {
                        ErrorMessage.ctor();
                        sensors.Ip_adress.Construct();
                        try
                        {
                            ReadFromFile();
                            // read data from files

                            done = true; // Initiator of this thread looks on this bit to detect the job has been done.
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error reading a file: {0}", ex.ToString());
                            ErrorMessage.s.Init(ex.ToString());
                            ErrorCode = -1;
                        }
                        
                    }
                    else
                    {
                        // Pause for a moment to provide a delay to make
                        // threads more apparent.
                        Thread.Sleep(100);
                    }
                }
            }
            static void ReadFromFile()
            {
                //string a;
                //int lengthReturned;
                //using (FileStream fs = System.IO.File.Open(@"data.config", FileMode.Open))
                //{
                //    //Max byte size of file to red in!
                //    byte[] b = new byte[65535];
                //    //PLCnext is default set to read/write with UTF-8.
                //    System.Text.UTF8Encoding temp = new System.Text.UTF8Encoding(true);
                //    lengthReturned = fs.Read(b, 0, b.Length);
                //    a = temp.GetString(b);
                //    fs.Flush();
                //    fs.Dispose();
                //    fs.Close();
                //}
                string[] arr = File.ReadAllLines(@"data.config");
                //Hack to fix .Split function, otherwise error occurs when parsing the last Timeout value
                //a = a.Substring(0, lengthReturned);
                //arr = a.Split(new string[] { "\n" }, StringSplitOptions.None);
                foreach (string t in arr)
                {
                    ConsoleLogger.log(t);
                    if (SensorCount > 100)
                    {
                        SensorCount = 100;
                    }
                    if (t.StartsWith("ENABLE="))
                    {
                        if (t.Trim().Substring(7) != "")
                        {
                            sensors.Enable[(short)SensorCount] = bool.Parse(t.Trim().Substring(7));
                        }
                    }
                    else if (t.StartsWith("ID="))
                    {
                        if (t.Trim().Substring(3) != "")
                        {
                            sensors.Id[(short)SensorCount] = short.Parse(t.Trim().Substring(3));
                        }
                    }
                    else if (t.StartsWith("IP="))
                    {
                        if (t.Trim().Substring(3) != "")
                        {
                            sensors.Ip_adress.InitStr(SensorCount, t.Trim().Substring(3));
                        }
                    }
                    else if (t.StartsWith("PORT="))
                    {
                        if (t.Trim().Substring(5) != "")
                        {
                            sensors.Port[(short)SensorCount] = short.Parse(t.Trim().Substring(5));
                        }
                    }
                    else if (t.StartsWith("RECONNECT_DELAY="))
                    {
                        if (t.Trim().Substring(16) != "")
                            sensors.ReconnectDelay[SensorCount] = int.Parse(t.Trim().Substring(16));
                        else
                            sensors.ReconnectDelay[SensorCount] = 1;
                    }
                    else if (t.StartsWith("TIMEOUT="))
                    {
                        if (t.Trim().Substring(8) != "")
                            sensors.Timeout[SensorCount] = int.Parse(t.Trim().Substring(8));
                        else
                            sensors.Timeout[SensorCount] = 1;
                        SensorCount++;
                    }
                }
            }
        }
        public static class BackgroundHelper
        {
            public static bool IsRisingEdge(bool value, ref bool _value)
            {
                if (value && !_value)
                {
                    _value = value;
                    return true;
                }
                return false;
            }
            public static bool IsFallingEdge(bool value, ref bool _value)
            {
                if (!value && _value)
                {
                    _value = value;
                    return true;
                }
                return false;
            }
        }
    }
}