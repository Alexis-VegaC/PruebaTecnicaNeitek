namespace BlazorAppAlexis.Shared;

public class Tarea
{
    public int Id { get; set; }
    public int MetaId { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreada { get; set; }
    public string Estado { get; set; } // "Abierta", "Completada"
    public bool EsImportante { get; set; }
    public Meta Meta { get; set; }
}