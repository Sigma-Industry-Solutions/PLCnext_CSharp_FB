using System;
using System.IO;


namespace Energizer__PLCnextFirmwareLibrary
{
    class SensorSampleValue

    {
        public float data { get; set; }
        public string timeCollected { get; set; }
        public int id { get; set; }
        public bool transferedAck { get; set; }

        public SensorSampleValue(float data, string timeCollected, int id)
        {
            this.data = data;
            this.timeCollected = timeCollected;
            this.id = id;
            transferedAck = false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SensorSampleValue);
        }

        public bool Equals(SensorSampleValue obj)
        {
            return obj != null &&
                    obj.data == this.data &&
                    obj.timeCollected == this.timeCollected &&
                    obj.id == this.id;
        }

        public override string ToString()
        {
            return "ID: " + id + " TIME:  " + timeCollected + " DATA:  " + data;
        }

        public string ToJSON()
        {
            /* 
             *      {
             *      "id":"25", 
             *      "timeSampled":"14:25:12:564", 
             *      "data":"54.22454"
             *      }      
             */
            return "{ " +
                " \"id\":\"" + id + "\", " +
                " \"timeSampled\":\"" + timeCollected +"\", " +
                " \"data\":\"" + data + "\", " +
                " \"transfered\":\" " + transferedAck + "\"" +
                "}";
        }


        public void ToFile()
        {
            try
            {
                // If techcoil.txt exists, seek to the end of the file,
                // else create a new one.
                FileStream fileStream = File.Open(@"data.json", FileMode.Append, FileAccess.Write);
                // Encapsulate the filestream object in a StreamWriter instance.
                StreamWriter fileWriter = new StreamWriter(fileStream);
                
                // Write the current date time to the file
                fileWriter.WriteLine(this.ToJSON() + "\n");
                fileWriter.Flush();
                fileWriter.Close();
            }
            catch (IOException ioe)
            {
                // ConsoleLogger.WriteToLogFile();
            }
        }


        public void ToFile(string text)
        {
            try
            {
                // If techcoil.txt exists, seek to the end of the file,
                // else create a new one.
                FileStream fileStream = File.Open("logger.txt",
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
