﻿namespace NLog.NetCoreLib
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NLog.Config;
    using NLog.Extensions.Logging;
    using NLog.Targets;
    using NUnit.Framework;

    [TestFixture]
    internal abstract class NLogNetCoreTest
    {
        [SetUp]
        public void NLogNetCoreTestSetUp()
        {
            pLog = _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(GetType().Name);
        }

        private static readonly ServiceProvider _serviceProvider;

        ///<summary>One-time only construction logic.</summary>
        static NLogNetCoreTest()
        {
            var nlogContainer = new NLogContainer();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog();
                nlogContainer.AddLogging(builder);
            });
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected ILogger pLog { get; private set; }
    }
}