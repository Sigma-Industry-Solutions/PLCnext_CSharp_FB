using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Grpc.Net.Client;
using Arp.Device.Interface.Services.Grpc;
using Arp.Plc.Gds.Services.Grpc;

namespace GrpcSampler
{
	class Program
	{
		static void Main()
		{
			// The code to connect to a Unix Domain Socket is from:
			// https://docs.microsoft.com/en-us/aspnet/core/grpc/interprocess?view=aspnetcore-6.0

			var udsEndPoint = new UnixDomainSocketEndPoint("/run/plcnext/grpc.sock");
			var connectionFactory = new UnixDomainSocketConnectionFactory(udsEndPoint);
			var socketsHttpHandler = new SocketsHttpHandler
			{
				ConnectCallback = connectionFactory.ConnectAsync
			};

			// Create a gRPC channel to the PLCnext unix socket
			using var channel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
			{
				HttpHandler = socketsHttpHandler
			});

			// Create a gRPC client for the Device Status Service on that channel
			var grpc_status_client = new IDeviceStatusService.IDeviceStatusServiceClient(channel);

			// Create a gRPC client for the Data Access Service on that channel
			var grpc_data_client = new IDataAccessService.IDataAccessServiceClient(channel);

			// Create an item to get from the Device Status Service
			// Item identifiers are listed in the PLCnext Info Center:
			// https://www.plcnext.help/te/Service_Components/Remote_Service_Calls_RSC/RSC_device_interface_services.htm#IDeviceStatusService

			var item = new IDeviceStatusServiceGetItemRequest();
			item.Identifier = "Status.Board.Temperature.Centigrade";

			// Create a variable to get from the Data Access Service
			var data = new IDataAccessServiceReadSingleRequest();
			data.PortName = "Arp.Plc.Eclr/gv_SensorGatewayState";

			// Response variables
			IDeviceStatusServiceGetItemResponse grpc_status_response;
			IDataAccessServiceReadSingleResponse grpc_data_response;

			// Endless loop
			while (true)
			{
				// Request the item from the Device Status Service
				grpc_status_response = grpc_status_client.GetItem(item);

				// Request data from the Data Access Service
				grpc_data_response = grpc_data_client.ReadSingle(data);

				// Report the results
				var temperature = grpc_status_response.ReturnValue.Int8Value;
				var ai1 = grpc_data_response.ReturnValue.Value.Int16Value;

				Console.WriteLine("Board Temperature = " + temperature + "°C");
				Console.WriteLine("MainInstance1.AI1 = " + ai1);

				// Wait for 1 second
				Thread.Sleep(1000);
			}
		}
	}

	public class UnixDomainSocketConnectionFactory
	{
		private readonly EndPoint _endPoint;

		public UnixDomainSocketConnectionFactory(EndPoint endPoint)
		{
			_endPoint = endPoint;
		}

		public async ValueTask<Stream> ConnectAsync(SocketsHttpConnectionContext _,
			CancellationToken cancellationToken = default)
		{
			var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

			try
			{
				await socket.ConnectAsync(_endPoint, cancellationToken).ConfigureAwait(false);
				return new NetworkStream(socket, true);
			}
			catch
			{
				socket.Dispose();
				throw;
			}
		}
	}
}