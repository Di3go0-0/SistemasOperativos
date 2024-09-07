namespace SoForm.Models
{
    public class ProcessDbModel
    {
        public int? Id { get; set; } // Clave primaria, opcional
        public string Proceso { get; set; } = "";
        public int Rafaga { get; set; }
        public int Llegada { get; set; }
        public int Prioridad { get; set; }
    }
}