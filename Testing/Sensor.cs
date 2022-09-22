using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
