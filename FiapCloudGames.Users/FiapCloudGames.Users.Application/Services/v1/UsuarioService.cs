using FiapCloudGames.Users.Domain.Entities;
using FiapCloudGames.Users.Domain.Messaging;
using FiapCloudGames.Users.Domain.Messaging.Sns;
using FiapCloudGames.Users.Domain.Repositories.v1;
using FiapCloudGames.Users.Domain.Services.v1;

namespace FiapCloudGames.Users.Application.Services.v1;

public sealed class UsuarioService(
    IUsuarioRepository usuarioRepository,
    ISnsService sqsService) : IUsuarioService
{
    public async Task<IEnumerable<Usuario>> ObterUsuariosAsync(CancellationToken cancellationToken)
        => await usuarioRepository.ObterUsuariosAsync(cancellationToken);

    public async Task<Usuario?> ObterUsuarioPorIdAsync(Guid id, CancellationToken cancellationToken)
        => !string.IsNullOrEmpty(id.ToString()) ? await usuarioRepository.ObterUsuarioPorIdAsync(id, cancellationToken) : null;

    public async Task<Usuario?> ObterUsuarioPorEmailAsync(string email, CancellationToken cancellationToken)
        => !string.IsNullOrEmpty(email) ? await usuarioRepository.ObterUsuarioPorEmailAsync(email, cancellationToken) : null;

    public async Task<Usuario> CriarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        Usuario usuarioCriado = await usuarioRepository.CriarUsuarioAsync(usuario, cancellationToken);
        
        await sqsService.PublishUserCreatedAsync(new UserCreatedEvent()
        {
            UsuarioId = usuarioCriado.Id,
            Nome = usuarioCriado.Nome,
        }, cancellationToken);

        return usuarioCriado;
    }

    public async Task<Usuario?> EditarUsuarioAsync(Guid id, Usuario usuario, CancellationToken cancellationToken)
        => await usuarioRepository.EditarUsuarioAsync(id, usuario, cancellationToken);

    public async Task DeletarUsuarioAsync(Guid id, CancellationToken cancellationToken)
        => await usuarioRepository.DeletarUsuarioAsync(id, cancellationToken);
}