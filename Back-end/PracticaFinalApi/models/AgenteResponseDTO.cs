namespace PracticaFinalApi.Models
{
    public class AgenteResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Rango { get; set; }
        public bool Activo { get; set; }
        public int? EquipoId { get; set; }
        public string? EquipoNombre { get; set; } // Nuevo campo para el nombre del equipo
    }
}