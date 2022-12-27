using Application.Interfaces;
using Application.Services;
using Infra.Data.Interfaces;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCuting.Native_Injector;

public static class NativeInjector
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IAutenticacaoService, AutenticacaoService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}