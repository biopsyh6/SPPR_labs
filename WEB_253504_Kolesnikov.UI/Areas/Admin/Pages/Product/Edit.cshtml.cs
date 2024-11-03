using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.UI.Services.ApiGenreService;
using WEB_253504_Kolesnikov.UI.Services.ApiMovieService;

namespace WEB_253504_Kolesnikov.UI.Areas.Admin.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public EditModel(IMovieService movieService, IGenreService genreService)
        {
            _genreService = genreService;
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public IEnumerable<Genre> Genres { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> GenreItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseDataMovie = await _movieService.GetMovieByIdAsync(id.Value);
            var movie = responseDataMovie.Data;
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;

            var responseDataGenre = await _genreService.GetGenreListAsync();
            Genres = responseDataGenre.Data!;
            GenreItems = Genres.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = g.Id == Movie.Genre!.Id ? true : false
            });

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error. Try again.");
                return Page();
            }

            try
            {
                var responseData = await _movieService.UpdateMovieAsync(Movie.Id, Movie, Image);
            }
            catch
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
