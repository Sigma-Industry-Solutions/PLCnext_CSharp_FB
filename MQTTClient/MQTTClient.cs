using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet;
using System.Threading;
using Serilog;
using System.Text.Json;

namespace PLCNextAutoConfigMQTTClient
{
    internal static class ObjectExtensions
    {
        public static TObject DumpToConsole<TObject>(this TObject @object)
        {
            var output = "NULL";
            if (@object != null)
            {
                output = JsonSerializer.Serialize(@object, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }

            Log.Logger.Information($"[{@object?.GetType().Name}]:\r\n{output}");
            return @object;
        }
    }
    public static class MQTTClient
    {

        static IMqttClient client;
        static CancellationToken timeout = new CancellationToken();

        public static void Connect_Client(String jsonPayload, String topic)
        {
            var mqttFactory = new MqttFactory();
            bool connected = false;
            using (client = mqttFactory.CreateMqttClient())
            {
                
                var mqttOptions = new MqttClientOptionsBuilder().WithTcpServer(ServerInfo.SERVER_URL, ServerInfo.PORT).WithClientId("ValidClientId").Build();

                var clientSubscribeOptions = new MqttClientSubscribeOptionsBuilder().WithTopicFilter("data/modbus").WithTopicFilter("configure/opcua").Build();
                var clientDisconnectOptions = new MqttClientDisconnectOptionsBuilder().WithReasonString("Manually disconnecting!").Build();

                var message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(jsonPayload).Build();

                Log.Information("MQTT Client is connecting");
                
                
                var t = Task.Run(
                    async () =>
                    {
                        int reconnectLimit = 0;
                        
                        
                        while (reconnectLimit < 5)
                        {
                            try
                            {
                                if (!client.IsConnected)
                                {
                                    await client.ConnectAsync(mqttOptions, timeout);
                                    Log.Information("MQTT Client established connection");
                                    await client.PingAsync(timeout);
                                    Log.Information("MQTT Client successfully pinged broker "+ ServerInfo.SERVER_URL);
                                    await client.SubscribeAsync(clientSubscribeOptions, timeout);
                                    Log.Information("MQTT Client successfully subscribed to broker on topic configure/modbus and configure/opcua");
                                    await client.PublishAsync(message, timeout);
                                    await client.PublishAsync(message, timeout);
                                    await client.PublishAsync(message, timeout);
                                    Log.Information("MQTT client published message");
                                    client.ApplicationMessageReceivedAsync += ApplicationMessageReceivedAsync;
                                    // await client.DisconnectAsync(clientDisconnectOptions, timeout);
                                    // Log.Information("MQTT Client diconnected manually");
                                }
                            }
                            catch (OperationCanceledException)
                            {
                                Log.Warning("MQTT Client canceled it's connection to broker!");
                                connected = true;
                            }
                            catch 
                            {
                                Log.Warning("MQTT Client lost connection!");
                                connected = true;
                            }
                            finally
                            {
                                Log.Warning("MQTT Client trying to reconnect in 5 seconds!");
                                await Task.Delay(TimeSpan.FromSeconds(5));
                                reconnectLimit++;
                            }
                        }

                        Log.Error("MQTT Client could not connect!");
                        
                    }
                );

                t.Wait();

                if(connected || t.IsCompleted)
                {
                    Log.Information("MQTT connected and disconnected perfectly");

                }
                else if(t.IsCanceled || t.IsFaulted)
                    Log.Information("MQTT never connected");
            }
        }

        public static void PublishMessage(String jsonPayload, String topic)
        {
            var message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(jsonPayload).Build();
            // await client.PublishAsync(message, timeout);
            Log.Information("MQTT client published message");
        }

        public static async Task ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            Log.Information(e.ClientId);
            string s = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Log.Information("Data recieved in client: " + s);

            //Sensors sensors = JsonConvert.DeserializeObject<Sensors>(s);
            //for (int i = 0; i < sensors.sensors.Length; i++)
            //{
            //    Sensor sensor = sensors.sensors[i];
            //    string data = "ENABLE=" + sensor.enable + "\n" + "IP=" + sensor.ip_adress + "\n" + "PORT=" + sensor.port + "\n" + "RECONNECT_DELAY=" + sensor.reconnect_delay + "\n" + "TIMEOUT=" + sensor.timeout + "\n";
            //    await File.AppendAllTextAsync(@"data2.config", data);

            //}
        }

        public class Sensors
        {
            public Sensors()
            {

            }
        }

    }
}
