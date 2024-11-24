using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.API.Services.MovieService;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;

namespace WEB_253504_Kolesnikov.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<ResponseData<ProductListModel<Movie>>>> GetMovies(string? category, int pageNo = 1, int pageSize = 3)
        {
            var response = await _movieService.GetMovieListAsync(category, pageNo, pageSize);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<ProductListModel<Movie>>>(response);
        }

        // GET: api/Movies/5
        [HttpGet("{id:int}")]
        //[Authorize(Policy = "admin")]
        public async Task<ActionResult<ResponseData<Movie>>> GetMovie(int id)
        {
            var response = await _movieService.GetMovieByIdAsync(id);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<Movie>>(response);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult<ResponseData<bool>>> PutMovie(int id, Movie movie)
        {
            var response = await _movieService.UpdateMovieAsync(id, movie);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<bool>>(response);
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "admin")]
        //[Authorize]
        public async Task<ActionResult<ResponseData<int>>> PostMovie(Movie movie)
        {
            await Console.Out.WriteLineAsync(Request.Headers.ToString());
            var response = await _movieService.CreateMovieAsync(movie);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<int>>(response);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult<bool>> DeleteMovie(int id)
        {
            var response = await _movieService.DeleteMovieAsync(id);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<bool>(response.Data);
        }

        //private bool MovieExists(int id)
        //{
        //    return _context.Movies.Any(e => e.Id == id);
        //}
    }
}
