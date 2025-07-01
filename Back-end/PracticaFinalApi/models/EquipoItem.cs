namespace PracticaFinalApi.Models
{
    public class EquipoItem
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public int OperacionId { get; set; }
        public virtual OperacionItem? Operacion { get; set; } // Relación con la operación a la que pertenece el equipo
        public virtual List<AgenteItem>? Agentes { get; set; } // Relación con los agentes que pertenecen al equipo
    }
}