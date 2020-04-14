using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Utilities;

namespace Microservice2
{
    public static class MicroService2
    {
        [FunctionName("MicroService2")]
        public static void Run([QueueTrigger("microservices2", Connection = "StorageConnectionString")]MicroServiceMessage microServiceMessage, ILogger log)
        {
            log.LogInformation($"microservices2 function processed with message : {microServiceMessage.Message} - {microServiceMessage.CorrelationId}");
            throw new DataMisalignedException(microServiceMessage.Message);
        }
    }
}
