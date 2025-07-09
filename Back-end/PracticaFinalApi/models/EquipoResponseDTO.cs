namespace PracticaFinalApi.Models
{
    public class EquipoResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public int? OperacionId { get; set; }
        public string? OperacionNombre { get; set; }
        public List<AgenteItem>? Agentes { get; set; }
    }
}