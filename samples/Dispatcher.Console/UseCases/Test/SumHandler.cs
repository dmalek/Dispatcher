namespace Dispatcher.Console.UseCases.Test
{
    public class SumHandler : IRequestHandler<DataIn, DataOut>
    {
        public async Task<DataOut> HandleAsync(DataIn request, CancellationToken cancellationToken = default)
        {
            var reault = new DataOut()
            {
                Sum = request.A + request.B
            };

            return await Task.FromResult(reault);
        }
    }
}
