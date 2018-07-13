using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace EventProcessor1
{
    class Program
    {
        static void Main(string[] args)
        {

            string eventHubConnectionString = "Endpoint=sb://iothub-ns-albzagioth-467180-8764f7122c.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=tUsKm77xu6EM6nvi/Iv6crht/a+QUU/5ORSuNdqLPto=";
            string eventHubName = "albzagiothub2";
            string storageAccountName = "containeraccount";
            string storageAccountKey = "cu+ZYEpjk+r+gKM+Tr/srgXIuA7Q0t0m4VBm7OD3vgeRD1RTb0StYMZ8zto1nCY9vSFPjwKdu06HhqSvFNsm4A==";
            string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            Console.WriteLine("Registering EventProcessor...");
            var options = new EventProcessorOptions();
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
