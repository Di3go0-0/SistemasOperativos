namespace SoForm.Models
{
    public class ProcessModel
    {
        public int Id { get; set; } 
        public string Proceso { get; set; } = "";
        public int Rafaga { get; set; }
        public int Llegada { get; set; }
        public int Prioridad { get; set; }
        public int Comienzo { get; set; }
        public int Finalizacion { get; set; }
        public bool Ejecutado { get; set; }
        public double TiempoEspera { get; set; }
        public double TiempoSistema { get; set; }

        
        public ProcessModel() {}

        public ProcessModel(int Id, string proceso, int rafaga, int llegada, int prioridad)
        {
            this.Id = Id;
            Proceso = proceso;
            Rafaga = rafaga;
            Llegada = llegada;
            Prioridad = prioridad;
        }

        public ProcessModel(string proceso, double TiempoEspera, double TiempoSistema)
        {
            Proceso = proceso;
            this.TiempoEspera = Convert.ToInt32(TiempoEspera);
            this.TiempoSistema = Convert.ToInt32(TiempoSistema);
        }
    }
}