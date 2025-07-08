using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaFinalApi.Models;
namespace PracticaFinalApi.Controllers
{
    [Route("api/Equipo")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EquipoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipoItem>>> GetEquipos()
        {
            return await _context.Equipos
                 //.Include(e => e.Operacion) // Incluye la operación relacionada con el equipo
                 .Include(e => e.Agentes) // Incluye los agentes relacionados con el equipo
                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoItem>> GetEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound($"Equipo con ID {id} no encontrado.");
            }

            return equipo;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<EquipoItem>>> PostEquipo(EquipoItemDTO equipo)
        {
            if (equipo == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
            }

            var newEq = EquipoDTOToEquipo(equipo);

            _context.Equipos.Add(newEq);
            await _context.SaveChangesAsync();

            return await GetEquipos();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<EquipoItem>>> PutEquipo(int id, EquipoItem equipo)
        {
            if (id != equipo.Id)
            {
                return BadRequest("El ID del equipo no coincide con el ID en la URL.");
            }

            var newEquipo = await _context.Equipos.FindAsync(id);
            if (newEquipo == null)
            {
                return NotFound();
            }

            newEquipo.Id = equipo.Id;
            newEquipo.Nombre = equipo.Nombre;
            newEquipo.Especialidad = equipo.Especialidad;
            newEquipo.OperacionId = equipo.OperacionId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EquipoExists(id))
            {
                return NotFound();
            }

            return await GetEquipos();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }

        public static EquipoItem EquipoDTOToEquipo(EquipoItemDTO eq)
        {
            return new EquipoItem
            {
                Nombre = eq.Nombre,
                Especialidad = eq.Especialidad,
                OperacionId = eq.OperacionId
            };
        }
    }
}