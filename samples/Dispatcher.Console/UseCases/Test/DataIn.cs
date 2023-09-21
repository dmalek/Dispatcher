namespace Dispatcher.Console.UseCases.Test
{
    public record DataIn: IRequest<DataOut>
    {
        public int A { get; set; } = 0;
        public int B { get; set; } = 0;
    }
}
