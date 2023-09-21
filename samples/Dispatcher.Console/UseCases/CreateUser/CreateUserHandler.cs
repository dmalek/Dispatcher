using Dispatcher.Console.Events;
using Microsoft.Extensions.Logging;

namespace Dispatcher.Console.UseCases.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _services;
        public CreateUserHandler(
            IServiceProvider services,
            ILoggerFactory loggerFactory
            )
        {
            _services = services;
            _loggerFactory = loggerFactory;
        }
        public async Task HandleAsync(CreateUser request, CancellationToken cancellationToken = default)
        {
            var logger = _loggerFactory.CreateLogger<CreateUser>();
            logger.LogInformation($"New user {request.FirstName} {request.LastName} ({request.Email}) has been created.");

            _services.PublishAsync(new UserDataChangedEvent()
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            });

            await Task.CompletedTask;
        }
    }
}
