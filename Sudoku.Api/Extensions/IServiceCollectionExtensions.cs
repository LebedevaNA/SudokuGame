using Microsoft.Extensions.DependencyInjection;
using Sudoku.Application.Services;

namespace Sudoku.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static  IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IJwtService, JwtService>();
            return services;
        }
    }
}