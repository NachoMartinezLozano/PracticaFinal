namespace PracticaFinalApi.Models
{
    public class OperacionItem
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; } 
        public virtual List<EquipoItem>? Equipos { get; set; } // Relación con los equipos que pertenecen a la operación
    }
}