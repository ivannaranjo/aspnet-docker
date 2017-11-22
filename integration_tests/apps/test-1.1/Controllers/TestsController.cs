using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Api.Gax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Test_1_1.Controllers
{
    /// <summary>
    /// This controller implements all of the tests for this app.
    /// </summary>
    [Route("/")]
    public class TestsController : Controller
    {
        private static readonly Lazy<string> s_projectId = new Lazy<string>(GetProjectId);

        private readonly ILogger _logger;

        public TestsController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(TestsController));
        }

        /// <summary>
        /// Basic serving test, verifies that the app is running by returning a well known
        /// string.
        /// </summary>
        [HttpGet]
        public string ServingTest()
        {
            return "Hello World!";
        }

        [HttpPost("/logging_standard")]
        public string StandardLoggingTests([FromBody] LoggingData data)
        {
            var loggerForLevel = GetLogger(data.Level);
            var project = s_projectId.Value;
            loggerForLevel(data.Token);
            return $"{data.LogName}/{data.Level}";
        }

        private static string GetProjectId()
        {
            var instance = Platform.Instance();
            return instance.ProjectId ?? GetEnvironmentProjectId();
        }

        private static string GetEnvironmentProjectId()
        {
            return Environment.GetEnvironmentVariable("GOOGLE_CLOUD_PROJECT");
        }

        private Action<string> GetLogger(string level)
        {
            switch (level)
            {
                case "WARNING":
                    return x => _logger.LogDebug(x);

                case "ERROR":
                    return x => _logger.LogError(x);

                case "CRITICAL":
                    return x => _logger.LogCritical(x);

                default:
                    return x => _logger.LogDebug(x);
            }
        }
    }
}
