namespace Dispatcher.Tests
{
    internal class DataHandler : IRequestHandler<DataIn, DataOut>
    {
        public async Task<DataOut> HandleAsync(DataIn request, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(new DataOut()
            {
                Lenght = request.Message.Length,
            });
        }
    }
}
