using Microsoft.AspNetCore.Mvc;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;
using WEB_253504_Kolesnikov.UI.Services.GenreService;
using WEB_253504_Kolesnikov.UI.Services.MovieService;

namespace WEB_253504_Kolesnikov.UI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        public MovieController(IMovieService movieService, IGenreService genreService) 
        {
            _movieService = movieService;
            _genreService = genreService;
        }
        public async Task <IActionResult> Index(string? genre, int? page)
        {
            int pageNumber = page ?? 1;
            //if(genre == "all")
            //{
            //    var movieResponse = await 
            //}
            var movieResponse = await _movieService.GetMovieListAsync(genre, pageNumber);
            var genres = await _genreService.GetGenreListAsync();

            if (!movieResponse.Successfull)
                return NotFound(movieResponse.ErrorMessage);
            var currentGenre = genre != "all" ? genres.Data?.Find(g => g.NormalizedName!.Equals(genre))?.Name : "Все";
            ViewData["currentGenre"] = currentGenre;
            Console.WriteLine($"Gerne: {genre}, {currentGenre}");
            ViewData["genres"] = _genreService.GetGenreListAsync().Result.Data;
            ViewData["totalPages"] = movieResponse.Data!.TotalPages;

            return View(new ProductListModel<Movie> { Items = movieResponse.Data.Items, CurrentPage = 1, TotalPages = 1 });
        }
    }
}
