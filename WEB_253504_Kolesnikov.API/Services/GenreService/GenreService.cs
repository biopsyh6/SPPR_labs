using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;

namespace WEB_253504_Kolesnikov.API.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _appDbContext;

        public GenreService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResponseData<List<Genre>>> GetGenreListAsync()
        {
            var genres = await _appDbContext.Genres.ToListAsync();
            return ResponseData<List<Genre>>.Success(genres);
        }
    }
}
