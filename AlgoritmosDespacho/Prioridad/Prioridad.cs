using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxyPlot;
using Taller1.Model;

namespace Taller1.Prioridad
{
    public class Prioridad
    {
        private List<ProcessModel> Procesos { get; set; } = new List<ProcessModel>();
        private int Tiempo { get; set; } = 0;
        private double PromedioTiempoEspera { get; set; } = 0;
        private double PromedioTiempoSistema { get; set; } = 0;

        private void AddProceso(string proceso, int rafaga, int llegada, int prioridad)
        {
            Procesos.Add(new ProcessModel(proceso, rafaga, llegada, prioridad));
        }
        public void LoadProcesos(List<ProcessModel> procesos)
        {
            foreach (var proceso in procesos)
            {
                AddProceso(proceso.Proceso, proceso.Rafaga, proceso.Llegada, proceso.Prioridad);
            }
        }
        public void RunProcess()
        {
            // Ordenamos los procesos primero por prioridad (menor prioridad numérica = mayor prioridad) 
            // y luego por el tiempo de llegada para que los procesos que lleguen antes se ejecuten primero si tienen la misma prioridad.
            var procesosOrdenados = Procesos.OrderBy(p => p.Prioridad).ThenBy(p => p.Llegada).ToList();

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

        private PlotModel GeneratePlotModel()
        {
            var plotModelGenerator = new PlotModelGenerator();
            return plotModelGenerator.CreatePlotPrioridadModel(Procesos, Tiempo);
        }
        public void CreateIMG()
        {
            var plotModel = this.GeneratePlotModel();
            using (var stream = System.IO.File.Create("IMG/prioridad_plot.png"))
            {
                var pngExporter = new OxyPlot.SkiaSharp.PngExporter { Width = 600, Height = 400 };
                pngExporter.Export(plotModel, stream);
            }
            Console.WriteLine("Gráfica generada y guardada como prioridad_plot.png");
        }
        public void Run()
        {
            this.RunProcess();
            this.CalcularTiempos();
            this.CreateIMG();
        }

    }
}