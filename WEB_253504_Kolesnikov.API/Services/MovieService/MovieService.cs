using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;

namespace WEB_253504_Kolesnikov.API.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly int _maxPageSize = 20;
        private readonly AppDbContext _appDbContext;

        public MovieService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResponseData<int>> CreateMovieAsync(Movie product)
        {
            var movie = await _appDbContext.Movies.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return ResponseData<int>.Success(movie.Entity.Id);
        }

        public async Task<ResponseData<bool>> DeleteMovieAsync(int id)
        {
            if (id < 0)
            {
                return ResponseData<bool>.Error("No Such Id");
            }
            await _appDbContext.Movies.Where(m => m.Id == id).ExecuteDeleteAsync();
            return ResponseData<bool>.Success(true);
        }

        public async Task<ResponseData<Movie>> GetMovieByIdAsync(int id)
        {
            if (id < 0)
            {
                return ResponseData<Movie>.Error("No Such Id");
            }
            var movie = await _appDbContext.Movies
                .AsNoTracking()
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            return ResponseData<Movie>.Success(movie!);
        }

        public async Task<ResponseData<ProductListModel<Movie>>> GetMovieListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if(pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }

            //var query = _appDbContext.Movies.AsQueryable();
            var query = _appDbContext.Movies.Include(m => m.Genre).AsQueryable();
            var dataList = new ProductListModel<Movie>();

            categoryNormalizedName = categoryNormalizedName == "all" ? null : categoryNormalizedName;
            query = query.Where(m => categoryNormalizedName == null || m.Genre.NormalizedName.Equals(categoryNormalizedName));

            //count of elements in list
            var itemsCount = await query.CountAsync();
            if(itemsCount == 0) 
            {
                return ResponseData<ProductListModel<Movie>>.Success(dataList);
            }

            //count of pages
            int totalPages = (int)Math.Ceiling(itemsCount / (double)pageSize);

            if(pageNo > totalPages)
            {
                return ResponseData<ProductListModel<Movie>>.Error("No Such Page");
            }

            dataList.Items = await query
                .OrderBy(m => m.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;

            return ResponseData<ProductListModel<Movie>>.Success(dataList);
        }

        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<bool>> UpdateMovieAsync(int id, Movie product)
        {
            if (id < 0 || id != product.Id)
            {
                return ResponseData<bool>.Error("Invalid Movie Id");
            }

            var existingMovie = await _appDbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if(existingMovie == null)
            {
                return ResponseData<bool>.Error("Movie Not Found");
            }

            if (product.ImagePath == null)
            {
                product.ImagePath = existingMovie.ImagePath;
            }

            existingMovie.Title = product.Title;
            existingMovie.Description = product.Description;
            existingMovie.Duration = product.Duration;
            existingMovie.ImagePath = product.ImagePath;
            
            if(product.Genre != null)
            {
                var genre = await _appDbContext.Genres.FindAsync(product.Genre.Id);
                if(genre != null)
                {
                    existingMovie.Genre = genre;
                }
            }

            await _appDbContext.SaveChangesAsync();
            
            return ResponseData<bool>.Success(true);
        }
    }
}
