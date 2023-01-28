using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Services;
using FluentValidation;
using Infra.Data.Interfaces;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using static Application.Validators.UsuarioValidator;

namespace CrossCuting.Native_Injector;

public static class NativeInjector
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IAutenticacaoService, AutenticacaoService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IValidator<UsuarioCadastroRequest>, UsuarioCadastroValidator>();
        services.AddScoped<IValidator<UsuarioLoginRequest>, UsuarioLoginValidator>();
    }
}