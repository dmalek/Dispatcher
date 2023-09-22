using Microsoft.Extensions.Logging;

namespace Dispatcher.Console.Events
{
    public class UserDataChangedEvent : INotification
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class UserDataChangedEventHandler_SendEmail : INotificationHandler<UserDataChangedEvent>
    {
        private readonly ILoggerFactory _loggerFactory;
        public UserDataChangedEventHandler_SendEmail(
            ILoggerFactory loggerFactory
            )
        {
            _loggerFactory = loggerFactory;
        }
        public async Task ReceiveAsync(UserDataChangedEvent notification, CancellationToken cancellationToken = default)
        {
            var logger = _loggerFactory.CreateLogger<UserDataChangedEventHandler_SendEmail>();
            logger.LogInformation($"Sending email to {notification.Email}.");

            await Task.CompletedTask;
        }
    }

    public class UserDataChangedEventHandler_SendSMS : INotificationHandler<UserDataChangedEvent>
    {
        private readonly ILoggerFactory _loggerFactory;
        public UserDataChangedEventHandler_SendSMS(
            ILoggerFactory loggerFactory
            )
        {
            _loggerFactory = loggerFactory;
        }

        public async Task ReceiveAsync(UserDataChangedEvent notification, CancellationToken cancellationToken = default)
        {
            var logger = _loggerFactory.CreateLogger<UserDataChangedEventHandler_SendSMS>();
            logger.LogInformation($"Sending SMS to {notification.PhoneNumber}.");

            await Task.CompletedTask;
        }
    }
}
