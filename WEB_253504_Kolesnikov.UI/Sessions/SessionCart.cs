using System.Text.Json;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;

namespace WEB_253504_Kolesnikov.UI.Sessions
{
    public class SessionCart : Cart
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessionCart(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            LoadCartFromSession();
        }

        private void LoadCartFromSession()
        {
            var session = _contextAccessor.HttpContext.Session;
            var cartJson = session.GetString("CartSession");
            if (cartJson != null)
            {
                var items = JsonSerializer.Deserialize<Dictionary<int, CartItem>>(cartJson);
                if (items != null) 
                { 
                    CartItems = items;
                }
            }
        }

        private void SaveCartToSession()
        {
            var session = _contextAccessor.HttpContext.Session;
            var cartJson = JsonSerializer.Serialize(CartItems);
            session.SetString("CartSession", cartJson);
        }

        public override void AddToCart(Movie movie)
        {
            base.AddToCart(movie);
            SaveCartToSession();
        }

        public override void RemoveItem(int id)
        {
            base.RemoveItem(id);
            SaveCartToSession();
        }

        public override void ClearAll()
        {
            base.ClearAll();
            SaveCartToSession();
        }
    }
}
