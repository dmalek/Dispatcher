# Dispatcher
> Extension methods for an `IServiceProvider`
 
Dipatcher is designed to be a simple, generic and flexible way to dispatch requests to their corresponding handlers using dependency injection and async processing. It allows you to send a request, and it dynamically determines the appropriate handler based on the request type, invokes the handler's HandleAsync method, and returns the response. 

> Simple as this: **response = await _serviceProvider.DispatchAsync(request);**

## Registering with IServiceCollection

```
services.AddDispatcher();
```
```
services.AddDispatcher(typeof(Program).Assembly);
```

This registers:
- `IRequestHandler<>`
- `IRequestHandler<,>`
- `INotificationHandler<>` 

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

| Method                                                    | Mean      | Error     | StdDev    | Median    |
|---------------------------------------------------------- |----------:|----------:|----------:|----------:|
| SendingRequests_WithDispatcher                            | 573.78 ns | 11.348 ns | 13.069 ns | 569.44 ns |
| PublishingNotifications_WithDispatcher                    | 701.46 ns | 13.842 ns | 17.999 ns | 703.28 ns |
| SendingRequests_CallingHandlerFromServiceProvider         |  82.97 ns |  1.669 ns |  2.228 ns |  82.59 ns |
| PublishingNotifications_CallingHandlerFromServiceProvider |  98.64 ns |  1.967 ns |  3.446 ns |  97.96 ns |
| SendingRequests_CallingHandlerDirectly                    |  53.99 ns |  1.111 ns |  2.829 ns |  53.45 ns |
| PublishingNotifications_CallingHandlerDirectly            |  37.72 ns |  0.940 ns |  2.620 ns |  36.92 ns |


