namespace Energizer__PLCnextFirmwareLibrary
{
    class SensorSampleValue  

    {

        public float data { get; set; }
        public string timeCollected { get; set; }
        public int id { get; set; }
        public bool transferedAck { get; set; }

        public SensorSampleValue(float data, string timeCollected, int sensorId)
        {
            this.data = data;
            this.timeCollected = timeCollected;
            this.id = sensorId;
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

        public string toString()
        {
            return id + "\n" + timeCollected + "\n" + data;
        }

        public string toJSON()
        {
            return null;
        }

        

    }
}
