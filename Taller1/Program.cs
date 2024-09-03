
using Taller1.Helpers;
using Taller1.FIFO;
using Taller1.SJF;
using Taller1.Model;
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

        }
    }
}