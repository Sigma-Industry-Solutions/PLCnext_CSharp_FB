using System;


namespace Energizer__PLCnextFirmwareLibrary
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Random r = new Random(5);
                int randomData = r.Next(5)+50;
                Random r2 = new Random(20);
                int randomId = r.Next(20);
                fb_SensorDataTransfer sdt = new fb_SensorDataTransfer(randomData, randomId);
                sdt.__Init();
                sdt.__Process();
            }

           
        }
    }
}
