namespace Dispatcher.Benchamrks
{
    internal class DataIn : IRequest<DataOut>
    {
        public string Message { get; set; }
    }
}
