
using Taller.Helpers;
using Taller.FIFO;
using Taller.SJF;
using Taller.Model;
using Taller.Prioridad;
namespace SO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ObtainData obtainData = new ObtainData(AlgorithmType.Fifo);
            List<ProcessModel> fifoData = obtainData.GetFifoData() ?? new List<ProcessModel>();

            FIFO fifo = new();
            fifo.LoadProcesos(fifoData);
            fifo.Run();

            SFJ sjf = new();
            sjf.LoadProcesos(fifoData);
            sjf.Run();

            Prioridad prioridad = new();
            prioridad.LoadProcesos(fifoData);
            prioridad.Run();

        }
    }
}