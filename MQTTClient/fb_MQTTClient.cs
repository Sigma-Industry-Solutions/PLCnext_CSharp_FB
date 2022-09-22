using System;
using System.Iec61131Lib;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace MQTTClient
{
    [FunctionBlock]
    public class FunctionBlock1
    {
        [Input]
        public short IN1;
        [Input]
        public short IN2;
        [Output, DataType("WORD")]
        public ushort OUT;

        [Initialization]
        public void __Init()
        {
            //
            // TODO: Initialize the variables of the function block here
            //
        }

        [Execution]
        public void __Process()
        {
            OUT = (ushort)(IN1 + IN2);
        }
    }
}
