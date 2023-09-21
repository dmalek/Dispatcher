namespace Dispatcher
{
    public interface IRequest
    {
    }

    public interface IRequest<TResult> : IRequest
    {
    }
}