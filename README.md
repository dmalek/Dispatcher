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
public class SumHandler : IRequestHandler<DataIn, DataOut>
{
    public async Task<DataOut> HandleAsync(DataIn request, CancellationToken cancellationToken = default)
    {
        var result = new DataOut()
        {
            Sum = request.A + request.B
        };

        return await Task.FromResult(result);
    }
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

## Event

Notification:
```
public class UserDataChangedEvent : INotification
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
```

Event:
```
public class UserDataChangedEventHandler_SendEmail : INotificationHandler<UserDataChangedEvent>
{
    public async Task ReceiveAsync(UserDataChangedEvent notification, CancellationToken cancellationToken = default)
    {
        // Send email to user
        await Task.CompletedTask;
    }
}
```

## Benchmarks

| Method                                                    | Mean        | Error     | StdDev    |
|---------------------------------------------------------- |------------:|----------:|----------:|
| SendingRequests_WithDispatcher                            |   534.68 ns | 10.391 ns | 13.142 ns |
| PublishingNotifications_WithDispatcher                    | 2,863.16 ns | 27.259 ns | 24.164 ns |
| SendingRequests_CallingHandlerFromServiceProvider         |    90.07 ns |  1.823 ns |  3.806 ns |
| PublishingNotifications_CallingHandlerFromServiceProvider |    99.60 ns |  2.024 ns |  4.043 ns |
| SendingRequests_CallingHandlerDirectly                    |    50.81 ns |  1.043 ns |  1.713 ns |
| PublishingNotifications_CallingHandlerDirectly            |    36.27 ns |  0.754 ns |  1.624 ns |
