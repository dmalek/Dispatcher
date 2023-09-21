using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dispatcher;
using Dispatcher.Console.Events;

namespace Dispatcher.Console.UseCases.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _services;

        public UpdateUserHandler(
            IServiceProvider services,
            ILoggerFactory loggerFactory
            )
        {
            _services = services;
            _loggerFactory = loggerFactory;
        }
        public async Task HandleAsync(UpdateUser request, CancellationToken cancellationToken = default)
        {
            var logger = _loggerFactory.CreateLogger<UpdateUser>();
            logger.LogInformation($"User {request.FirstName} {request.LastName} ({request.Email}) has been updated.");

            _services.PublishAsync(new UserDataChangedEvent()
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            });

            await Task.CompletedTask;
        }
    }
}
