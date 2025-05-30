// 1. Define an abstraction
public interface IMessageService
{
    void Send(string message);
}

// 2. Provide a concrete implementation
public class EmailService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine($"[Email] {message}");
    }
}

// 3. Consume the service via constructor injection
public class NotificationManager
{
    private readonly IMessageService _messageService;

    // DI happens here: we ask for IMessageService rather than new one ourselves
    public NotificationManager(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string message)
    {
        _messageService.Send(message);
    }
}

// 4. Composition root: wire up dependencies and run
class Program
{
    static void Main()
    {
        // Manually create the dependency
        IMessageService emailSvc = new EmailService();

        // Inject it into the consumer
        var notifier = new NotificationManager(emailSvc);

        notifier.Notify("Dependency Injection in action!");
    }
}

