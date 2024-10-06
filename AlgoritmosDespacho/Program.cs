
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
            List<ProcessModel> Data = obtainData.GetData() ?? [];

            FIFO fifo = new();
            fifo.LoadProcesos(Data);
            fifo.Run();

            SFJ sjf = new();
            sjf.LoadProcesos(Data);
            sjf.Run();

            Prioridad prioridad = new();
            prioridad.LoadProcesos(Data);
            prioridad.Run();

            RoundRobinFIFO roundRobinFIFO = new();

            List<RoundRobinProcessModel> DataRoundRobin = obtainData.GetData().Cast<RoundRobinProcessModel>().ToList() ?? new List<RoundRobinProcessModel>();
            roundRobinFIFO.LoadProcesos(DataRoundRobin);
            roundRobinFIFO.Run();

        }
    }
}