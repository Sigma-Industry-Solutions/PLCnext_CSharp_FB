using System;


namespace Energizer__PLCnextFirmwareLibrary
{

    public class SensorDataGatewayPrg
    {
        // Use the attributes [Global] and either [InputPort] or [OutputPort] to mark fields, 
        // that should exchange data with other IEC- or C#-Programs
        
        public int a = 1;
   
        public int b = 2;
        public int c = 3;
    
        public void __Init()
        {
            //
            // TODO: Initialize the variables of the program here
            //
        }

        public void __Process()
        {
            a = b + c;
        }
    }
}
