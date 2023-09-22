namespace Dispatcher.Tests
{
    internal class DataChangedEvent_1 : INotificationHandler<DataChanged>
    {
        public async Task ReceiveAsync(DataChanged notification, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(notification);
            await Task.CompletedTask;
        }
    }

    internal class DataChangedEvent_2 : INotificationHandler<DataChanged>
    {
        public async Task ReceiveAsync(DataChanged notification, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(notification);
            await Task.CompletedTask;
        }
    }
}
