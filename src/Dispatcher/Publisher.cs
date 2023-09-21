using Microsoft.Extensions.DependencyInjection;

namespace Dispatcher
{
    public static class Publisher
    {
        /// <summary>
        /// 
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

            //using var scope = serviceProvider.CreateScope();
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
            IEnumerable<dynamic>? subscribers = serviceProvider.GetServices(handlerType);

            var tasks = subscribers
                .Select(handler =>
                {
                    var methodInfo = handlerType.GetMethod(nameof(INotificationHandler<INotification>.ReceiveAsync));
                    if (methodInfo != null)
                    {
                        return (Task)methodInfo.Invoke(handler, new object[] { notification, cancellationToken });
                    }
                    return Task.CompletedTask;
                })
                .ToArray();

            await Task.WhenAll(tasks);
        }
    }
}
