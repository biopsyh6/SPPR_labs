using WEB_253504_Kolesnikov.Domain.Models;
using WEB_253504_Kolesnikov.UI.Models;
using WEB_253504_Kolesnikov.UI.Services.ApiFileService;
using WEB_253504_Kolesnikov.UI.Services.ApiGenreService;
using WEB_253504_Kolesnikov.UI.Services.ApiMovieService;
using WEB_253504_Kolesnikov.UI.Services.Authentication;
using WEB_253504_Kolesnikov.UI.Services.Authorization;
using WEB_253504_Kolesnikov.UI.Services.GenreService;
using WEB_253504_Kolesnikov.UI.Services.MovieService;
using WEB_253504_Kolesnikov.UI.Sessions;

namespace WEB_253504_Kolesnikov.UI.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            //builder.Services.AddScoped<IGenreService, MemoryGenreService>();
            //builder.Services.AddScoped<IMovieService, MemoryMovieService>();

            builder.Services.AddScoped<IMovieService, ApiMovieService>();
            builder.Services.AddScoped<IGenreService, ApiGenreService>();
            builder.Services.AddScoped<IFileService, ApiFileService>();
            builder.Services.Configure<KeycloakData>(builder.Configuration.GetSection("Keycloak"));
            builder.Services.AddScoped<ITokenAccessor, KeycloakTokenAccessor>();
            builder.Services.AddScoped<IAuthService, KeycloakAuthService>();

            builder.Services.AddScoped<Cart, SessionCart>();
        }
    }
}
