namespace PracticaFinalApi.Models
{
    public class OperacionItem
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } 
        public string? Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; } 
    }
}