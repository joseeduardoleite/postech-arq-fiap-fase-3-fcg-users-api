using FiapCloudGames.Users.Domain.Enums;

namespace FiapCloudGames.Users.Application.Dtos;

public record UsuarioDto(
    string? Nome,
    string? Email,
    string? Senha,
    ERole? Role = null
)
{
    public Guid? Id { get; init; }
}