using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelProgramNETCore
{
    public class FooService : IFooService
    {
        private readonly ILogger<FooService> _logger;

        public FooService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FooService>();
        }

        public void DoWork() => _logger.LogInformation($"Foo did work");
    }
}
