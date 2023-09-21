namespace Dispatcher
{
    public interface INotificationHandler<TNotification>
        where TNotification : INotification
    {
        Task ReceiveAsync(TNotification notification, CancellationToken cancellationToken = default);
    }
}
