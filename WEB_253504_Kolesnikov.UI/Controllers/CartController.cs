using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_253504_Kolesnikov.Domain.Models;
using WEB_253504_Kolesnikov.UI.Services.ApiMovieService;

namespace WEB_253504_Kolesnikov.UI.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly Cart _cart;

        public CartController(IMovieService movieService, Cart cart)
        {
            _movieService = movieService;
            _cart = cart;
        }
        public IActionResult Index()
        {
            return View(_cart);
        }

        //[Authorize]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> Add(int id, string returnUrl)
        {
            var data = await _movieService.GetMovieByIdAsync(id);
            if (data.Successfull)
            {
                _cart.AddToCart(data.Data!);
            }

            return Redirect(returnUrl);

        }

        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> ClearCart(string returnUrl)
        {
            _cart.ClearAll();
            return Redirect(returnUrl);
        }

        [Authorize]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> RemoveFromCart(int id, string returnUrl)
        {
            _cart.RemoveItem(id);
            return Redirect(returnUrl);
        }
    }   
}
