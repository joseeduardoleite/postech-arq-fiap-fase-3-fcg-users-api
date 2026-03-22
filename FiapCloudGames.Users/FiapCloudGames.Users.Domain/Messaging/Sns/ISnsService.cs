namespace FiapCloudGames.Users.Domain.Messaging.Sns;

public interface ISnsService
{
    Task PublishUserCreatedAsync(UserCreatedEvent userEvent, CancellationToken cancellationToken);
}