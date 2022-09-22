using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;

namespace Energizer__PLCnextFirmwareLibrary
{
    class Sensor
    {

        public int id { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public String ipaddr { get; set; }
        public String port { get; set; }
        public String gw { get; set; }
        public String subnet { get; set; }
        public String protocol { get; set; }

        private List<SensorSampleValue> ss;

        public Sensor(int id)
        {
            this.id = id;
            ss = new List<SensorSampleValue>(); 
        }

        public void addSample(SensorSampleValue ssv) 
        {
            ss.Add(ssv);
        }

        public string toString()
        {
            return "Sensor ID: " + id + "Sample points: " + ss.Count;
        }

        public String ToJSON()
        {

            StringBuilder sb = new StringBuilder();
            
            string s = "{\n" +
                " \"sensorId\":\"" + id + "\", \n" +
                " \"samples\": [";

            sb.AppendLine(s);

            for(int i = 0; i<ss.Count-1; i++)
            {
                s += "\n" + "" + ss[i].ToJSON() + ",";
                sb.AppendLine("" + ss[i].ToJSON() + ",");
            }

            s += "" + "] \n " +
                "}";

            sb.AppendLine(s);

            return sb.ToString();

            //return s;
        }

     
        public void ToFile()
        {
            try
            {
                // If techcoil.txt exists, seek to the end of the file,
                // else create a new one.
                FileStream fileStream = File.Open("Sensor" + id + ".txt",
                    FileMode.Append, FileAccess.Write);
                // Encapsulate the filestream object in a StreamWriter instance.
                StreamWriter fileWriter = new StreamWriter(fileStream);
                // Write the current date time to the file
                fileWriter.WriteLine(this.ToJSON() + "\n");
                fileWriter.Flush();
                fileWriter.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe);
            }
        }
        

    }
}
