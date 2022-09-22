using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;


namespace Energizer__PLCnextFirmwareLibrary
{

    static class SensorCollection
    {

        private static List<Sensor> sl = new List<Sensor>();
        public static void AddSample(SensorSampleValue ssv)
        {

            if (sl.Count == 0)
            {
                Sensor s = new Sensor(ssv.id);
                s.addSample(ssv);
                sl.Add(s);
            }
            else
            {
                bool found = false;
                for(int i = 0; i<sl.Count-1; i++)
                {
                    if(sl[i].id == ssv.id)
                    {
                        sl[i].addSample(ssv);
                        
                        found = true;
                    }
                }
                if (!found)
                {
                    Sensor s = new Sensor(ssv.id);
                    s.addSample(ssv);
                    sl.Add(s);
                }
            }
        }

        public static void ToFile()
        {
            try
            {
                // If techcoil.txt exists, seek to the end of the file,
                // else create a new one.
                FileStream fileStream = File.Open("SensorCollection.txt",
                FileMode.Create, FileAccess.Write);
                // Encapsulate the filestream object in a StreamWriter instance.
                StreamWriter fileWriter = new StreamWriter(fileStream);
                // Write the current date time to the file
                fileWriter.WriteLine(SensorCollection.ToJSON() + "\n");
                fileWriter.Flush();
                fileWriter.Close();
            }
            catch (IOException ioe)
            {
                ConsoleLogger.log(ioe.ToString());
            }
        }

        public static string ToJSON()
        {
            string s = "{\n\"SensorCollection\": [";

            for (int i = 0; i < sl.Count - 1; i++)
            {
                s += "\n" + "" + sl[i].ToJSON() + ",";
            }

            s += "" + " ] \n" +
                " }";

            return s;
        }

        public static string toString()
        {
            string data = null;

            for(int i = 0; i<sl.Count; i++)
            {
                data += sl[i].toString() + "\n";
            }

            return data;
        }
    }
}
