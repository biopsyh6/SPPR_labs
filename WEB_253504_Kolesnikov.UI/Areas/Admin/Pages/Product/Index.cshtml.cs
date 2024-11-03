using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;
using WEB_253504_Kolesnikov.UI.Services.ApiMovieService;

namespace WEB_253504_Kolesnikov.UI.Areas.Admin.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;

        public IndexModel(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [BindProperty]
        public ResponseData<ProductListModel<Movie>> Movie { get;set; } = default!;

        public async Task OnGetAsync(int pageNo = 1)
        {
            Movie = await _movieService.GetMovieListAsync(null, pageNo);
            //var responseData = await _movieService.GetMovieListAsync("all", pageNo);
            //Movie = responseData.Data.Items;
        }
    }
}
