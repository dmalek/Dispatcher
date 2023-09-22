using Microsoft.Extensions.DependencyInjection;
using Dispatcher;
using System.Runtime.CompilerServices;

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

            services.AddSingleton(TextWriter.Null);
            services.AddDispatcher();

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        public async Task  TestMethod1()
        {
            var response = await _serviceProvider.DispatchAsync(_request);
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Lenght, 12);
        }
    }
}