using Microsoft.Extensions.DependencyInjection;

namespace Dispatcher
{
    public static class Publisher
    {
        /// <summary>
        /// Send notification to services that handles notification
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task PublishAsync(this IServiceProvider serviceProvider, INotification notification, CancellationToken cancellationToken = default)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
            IEnumerable<dynamic>? subscribers = serviceProvider.GetServices(handlerType);

            
            foreach (var handler in subscribers)
            {
                await (Task)handlerType
                 .GetMethod(nameof(INotificationHandler<INotification>.ReceiveAsync))?
                 .Invoke(handler, new object[] { notification, cancellationToken });

            }
        }
    }
}
