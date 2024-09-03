using System.Collections.Generic;
using System.Linq;
using System;
using Taller.Model;
using OxyPlot;
using Taller.Helpers;

namespace Taller.FIFO
{
    public class FIFO
    {
        private List<ProcessModel> Procesos { get; set; } = new List<ProcessModel>();
        private int Tiempo { get; set; } = 0;
        private double PromedioTiempoEspera { get; set; } = 0;
        private double PromedioTiempoSistema { get; set; } = 0;

        private void AddProceso(string proceso, int rafaga, int llegada)
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

        public void RunProcess()
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
        
        private PlotModel GeneratePlotModel()
        {
            var plotModelGenerator = new PlotModelGenerator();
            return plotModelGenerator.CreatePlotFIFOModel(Procesos, Tiempo);
        }
        private void CreateIMG()
        {
            var plotModel = this.GeneratePlotModel();
            var imageGenerator = new ImageGenerator();
            imageGenerator.GenerateImage(plotModel, Procesos, PromedioTiempoEspera, PromedioTiempoSistema, "IMG/fifo"); 
        }
        public void Run()
        {
            this.RunProcess();
            this.CalcularTiempos();
            this.CreateIMG();
        }

    }
}