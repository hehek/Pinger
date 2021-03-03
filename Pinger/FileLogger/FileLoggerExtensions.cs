using System;
using Microsoft.Extensions.Logging;

namespace Pinger.FileLogger
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                        FileLoggerConfiguration config)
        {
            factory.AddProvider(new FileLoggerProvider(config));
            return factory;
        }

        public static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder) =>
        builder.AddFileLogger(
            new FileLoggerConfiguration());

        public static ILoggingBuilder AddFileLogger(
            this ILoggingBuilder builder,
            Action<FileLoggerConfiguration> configure)
        {
            var config = new FileLoggerConfiguration();
            configure(config);

            return builder.AddFileLogger(config);
        }

        public static ILoggingBuilder AddFileLogger(
            this ILoggingBuilder builder,
            FileLoggerConfiguration config)
        {
            builder.AddProvider(new  FileLoggerProvider(config));
            return builder;
        }
    }
}
