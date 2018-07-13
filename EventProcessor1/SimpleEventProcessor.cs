
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EventProcessor1
{
    class SimpleEventProcessor : IEventProcessor
    {
        Stopwatch checkpointStopWatch;

        private CloudStorageAccount _account;

        public String deviceId = "ManualConnectedCiotola";
        private CloudBlobClient _client;
        private CloudBlobContainer _container;

        private string photo;

        async Task IEventProcessor.CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("Processor Shutting Down. Partition '{0}', Reason: '{1}'.", context.Lease.PartitionId, reason);
            if (reason == CloseReason.Shutdown)
            {
                await context.CheckpointAsync();
            }
        }

        Task IEventProcessor.OpenAsync(PartitionContext context)
        {
            Console.WriteLine("SimpleEventProcessor initialized.  Partition: '{0}', Offset: '{1}'", context.Lease.PartitionId, context.Lease.Offset);
            this.checkpointStopWatch = new Stopwatch();
            this.checkpointStopWatch.Start();
            return Task.FromResult<object>(null);
        }

        async Task IEventProcessor.ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var message in messages)
            {
                var json = Encoding.UTF8.GetString(message.Body.Array);
                //Console.WriteLine($"{context.PartitionId} [{message.SystemProperties.EnqueuedTimeUtc}]");
                // Console.WriteLine($" Enqueue Time : {message.SystemProperties.EnqueuedTimeUtc}");
                // Console.WriteLine($" Local Time: {DateTime.Now.ToUniversalTime()}");

                Console.WriteLine($"{json}");

                var ev = JsonConvert.DeserializeObject<JObject>(json);

                // photo = ev.Value<string>("carica");
                

        }
    }
}