namespace BlazorAppAlexis.Shared.Dtos;

public class TareaDto
{
    public int Id { get; set; }
    
    public int MetaId { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreada { get; set; }
    public string Estado { get; set; }
    public bool EsImportante { get; set; }
}