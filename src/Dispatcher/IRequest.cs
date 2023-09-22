namespace Dispatcher
{
    /// <summary>
    /// Defines a request
    /// </summary>
    public interface IRequest
    {
    }

    /// <summary>
    /// Defines a request with response
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IRequest<TResult> : IRequest
    {
    }
}