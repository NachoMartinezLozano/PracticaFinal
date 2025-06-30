namespace PracticaFinalApi.Models
{
    public class Operacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // "Compra" o "Venta"
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; } // Relación con el usuario que realizó la operación
    }
}