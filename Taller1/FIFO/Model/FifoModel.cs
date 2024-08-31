namespace Taller1.FIFO.Model
{
    public class FifoModel
    {
        public string Proceso { get; set; }
        public int Rafaga { get; set; }
        public int Llegada { get; set; }
        public int Comienzo { get; set; }
        public int Finalizacion { get; set; }
        public bool Ejecutado { get; set; }
        public int TiempoEspera { get; set; }
        public int TiempoSistema { get; set; }

        public FifoModel(string proceso, int rafaga, int llegada)
        {
            Proceso = proceso;
            Rafaga = rafaga;
            Llegada = llegada;
        }
    }
}