using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_253504_Kolesnikov.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; } //Name
        public string? Description { get; set; }
        public Genre? Genre { get; set; } //Category
        public int Duration { get; set; }
        public string? ImagePath { get; set; }
    }
}
