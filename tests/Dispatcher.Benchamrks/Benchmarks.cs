using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatcher.Benchamrks
{
    public class Benchmarks
    {
        private readonly DataIn _request = new DataIn { Message = "Hello World!" };
        private readonly DataChanged _notification = new DataChanged();
       
        private IServiceProvider _serviceProvider;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();

            services.AddSingleton(TextWriter.Null);
            services.AddDispatcher();

            _serviceProvider = services.BuildServiceProvider();

        }


        [Benchmark]
        public async Task SendingRequests_WithDispatcher()
        {
            var response = await _serviceProvider.DispatchAsync(_request);
        }

        [Benchmark]
        public async Task PublishingNotifications_WithDispatcher()
        {
            await _serviceProvider.PublishAsync(_notification);
        }

        [Benchmark]
        public async Task SendingRequests_CallingHandlerFromServiceProvider()
        {
            var handler = _serviceProvider.GetService<IRequestHandler<DataIn, DataOut>>();
            var response = await handler.HandleAsync(_request);
        }

        [Benchmark]
        public async Task PublishingNotifications_CallingHandlerFromServiceProvider()
        {
            var handlers = _serviceProvider.GetServices<INotificationHandler<DataChanged>>();
            await handlers.First().ReceiveAsync(new DataChanged());
        }

        [Benchmark]
        public async Task SendingRequests_CallingHandlerDirectly()
        {
            var dataHandler = new DataHandler();    
            var response = await dataHandler.HandleAsync(_request);
        }

        [Benchmark]
        public async Task PublishingNotifications_CallingHandlerDirectly()
        {
            var dataChangedEvent = new DataChangedEvent();
            await dataChangedEvent.ReceiveAsync(new DataChanged());
        }
    }
}
