﻿using Microsoft.Extensions.DependencyInjection;

namespace Dispatcher
{
    public static class Dispatcher
    {
        /// <summary>
        /// Send request to handler and return response
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<TResponse> DispatchAsync<TResponse>(this IServiceProvider serviceProvider, IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            dynamic? handler = serviceProvider.GetRequiredService(handlerType);

            var handleMethod = handlerType?
                .GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.HandleAsync));

            if (handleMethod == null)
            {
                // Handle the case when the handlerType or handleMethod is null
                throw new InvalidOperationException("Invalid handlerType or handleMethod is null.");
            }

            var response = await (Task<TResponse>)handleMethod.Invoke(handler, new object[] { request, cancellationToken });
            return response;            
        }

        /// <summary>
        /// Send request to handler 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task DispatchAsync(this IServiceProvider serviceProvider, IRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
            dynamic? handler = serviceProvider.GetRequiredService(handlerType);

            var handleMethod = handlerType
                 .GetMethod(nameof(IRequestHandler<IRequest>.HandleAsync));

            if (handleMethod == null)
            {
                // Handle the case when the handlerType or handleMethod is null
                throw new InvalidOperationException("Invalid handlerType or handleMethod is null.");
            }

            await (Task)handleMethod.Invoke(handler, new object[] { request, cancellationToken });
        }
    }
}
