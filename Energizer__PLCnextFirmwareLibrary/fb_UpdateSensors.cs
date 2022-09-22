using System;
using System.Iec61131Lib;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace Energizer__PLCnextFirmwareLibrary
{
    [FunctionBlock]
    public class fb_UpdateSensors
    {

        [Input, DataType("BOOL")]
        public bool simulate;

        [Output, DataType("BOOL")]
        public bool enabled;

        [Output, DataType("DINT")]
        public int stationId;

        [Output, DataType("STRING")]
        public IecString80 ipAddr;

        [Output, DataType("DINT")]
        public int port;


        [Initialization]
        public void __Init()
        {

            if (simulate)
            {

            }
            
            
            //
            // TODO: Initialize the variables of the function block here
            //
        }

        [Execution]
        public void __Process()
        {

            

        }
    }
}
