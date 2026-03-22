using FiapCloudGames.Users.Domain.Entities;
using FiapCloudGames.Users.Domain.Messaging.Sns;
using FiapCloudGames.Users.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Users.Infrastructure.Seed;

[ExcludeFromCodeCoverage]
public sealed class DataSeederHostedService(
    ILogger<DataSeederHostedService> logger,
    IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        ISnsService sqsService = scope.ServiceProvider.GetRequiredService<ISnsService>();

        if (await context.Usuarios.AnyAsync(cancellationToken))
            return;

        IEnumerable<Usuario> usuarios = UsuarioSeed.GetUsuarios();

        await context.Usuarios.AddRangeAsync(usuarios, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        foreach (var usuario in usuarios)
            await sqsService.PublishUserCreatedAsync(new() { UsuarioId = usuario.Id, Nome = usuario.Nome }, cancellationToken);

        logger.LogInformation("Dados iniciais populados com sucesso!");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}