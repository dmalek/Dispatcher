# Dispatcher

public class Program
{
    private static IServiceProvider _serviceProvider;
    public static async Task Main(string[] args)
    {
        //setup our DI
        IServiceCollection services = new ServiceCollection();
        services.AddLogging(builder =>
        {
            builder.AddConsole();
        });

        // Setup dispatcher and add request handlers 
        services.AddDispatcher();
        _serviceProvider = services.BuildServiceProvider();       
        
        // Create new user
        await _serviceProvider.DispatchAsync(new CreateUser()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@server.com",
            PhoneNumber = "3859155554444"
        });

        // Update user data
        await _serviceProvider.DispatchAsync(new UpdateUser()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@server.com",
            PhoneNumber = "3859155554444"
        });
    }
}
