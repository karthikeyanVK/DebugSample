using System;
using System.Net.Cache;

using Microsoft.Azure.Storage; // Namespace for CloudStorageAccount
using Microsoft.Azure.Storage.Queue; // Namespace for Queue storage types
using Newtonsoft.Json;
namespace Utilities
{
    public class MicroServiceMessage
    {
        public string CorrelationId { get; set; }
        public string Message { get; set; }

        public void PushToService(string correlationId, string queueName, string queMessage, string connectionString = null)
        {
            connectionString = string.IsNullOrEmpty(connectionString)
                ? Environment.GetEnvironmentVariable("StorageConnectionString")
                : connectionString;
            // Parse the connection string and return a reference to the storage account.
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            var queue = queueClient.GetQueueReference(queueName);

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            var message = new CloudQueueMessage(JsonConvert.SerializeObject(new MicroServiceMessage
            {
                CorrelationId = correlationId,
                Message = queMessage
            }));

            queue.AddMessage(message);

        }
    }

}
