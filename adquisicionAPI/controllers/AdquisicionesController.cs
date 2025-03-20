using Microsoft.AspNetCore.Mvc;
using adquisicionAPI.data;
using adquisicionAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace adquisicionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdquisicionesController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/Adquisiciones
        [HttpGet]
        public ActionResult<IEnumerable<Adquisicion>> GetAdquisiciones()
        {
            return _context.Adquisiciones.ToList();
        }

        // GET: api/Adquisiciones/5
        [HttpGet("{id}")]
        public ActionResult<Adquisicion> GetAdquisicion(int id)
        {
            var adquisicion = _context.Adquisiciones.Find(id);

            if (adquisicion == null)
            {
                return NotFound();
            }

            return adquisicion;
        }

        // POST: api/Adquisiciones
        [HttpPost]
        public ActionResult<Adquisicion> PostAdquisicion(Adquisicion adquisicion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            adquisicion.Activo = true;
            _context.Adquisiciones.Add(adquisicion);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Ocurrió un problema de concurrencia al guardar los cambios.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetAdquisicion), new { id = adquisicion.Id }, adquisicion);
        }

        // PUT: api/Adquisiciones/5
        [HttpPut("{id}")]
        public IActionResult PutAdquisicion(int id, Adquisicion adquisicion)
        {
            if (id != adquisicion.Id)
            {
                return BadRequest();
            }
            _context.Adquisiciones.Attach(adquisicion);
            _context.Entry(adquisicion).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Ocurrió un problema de concurrencia al guardar los cambios.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return NoContent();
        }

        [HttpPatch("{id}/desactivar")]
        public IActionResult DesactivarAdquisicion(int id)
        {
            var adquisicion = _context.Adquisiciones.Find(id);
            if (adquisicion == null)
            {
                return NotFound();
            }

            if (!adquisicion.Activo)
            {
                return BadRequest("La adquisición ya está desactivada.");
            }

            adquisicion.Activo = false;
            _context.Entry(adquisicion).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        // GET: api/Adquisiciones/5/Historial
        [HttpGet("{id}/Historial")]
        public ActionResult<IEnumerable<Historial>> GetHistorialAdquisicion(int id)
        {
            var adquisicion = _context.Adquisiciones.Find(id);
            if (adquisicion == null)
            {
                return NotFound("La adquisición no existe.");
            }

            var historial = _context.Historial
                .Where(ah => ah.IdAdquisicion == id)
                .OrderByDescending(ah => ah.FechaCambio)
                .ToList();

            if (historial == null || !historial.Any())
            {
                return NotFound("No se encontró historial para esta adquisición.");
            }

            return historial;
        }

    }
}