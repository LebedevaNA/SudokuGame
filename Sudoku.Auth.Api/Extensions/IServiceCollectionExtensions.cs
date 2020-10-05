using Microsoft.Extensions.DependencyInjection;
using Sudoku.Application.Services;

namespace Sudoku.Auth.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddCustomServiecs(this IServiceCollection services)
        {
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IJwtService, JwtService>();
            return services;
        }
    }
}