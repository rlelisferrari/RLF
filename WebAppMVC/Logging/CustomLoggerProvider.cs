﻿using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace ApiCatalogo.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration loggerConfig;

        private readonly ConcurrentDictionary<string, CustomerLogger> loggers =
            new ConcurrentDictionary<string, CustomerLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            this.loggerConfig = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return this.loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, this.loggerConfig));
        }

        public void Dispose()
        {
            this.loggers.Clear();
        }
    }
}