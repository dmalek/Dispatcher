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

| Method                                                    | Mean      | Error     | StdDev    |
|---------------------------------------------------------- |----------:|----------:|----------:|
| SendingRequests_WithDispatcher                            | 565.24 ns | 11.197 ns | 14.948 ns |
| PublishingNotifications_WithDispatcher                    | 649.79 ns |  7.833 ns |  6.115 ns |
| SendingRequests_CallingHandlerFromServiceProvider         |  81.84 ns |  1.601 ns |  1.644 ns |
| PublishingNotifications_CallingHandlerFromServiceProvider |  97.95 ns |  2.003 ns |  2.936 ns |
| SendingRequests_CallingHandlerDirectly                    |  51.48 ns |  1.059 ns |  1.219 ns |
| PublishingNotifications_CallingHandlerDirectly            |  37.33 ns |  0.767 ns |  0.821 ns |

