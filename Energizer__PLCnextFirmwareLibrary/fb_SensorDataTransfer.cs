using System;
using System.Iec61131Lib;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace Energizer__PLCnextFirmwareLibrary
{
    [FunctionBlock]
    public class fb_SensorDataTransfer
    {
        /* 
         *  
         */

        [Input, DataType("REAL")]
        public float DATA;
        [Input, DataType("DINT")]
        public int SensorID;
        [Output]
        public IecString80 OUT_DiagCode;

        private SensorSampleValue ssv;
        

        [Initialization]
        public void __Init()
        {
            OUT_DiagCode.ctor();
            
        }

        [Execution]
        public void __Process()
        {
            string timeStamp = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond;

            ssv = new SensorSampleValue(DATA, timeStamp, SensorID);
            //SensorCollection.AddSample(ssv);
            if(ssv.id != 0)
            {
                ssv.ToFile();
            }
            
            //SensorCollection.ToFile();

            OUT_DiagCode.s.Init(ssv.ToJSON());
        }
    }
}
