namespace Taller1.FIFO.Model
{
    public class FifoModel
    {
        public string Proceso { get; set; }
        public int Rafaga { get; set; }
        public int Llegada { get; set; }

        public FifoModel(string proceso, int rafaga, int llegada)
        {
            Proceso = proceso;
            Rafaga = rafaga;
            Llegada = llegada;
        }
    }
}