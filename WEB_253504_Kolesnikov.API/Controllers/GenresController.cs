﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.API.Data;
using WEB_253504_Kolesnikov.API.Services.GenreService;
using WEB_253504_Kolesnikov.Domain.Entities;

namespace WEB_253504_Kolesnikov.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IQueryable<Genre>>> GetGenres()
        {
            var response = await _genreService.GetGenreListAsync();
            return new ActionResult<IQueryable<Genre>>(response.Data!.AsQueryable());
        }

        // GET: api/Genres/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Genre>> GetGenre(int id)
        //{
        //    var genre = await _context.Genres.FindAsync(id);

        //    if (genre == null)
        //    {
        //        return NotFound();
        //    }

        //    return genre;
        //}

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGenre(int id, Genre genre)
        //{
        //    if (id != genre.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(genre).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GenreExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        //{
        //    _context.Genres.Add(genre);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
        //}

        // DELETE: api/Genres/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGenre(int id)
        //{
        //    var genre = await _context.Genres.FindAsync(id);
        //    if (genre == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Genres.Remove(genre);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool GenreExists(int id)
        //{
        //    return _context.Genres.Any(e => e.Id == id);
        //}
    }
}
