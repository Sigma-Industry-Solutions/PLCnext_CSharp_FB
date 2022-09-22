using System;
using System.Collections.Generic;
using System.Text;

namespace Energizer__PLCnextFirmwareLibrary
{

    static class SensorCollection
    {

        private static List<Sensor> sensorList = new List<Sensor>();
        public static void AddSample(SensorSampleValue ssv) 
        {
            int id = ssv.id;
            bool found = false;

            for(int i = 0; i<sensorList.Count; i++)
            {
                if (sensorList[i].id == id)
                {
                    sensorList[i].addSample(ssv);
                    found = true;
                }
            }
            if (!found && sensorList.Count != 0) 
            {
                sensorList[sensorList.Count - 1] = new Sensor(ssv.id);
                sensorList[sensorList.Count - 1].addSample(ssv);
            }else if (!found)
            {
                sensorList[0] = new Sensor(ssv.id);
                sensorList[0] = new Sensor(ssv.id);
            }
        }

        public static string toString()
        {
            string data = null;

            for(int i = 0; i<sensorList.Count; i++)
            {
                data += sensorList[i].toString() + "\n";
            }

            return data;
        }
    }
}
