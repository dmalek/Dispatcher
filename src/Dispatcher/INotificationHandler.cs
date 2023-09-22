namespace Dispatcher
{
    /// <summary>
    /// Defines a handler for a notification
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface INotificationHandler<TNotification>
        where TNotification : INotification
    {
        /// <summary>
        /// Receive and handle notification
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ReceiveAsync(TNotification notification, CancellationToken cancellationToken = default);
    }
}
