using System.Collections.Generic;
using System.Linq;
using Taller1.FIFO.Model;
using Taller1.Model;
using OxyPlot;

namespace Taller1.FIFO
{
    public class FIFO
    {
        private List<ProcessModel> Procesos { get; set; } = new List<ProcessModel>();
        private int Tiempo { get; set; } = 0;
        private double PromedioTiempoEspera { get; set; } = 0;
        private double PromedioTiempoSistema { get; set; } = 0;

        public void AddProceso(string proceso, int rafaga, int llegada)
        {
            Procesos.Add(new ProcessModel(proceso, rafaga, llegada));
        }

        public void LoadProcesos(List<ProcessModel> procesos)
        {
            foreach (var proceso in procesos)
            {
                AddProceso(proceso.Proceso, proceso.Rafaga, proceso.Llegada);
            }
        }

        public List<ProcessModel> GetProcesos()
        {
            return Procesos;
        }

        public void Run()
        {
            foreach (var proceso in Procesos)
            {
                if (proceso.Llegada <= Tiempo && !proceso.Ejecutado)
                {
                    proceso.Comienzo = Tiempo;
                    proceso.Finalizacion = Tiempo + proceso.Rafaga;
                    proceso.Ejecutado = true;
                    Tiempo += proceso.Rafaga;
                }
                else if (!proceso.Ejecutado)
                {
                    Tiempo++;
                }
            }
        }

        public void PrintModels()
        {
            Run();
            foreach (var proceso in Procesos)
            {
                System.Console.WriteLine($"Proceso: {proceso.Proceso}, Rafaga: {proceso.Rafaga}, Llegada: {proceso.Llegada}, Comienzo: {proceso.Comienzo}, Finalizacion: {proceso.Finalizacion}");
            }
        }

        private void CalcularTiempos()
        {
            foreach (var proceso in Procesos)
            {
                proceso.TiempoEspera = proceso.Comienzo - proceso.Llegada;
                proceso.TiempoSistema = proceso.Finalizacion - proceso.Llegada;
            }
            PromedioTiempoEspera = Procesos.Average(proceso => proceso.TiempoEspera);
            PromedioTiempoSistema = Procesos.Average(proceso => proceso.TiempoSistema);
        }

        public void PrintTiempos()
        {
            CalcularTiempos();
            foreach (var proceso in Procesos)
            {
                System.Console.WriteLine($"Proceso: {proceso.Proceso}, Tiempo de espera: {proceso.TiempoEspera}, Tiempo de sistema: {proceso.TiempoSistema}");
            }
            System.Console.WriteLine($"Promedio de tiempo de espera: {PromedioTiempoEspera:F2}");
            System.Console.WriteLine($"Promedio de tiempo de sistema: {PromedioTiempoSistema:F2}");
        }

        public PlotModel GeneratePlotModel()
        {
            var plotModelGenerator = new PlotModelGenerator();
            return plotModelGenerator.CreatePlotModel(Procesos, Tiempo);
        }
    }
}