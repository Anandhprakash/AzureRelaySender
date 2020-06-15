using System;
using Microsoft.Azure.Relay;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace AzureRelaySender
{
	class Program
	{
		//set your connection parameters
		//private const string Relaynamespace = "{Relaynamespace}.servicebus.windows.net";
		//private const string CoonectionName = "{HybridconnectionName}";
		//private const string KeyName = "{SASKeyName}";
		//private const string Key = "{SASKey}";

		//set your connection parameters
		private const string RelayNamespace = "azurerelaydemo.servicebus.windows.net";
		private const string ConnectionName = "relayhbconn";
		private const string KeyName = "cSend";
		private const string Key = "dkH9EcYYMn6ruSdy34zxj7WGbiGu79gzX24PMBx/yA8=";									

		static void Main(string[] args)
		{
			RunAsync().GetAwaiter().GetResult();
		}

		private static async Task RunAsync()
		{
			var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
			KeyName, Key);

			var uri = new Uri(string.Format("https://{0}/{1}", RelayNamespace, ConnectionName));

			var token = (await tokenProvider.GetTokenAsync(uri.AbsoluteUri, TimeSpan.FromHours(1))).TokenString;

			var client = new HttpClient();
			var request = new HttpRequestMessage()
			{
				RequestUri = uri,
				Method = HttpMethod.Get,
			};

			request.Headers.Add("ServiceBusAuthorization", token);
			var response = await client.SendAsync(request);
			Console.WriteLine(await response.Content.ReadAsStringAsync());
			Console.ReadLine();
		}

	}
}
