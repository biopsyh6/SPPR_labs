using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.UI.Models;

namespace WEB_253504_Kolesnikov.UI
{
    public class AppDbContextUI : DbContext
    {
        public AppDbContextUI(DbContextOptions<AppDbContextUI> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisterUserViewModel>().HasNoKey();
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<WEB_253504_Kolesnikov.UI.Models.RegisterUserViewModel> RegisterUserViewModel { get; set; } = default!;
        //public DbSet<RegisterUserViewModel> RegisterUsers { get; set; }
    }
}
