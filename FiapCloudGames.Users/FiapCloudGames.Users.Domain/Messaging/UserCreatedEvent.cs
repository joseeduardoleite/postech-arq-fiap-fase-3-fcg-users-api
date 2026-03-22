namespace FiapCloudGames.Users.Domain.Messaging;

public class UserCreatedEvent
{
    public Guid UsuarioId { get; init; }
    public string? Nome { get; set; }
}