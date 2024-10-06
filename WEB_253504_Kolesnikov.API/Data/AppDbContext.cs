﻿using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.Domain.Entities;

namespace WEB_253504_Kolesnikov.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
