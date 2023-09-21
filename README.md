# Dispatcher

Simple mediator pattern implemented by service provider extensions.

## Registering with IServiceCollection

```
services.AddDispatcher();
```
```
services.AddDispatcher(typeof(Program).Assembly);
```

This registers:
- IRequestHandler<>
- IRequestHandler<,>
- INotificationHandler<>

## Request/response

Request:
```
public record DataIn: IRequest<DataOut>
{
    public int A { get; set; } = 0;
    public int B { get; set; } = 0;
}
```

Response:
```
public record DataOut : IResponse
{
    public int Sum { get; set; } = 0;
}
```

Handler:
```
public async Task<DataOut> HandleAsync(DataIn request, CancellationToken cancellationToken = default)
{
    var reault = new DataOut()
    {
        Sum = request.A + request.B
    };

    return await Task.FromResult(reault);
}
```

Send request and get response:
```
var response = await _serviceProvider.DispatchAsync(new DataIn()
{
    A = 8,
    B = 5
});
```
