namespace Dispatcher
{
    /// <summary>
    /// Defines a handler for request
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {

        /// <summary>
        /// Handle request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response</returns>
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }


    /// <summary>
    /// Defines a handler for request
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
    {
        /// <summary>
        /// Handle request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}





