using BlazorAppAlexis.Shared;
using BlazorAppAlexis.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppAlexis.Server.Controllers;

// MetasController.cs
[Route("api/[controller]")]
[ApiController]
public class MetasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MetasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MetaDto>>> GetMetas()
    {
        try
        {
            // Obtener la lista de entidades Meta de la base de datos, incluyendo las tareas relacionadas.
            var metas = await _context.Metas.Include(m => m.Tareas).ToListAsync();

            // Transformar cada entidad Meta en un MetaDto y devolver la lista de MetaDto.
            var metaDtos = metas.Select(m => new MetaDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                FechaCreada = m.FechaCreada,
                PorcentajeCumplimiento = m.PorcentajeCumplimiento,
                Tareas = m.Tareas.Select(t => new TareaDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    FechaCreada = t.FechaCreada,
                    Estado = t.Estado,
                    EsImportante = t.EsImportante,
                    MetaId = t.MetaId
                }).ToList()
            }).ToList();

            return Ok(metaDtos);
        }
        catch (Exception ex)
        {
            // Manejar errores inesperados
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MetaDto>> GetMeta(int id)
    {
        // Obtener la entidad Meta junto con sus tareas relacionadas.
        var meta = await _context.Metas.Include(m => m.Tareas).FirstOrDefaultAsync(m => m.Id == id);

        // Si no se encuentra la meta, retornar 404 Not Found.
        if (meta == null)
        {
            return NotFound();
        }

        // Convertir la entidad Meta a un MetaDto.
        var metaDto = new MetaDto
        {
            Id = meta.Id,
            Nombre = meta.Nombre,
            FechaCreada = meta.FechaCreada,
            PorcentajeCumplimiento = meta.PorcentajeCumplimiento,
            Tareas = meta.Tareas.Select(t => new TareaDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                FechaCreada = t.FechaCreada,
                Estado = t.Estado,
                EsImportante = t.EsImportante,
                MetaId = t.MetaId
                
            }).ToList()
        };

        // Retornar el MetaDto con un código de estado 200 OK.
        return Ok(metaDto);
    }

    [HttpPost]
    public async Task<ActionResult<MetaDto>> CreateMeta(MetaDto metaDto)
    {
        var meta = new Meta
        {
            Nombre = metaDto.Nombre,
            FechaCreada = DateTime.Now,
            PorcentajeCumplimiento = metaDto.PorcentajeCumplimiento,
            TareasCompletadas = metaDto.TareasCompletadas
        };

        _context.Metas.Add(meta);
        await _context.SaveChangesAsync();

  
        metaDto.Id = meta.Id;

        return CreatedAtAction(nameof(GetMeta), new { id = meta.Id }, metaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeta(int id, MetaDto metaDto)
    {
        if (id != metaDto.Id)
        {
            return BadRequest();
        }

        var meta = await _context.Metas.Include(m => m.Tareas).FirstOrDefaultAsync(m => m.Id == id);

        if (meta == null)
        {
            return NotFound();
        }

        // Actualizar las propiedades de Meta
        meta.Nombre = metaDto.Nombre;
        meta.PorcentajeCumplimiento = metaDto.PorcentajeCumplimiento;

        // Actualizar tareas
        var existingTaskIds = meta.Tareas.Select(t => t.Id).ToList();
        var updatedTaskIds = metaDto.Tareas.Select(t => t.Id).ToList();

        // Eliminar tareas que ya no están en MetaDto
        var tasksToRemove = meta.Tareas.Where(t => !updatedTaskIds.Contains(t.Id)).ToList();
        foreach (var task in tasksToRemove)
        {
            _context.Tareas.Remove(task);
        }

        // Actualizar o agregar tareas
        foreach (var taskDto in metaDto.Tareas)
        {
            var task = meta.Tareas.FirstOrDefault(t => t.Id == taskDto.Id);
            if (task != null)
            {
                // Actualizar tarea existente
                task.Nombre = taskDto.Nombre;
                task.FechaCreada = taskDto.FechaCreada;
                task.Estado = taskDto.Estado;
                task.EsImportante = taskDto.EsImportante;
            }
            else
            {
                // Agregar nueva tarea
                meta.Tareas.Add(new Tarea
                {
                    Nombre = taskDto.Nombre,
                    FechaCreada = taskDto.FechaCreada,
                    Estado = taskDto.Estado,
                    EsImportante = taskDto.EsImportante,
                    MetaId = meta.Id // Establecer la relación con la meta
                });
            }
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!_context.Metas.Any(e => e.Id == id))
        {
            return NotFound();
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeta(int id)
    {
        var meta = await _context.Metas.FindAsync(id);
        if (meta == null)
        {
            return NotFound();
        }

        _context.Metas.Remove(meta);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MetaExists(int id)
    {
        return _context.Metas.Any(e => e.Id == id);
    }
}
