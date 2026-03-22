using FiapCloudGames.Users.Application.Dtos;
using FiapCloudGames.Users.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Users.Api.Examples;

[ExcludeFromCodeCoverage]
public sealed class CriarUsuarioRequestExample : IExamplesProvider<UsuarioDto>
{
    public UsuarioDto GetExamples() => new(
        Nome: "Jose Teste 1",
        Email: "jose.teste1@outlook.com",
        Senha: "Teste1@1234",
        Role: ERole.Admin
    );
}