﻿using WEB_253504_Kolesnikov.UI.Services.GenreService;
using WEB_253504_Kolesnikov.UI.Services.MovieService;

namespace WEB_253504_Kolesnikov.UI.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<Services.GenreService.IGenreService, MemoryGenreService>();
            builder.Services.AddScoped<Services.MovieService.IMovieService, MemoryMovieService>();
        }
    }
}
