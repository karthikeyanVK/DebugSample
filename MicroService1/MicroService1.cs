using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Utilities;

namespace MicroService1
{
    public static class MicroService1
    {
        [FunctionName("MicroService1")]
        public static void Run([QueueTrigger("microservices1", Connection = "StorageConnectionString")]MicroServiceMessage microServiceMessage, ILogger log)
        {
            log.LogInformation($"microservices1 function started with message : {microServiceMessage.Message} - {microServiceMessage.CorrelationId}");


            microServiceMessage.PushToService(microServiceMessage.CorrelationId, "microservices2", microServiceMessage.Message);
            log.LogInformation($"microservices1 function Processed with message : {microServiceMessage.Message} - {microServiceMessage.CorrelationId}");
        }
    }
}
