namespace PracticaFinalApi.Models
{
    public class OperacionItem
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public required string FechaInicio { get; set; }
        public required string FechaFinal { get; set; } 
        public virtual List<EquipoItem>? Equipos { get; set; } // Relación con los equipos que pertenecen a la operación
    }
}