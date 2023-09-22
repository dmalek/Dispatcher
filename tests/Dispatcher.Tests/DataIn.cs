namespace Dispatcher.Tests
{
    internal class DataIn : IRequest<DataOut>
    {
        public string Message { get; set; } = string.Empty;
    }
}
