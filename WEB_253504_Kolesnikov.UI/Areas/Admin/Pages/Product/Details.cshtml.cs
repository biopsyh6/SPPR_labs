using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.UI.Services.ApiMovieService;

namespace WEB_253504_Kolesnikov.UI.Areas.Admin.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DetailsModel(IMovieService movieSevice)
        {
            _movieService = movieSevice;
        }

        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseData = await _movieService.GetMovieByIdAsync(id.Value);
            if (!responseData.Successfull)
            {
                return NotFound();
            }
            else
            {
                Movie = responseData.Data!;
            }
            return Page();
        }
    }
}
