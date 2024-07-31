using BlazorAppAlexis.Shared;
using BlazorAppAlexis.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppAlexis.Server.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TareaDto>>> GetAllTareas()
        {
            var tareas = await _context.Tareas
                .Select(t => new TareaDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    FechaCreada = t.FechaCreada,
                    Estado = t.Estado,
                    EsImportante = t.EsImportante,
                    MetaId = t.MetaId
                })
                .ToListAsync();

            return Ok(tareas);
        }

        [HttpGet("meta/{metaId}")]
        public async Task<ActionResult<IEnumerable<TareaDto>>> GetTareasByMetaId(int metaId)
        {
            var tareas = await _context.Tareas
                .Where(t => t.MetaId == metaId)
                .Select(t => new TareaDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    FechaCreada = t.FechaCreada,
                    Estado = t.Estado,
                    EsImportante = t.EsImportante,
                    MetaId = t.MetaId
                })
                .ToListAsync();

            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            var tareaDto = new TareaDto
            {
                Id = tarea.Id,
                Nombre = tarea.Nombre,
                FechaCreada = tarea.FechaCreada,
                Estado = tarea.Estado,
                EsImportante = tarea.EsImportante,
                MetaId = tarea.MetaId
            };

            return Ok(tareaDto);
        }

        [HttpPost]
        public async Task<ActionResult<TareaDto>> CreateTarea(TareaDto tareaDto)
        {
            var tarea = new Tarea
            {
                Nombre = tareaDto.Nombre,
                FechaCreada = tareaDto.FechaCreada,
                Estado = tareaDto.Estado,
                EsImportante = tareaDto.EsImportante,
                MetaId = tareaDto.MetaId
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            tareaDto.Id = tarea.Id;

            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tareaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarea(int id, TareaDto tareaDto)
        {
            if (id != tareaDto.Id)
            {
                return BadRequest();
            }

            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            tarea.Nombre = tareaDto.Nombre;
            tarea.FechaCreada = tareaDto.FechaCreada;
            tarea.Estado = tareaDto.Estado;
            tarea.EsImportante = tareaDto.EsImportante;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_context.Tareas.Any(e => e.Id == id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            tarea.Estado = "Completada"; 
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }