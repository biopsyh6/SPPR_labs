using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.UI.Services.ApiGenreService;
using WEB_253504_Kolesnikov.UI.Services.ApiMovieService;

namespace WEB_253504_Kolesnikov.UI.Areas.Admin.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public CreateModel(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        public async Task<IActionResult> OnGet()
        {
            var responseData = await _genreService.GetGenreListAsync();
            Genres = responseData.Data!;
            GenreItems = Genres.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = false
            });
            return Page();
        }

        [BindProperty]
        public IEnumerable<Genre> Genres { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> GenreItems { get; set; }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error. Try again.");
                return Page();
            }
            
            await _movieService.CreateMovieAsync(Movie, Image);

            return RedirectToPage("./Index");
        }
    }
}
