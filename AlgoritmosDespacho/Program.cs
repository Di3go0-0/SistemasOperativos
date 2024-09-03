
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
            ObtainData obtainData = new();
            List<ProcessModel> fifoData = obtainData.GetFifoData() ?? [];

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