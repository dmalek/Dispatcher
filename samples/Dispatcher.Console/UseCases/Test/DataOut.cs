namespace Dispatcher.Console.UseCases.Test
{
    public record DataOut : IResponse
    {
        public int Sum { get; set; } = 0;
    }
}
