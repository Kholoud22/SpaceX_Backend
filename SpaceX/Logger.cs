using Serilog;
using Serilog.Exceptions;

namespace SpaceX
{
    public static class Logger
    {
        public static Action<HostBuilderContext, IServiceProvider, LoggerConfiguration> ConfigureLogger =>
            (context, service, configuration) =>
            {
                var env = context.HostingEnvironment;
                configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .Enrich.WithExceptionDetails()
                    .ReadFrom.Configuration(context.Configuration);
            };
    }
}
