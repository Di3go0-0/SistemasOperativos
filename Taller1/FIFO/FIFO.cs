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
            // Ordenamos los procesos por el tiempo de llegada
            var procesosOrdenados = Procesos.OrderBy(p => p.Llegada).ToList();

            foreach (var proceso in procesosOrdenados)
            {
                // Si el proceso aún no ha llegado, el tiempo debe avanzar hasta su llegada
                if (Tiempo < proceso.Llegada)
                {
                    Tiempo = proceso.Llegada;
                }

                // Una vez que el proceso puede empezar, actualizamos sus tiempos
                proceso.Comienzo = Tiempo;
                proceso.Finalizacion = Tiempo + proceso.Rafaga;
                proceso.Ejecutado = true;

                // Avanzamos el tiempo según la ráfaga del proceso
                Tiempo += proceso.Rafaga;
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
        public PlotModel GeneratePlotModel()
        {
            var plotModelGenerator = new PlotModelGenerator();
            return plotModelGenerator.CreatePlotModel(Procesos, Tiempo);
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

    }
}