using System;


namespace Energizer__PLCnextFirmwareLibrary
{
    
    public class fb_SensorDataTransfer
    {
        /* 
         *  
         */

        
        public float DATA;
   
        public int SensorID;
      
        public String OUT_DiagCode;

        private SensorSampleValue ssv;
        

        public fb_SensorDataTransfer(float DATA, int SensorID)
        {
            this.DATA = DATA;
            this.SensorID = SensorID;

        }


        public void __Init()
        {
            
            
        }

    
        public void __Process()
        {
            string timeStamp = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond;

            ssv = new SensorSampleValue(DATA, timeStamp, SensorID);
            SensorCollection.AddSample(ssv);
            //ensorCollection.ToFile();
            
            OUT_DiagCode = (ssv.toString());
        }
    }
}
