using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;

namespace WEB_253504_Kolesnikov.UI.Services.GenreService
{
    public class MemoryGenreService : IGenreService
    {
        public Task<ResponseData<List<Genre>>> GetGenreListAsync()
        {
            var genres = new List<Genre>
            {
                new Genre { Id=1, Name="Боевик", NormalizedName="action"},
                new Genre { Id=2, Name="Ужасы", NormalizedName="horror"},
                new Genre { Id=3, Name="Комедия", NormalizedName="comedy"},
                new Genre { Id=4, Name="Приключения", NormalizedName="adventure"},
                new Genre { Id=5, Name="Драма", NormalizedName="drama"},
                new Genre { Id=6, Name="Криминальный", NormalizedName="crime"},
                new Genre { Id=7, Name="Фэнтези", NormalizedName="fantasy"},
                new Genre { Id=8, Name="Романтический", NormalizedName="romance"},
                new Genre { Id=9, Name="Триллер", NormalizedName="thriller"},
                new Genre { Id=10, Name="Анимационный", NormalizedName="animation"},
                new Genre { Id=11, Name="Семейный", NormalizedName="family"},
                new Genre { Id=12, Name="Документальный", NormalizedName="documentary"},
                new Genre { Id=13, Name="Мюзикл", NormalizedName="musical"},
                new Genre { Id=14, Name="Научная фантастика", NormalizedName="sci-fi"},
                new Genre { Id=15, Name="Вестерн", NormalizedName="western"},
            };
            var result = ResponseData<List<Genre>>.Success(genres);
            return Task.FromResult(result);
        }
    }
}
