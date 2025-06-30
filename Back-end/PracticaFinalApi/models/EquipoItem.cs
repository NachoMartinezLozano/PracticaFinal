namespace PracticaFinalApi.Models
{
    public class EquipoItem
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } 
        public string? Especialidad { get; set; }
        public int OperacionId { get; set; }
    }
}