using Microsoft.AspNetCore.Mvc;

namespace WEB_253504_Kolesnikov.UI.Views.Shared.Components.CartSummary
{
    public class CartSummary : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
