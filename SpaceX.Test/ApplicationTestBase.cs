using SpaceX.API;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SpaceX.Test
{
    public class ApplicationTestBase : IDisposable
    {
        private IServiceScope _serviceScope;

        public ApplicationTestBase()
        {
            SetupApplication();
        }

        protected IServiceProvider Services => _serviceScope.ServiceProvider;

        protected IMediator Mediator => Services.GetRequiredService<IMediator>();

        public Task<TResult> SendAsync<TResult>(IRequest<TResult> request)
        {
            return Mediator.Send(request);
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
        private IConfiguration LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false);

            return config.Build();
        }
        private void SetupApplication()
        {
            var config = LoadConfiguration();
            var serviceCollection = ConfigureServices(config);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _serviceScope = serviceProvider.CreateScope();
        }
        private IServiceCollection ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            services.RegisterServices(configuration);
            services.AddLogging();
            return services;
        }
    }
}
