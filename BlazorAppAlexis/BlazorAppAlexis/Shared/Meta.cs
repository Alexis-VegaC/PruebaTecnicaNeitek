namespace BlazorAppAlexis.Shared;

public class Meta
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreada { get; set; }
    public double PorcentajeCumplimiento { get; set; }
    
    public int TareasCompletadas { get; set; }
    public ICollection<Tarea> Tareas { get; set; }
}