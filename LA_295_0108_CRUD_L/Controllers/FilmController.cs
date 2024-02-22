using LA_295_0108_CRUD_L.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LA_295_0108_CRUD_L.Context;
using Microsoft.AspNetCore.Authorization;

namespace LA_295_0108_CRUD_L.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FilmController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmDTO>>> GetFilme()
        {
            if (_context.Filme == null)
            {
                return NotFound();
            }
            return await _context.Filme.Select(x => FilmToFilmDTO(x)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilmById(int id)
        {
            var film = await _context.Filme.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        // PUT: api/Film/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, FilmDTO filmDTO)
        {
            if (id != filmDTO.FilmId)
            {
                return BadRequest();
            }
            var film = await _context.Filme.FindAsync(filmDTO.FilmId);
            if (film == null)
            {
                return NotFound();
            }
            film.Titel = filmDTO.Titel;
            film.Regisseur = filmDTO.Regisseur;
            film.Erscheinungsjahr = filmDTO.Erscheinungsjahr;

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Film
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            _context.Filme.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFilmById), new { id=film.FilmId}, film);
        }

        // DELETE: api/Film/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (_context.Filme == null)
            {
                return NotFound();
            }
            var film = await _context.Filme.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Filme.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(int id)
        {
            return (_context.Filme?.Any(e => e.FilmId == id)).GetValueOrDefault();
        }

        private static FilmDTO FilmToFilmDTO(Film film)
        {
            return new FilmDTO
            {
                FilmId = film.FilmId,
                Titel = film.Titel,
                Regisseur = film.Regisseur,
                Erscheinungsjahr = film.Erscheinungsjahr
            };
        }
    }
}
