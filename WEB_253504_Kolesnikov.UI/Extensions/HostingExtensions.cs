using WEB_253504_Kolesnikov.UI.Services.ApiGenreService;
using WEB_253504_Kolesnikov.UI.Services.ApiMovieService;
using WEB_253504_Kolesnikov.UI.Services.GenreService;
using WEB_253504_Kolesnikov.UI.Services.MovieService;

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
        }
    }
}
