namespace BlazorAppAlexis.Shared.Dtos;

public class MetaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreada { get; set; }
    public double PorcentajeCumplimiento { get; set; }
    
    public int TareasCompletadas { get; set; }
    public List<TareaDto> Tareas { get; set; }
}