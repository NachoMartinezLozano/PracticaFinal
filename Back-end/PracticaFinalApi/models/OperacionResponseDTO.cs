namespace PracticaFinalApi.Models
{
    public class OperacionResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFinal { get; set; }
        public List<string> NombresEquipos { get; set; } = new List<string>(); // Lista de nombres de los equipos asociados
    }
}