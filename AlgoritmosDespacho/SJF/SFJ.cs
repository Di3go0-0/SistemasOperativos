using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxyPlot;
using Taller.Model;

namespace Taller.SJF
{
    public class SFJ
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
                this.AddProceso(proceso.Proceso, proceso.Rafaga, proceso.Llegada);
            }
        }

        public List<ProcessModel> GetProcesos()
        {
            return Procesos;
        }
        public void RunProcess()
        {
            var procesosOrdenados = Procesos.OrderBy(p => p.Rafaga).ToList();
            foreach (var proceso in procesosOrdenados)
            {
                if (Tiempo < proceso.Llegada)
                {
                    Tiempo = proceso.Llegada;
                }
                proceso.Comienzo = Tiempo;
                proceso.Finalizacion = Tiempo + proceso.Rafaga;
                proceso.Ejecutado = true;
                Tiempo += proceso.Rafaga;
            }
        }

        private void CalcularTiempos()
        {
            foreach (var proceso in Procesos)
            {
                proceso.TiempoEspera = proceso.Comienzo - proceso.Llegada;
                proceso.TiempoSistema = proceso.Finalizacion - proceso.Llegada;
                PromedioTiempoEspera += proceso.TiempoEspera;
                PromedioTiempoSistema += proceso.TiempoSistema;
            }

            PromedioTiempoEspera /= Procesos.Count;
            PromedioTiempoSistema /= Procesos.Count;
        }
        private PlotModel GeneratePlotModel()
        {
            var plotModelGenerator = new PlotModelGenerator();
            return plotModelGenerator.CreatePlotSJFModel(Procesos, Tiempo);
        }

        public void CreateIMG()
        {
            var plotModel = this.GeneratePlotModel();
            using (var stream = System.IO.File.Create("IMG/sjf_plot.png"))
            {
                var pngExporter = new OxyPlot.SkiaSharp.PngExporter { Width = 600, Height = 400 };
                pngExporter.Export(plotModel, stream);
            }
            Console.WriteLine("Gráfica generada y guardada como sjf_plot.png");

        }
        public void Run()
        {
            this.RunProcess();
            this.CalcularTiempos();
            this.CreateIMG();
        }

    }
}