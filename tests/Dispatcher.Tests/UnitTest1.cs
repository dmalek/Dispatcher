using Microsoft.Extensions.DependencyInjection;

namespace Dispatcher.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly DataIn _request = new DataIn { Message = "Hello World!" };
        private readonly DataChanged _notification = new DataChanged();

        private IServiceProvider _serviceProvider;

        public UnitTest1()
        {
            var services = new ServiceCollection();
            services.AddDispatcher();

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        public async Task Test_SendRequest()
        {
            var response = await _serviceProvider.DispatchAsync(_request);
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Lenght, 12);
        }

        [TestMethod]
        public async Task Test_PublishNotification()
        {
            await _serviceProvider.PublishAsync(_notification);           
        }
    }
}