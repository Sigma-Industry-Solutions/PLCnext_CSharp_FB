using System;
using System.Iec61131Lib;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace Energizer__PLCnextFirmwareLibrary
{
    // Template with a function container including several function definitions
    [FunctionContainer]
    public static class FunctionContainer1
    {
        // A and B are both interpreted as DINT.
        // return type is optional, now interpreted as DINT.
        [Function]
        public static int FUN1(int A, int B)
        {
            return A + B;
        }

        // Parameter type of A and B are explicitly set to DWORD. Otherwise they may be interpreted as UDINT.
        // The Input-definition is optional.
        [Function, DataType("DWORD")] //Return type explicitly set
        public static uint FUN2([Input, DataType("DWORD")] uint A, [Input, DataType("DWORD")] uint B)
        {
            return A | B;
        }
    }
}
