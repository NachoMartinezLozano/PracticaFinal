namespace PracticaFinalApi.Models
{
    public class AgenteItem
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } // "Compra" o "Venta"
        public string? Rango { get; set; }
        public bool Activo { get; set; }
        public int EquipoId { get; set; } // Relación con el usuario que realizó la operación
        //public virtual EquipoItem? Equipo { get; set; } // Relación con el equipo al que pertenece el agente
    }
}