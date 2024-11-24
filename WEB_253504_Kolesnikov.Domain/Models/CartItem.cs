using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_253504_Kolesnikov.Domain.Entities;

namespace WEB_253504_Kolesnikov.Domain.Models
{
    public class CartItem
    {
        public Movie ?Movie { get; set; }
        public int Count { get; set; } = 0;
    }
}
