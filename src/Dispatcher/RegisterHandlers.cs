using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dispatcher
{
    public static class RegisterHandlers
    {
        public static IServiceCollection AddDispatcher(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();
            return AddDispatcher(services, assembly);
        }

        public static IServiceCollection AddDispatcher(this IServiceCollection services, Assembly assembly)
        {
            RegisterHandlersByInterface(services, assembly, typeof(IRequestHandler<>));
            RegisterHandlersByInterface(services, assembly, typeof(IRequestHandler<,>));
            RegisterHandlersByInterface(services, assembly, typeof(INotificationHandler<>));

            return services;
        }
  
        private static void RegisterHandlersByInterface(IServiceCollection services, Assembly assembly, Type interfaceType)
        {
            // Find all types in the assembly that implement the specified interface
            var handlerTypes = assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                               i.GetGenericTypeDefinition() == interfaceType));

            foreach (var handlerType in handlerTypes)
            {
                // Find the interface(s) implemented by this type
                var handlerInterfaces = handlerType.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                 i.GetGenericTypeDefinition() == interfaceType);

                foreach (var handlerInterface in handlerInterfaces)
                {
                    // Register the interface with the corresponding implementation
                    services.AddScoped(handlerInterface, handlerType);
                }
            }
        }

    }
}
