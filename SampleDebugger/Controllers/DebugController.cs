using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utilities;

namespace SampleForDebugging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly IConfiguration _config;
        [HttpPost]
        public bool Post(string message)
            {
            var microServiceMessage = new MicroServiceMessage();
            var correlationId = Request.Headers["X-Correlation-ID"].ToString();
            correlationId = Guid.NewGuid().ToString();
            microServiceMessage.PushToService(correlationId, "microservices1", message, _config.GetValue<string>(
                "StorageConnectionString"));
            return true;
        }
        public DebugController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public bool Get(string message)
        {
            return true;
        }
    }


}