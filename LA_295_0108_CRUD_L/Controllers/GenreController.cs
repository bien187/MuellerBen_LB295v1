using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LA_295_0108_CRUD_L.Model;
using LA_295_0108_CRUD_L.Context;

namespace LA_295_0108_CRUD_L.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Genre
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            var genreDTOs = genres.Select(g => new GenreDTO { GenreId = g.GenreId, Name = g.Name }).ToList();
            return genreDTOs;
        }

        // GET: api/Genre/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            var genreDTO = new GenreDTO { GenreId = genre.GenreId, Name = genre.Name };
            return genreDTO;
        }

        // PUT: api/Genre/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, GenreDTO genreDTO)
        {
            if (id != genreDTO.GenreId)
            {
                return BadRequest();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            genre.Name = genreDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genre
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genreDTO)
        {
            var genre = new Genre { Name = genreDTO.Name };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            genreDTO.GenreId = genre.GenreId;

            return CreatedAtAction(
                nameof(GetGenre),
                new { id = genre.GenreId },
                genreDTO
            );
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }
    }
}
