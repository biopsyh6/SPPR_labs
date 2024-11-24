using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_253504_Kolesnikov.Domain.Entities;

namespace WEB_253504_Kolesnikov.Domain.Models
{
    public class Cart
    {
        /// <summary>
        /// Список объектов в корзине
        /// key - идентификатор объекта
        /// </summary>
        public Dictionary<int, CartItem> CartItems { get; set; } = new();

        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="dish">Добавляемый объект</param>
        public virtual void AddToCart(Movie movie)
        {
            if (movie == null)
            {
                return;
            }

            if (CartItems.TryGetValue(movie.Id, out CartItem? value))
            {
                value.Count++;
            }
            else
            {
                CartItems[movie.Id] = new CartItem { Movie = movie, Count = 1 };
            }
        }

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id"> id удаляемого объекта</param>
        public virtual void RemoveItem(int id)
        {
            CartItems.Remove(id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }

        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count { get => CartItems.Sum(item => item.Value.Count); }

        /// <summary>
        /// Общая длительность
        /// </summary>
        public int TotalDuration
        {
            get =>
            CartItems.Sum(item => item.Value.Movie!.Duration * item.Value.Count);
        }
    }
}
