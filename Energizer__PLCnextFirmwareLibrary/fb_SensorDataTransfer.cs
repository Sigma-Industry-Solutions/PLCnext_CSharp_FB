using System;
using System.Iec61131Lib;
using System.Runtime.InteropServices;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace Energizer__PLCnextFirmwareLibrary
{
    // Define a string data type with a maximum string length of 200 characters
    // Size is string length + 4 byte header + 1 byte terminating zero + padding for two byte alignment
    // Mark the declaration with a String attribute and define the length
    [String(506)]
    [StructLayout(LayoutKind.Explicit, Size = 512)]
    public struct TString506
    {
        // Fields
        [FieldOffset(0)]
        public IecStringEx s;  // This member must have the name 's' because the name is evaluated by PLCnext Engineer!

        // Methods
        // Init is needed to set the maximum size and called in the initialization

        public void Init()
        {
            s.maximumLength = 506;
            s.Empty();
        }
        public void ctor()
        {
            Init();
        }
    }

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
        [InOut]
        public TString506 OUT_DiagCode;

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
