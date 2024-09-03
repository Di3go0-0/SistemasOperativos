using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxyPlot;
using Taller.Model;
using Taller.Helpers;

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
            var plotModel = GeneratePlotModel();
            var imgGenerator = new ImageGenerator();
            imgGenerator.GenerateImage(plotModel, Procesos, PromedioTiempoEspera, PromedioTiempoSistema, "IMG/sjf");
        }
        
        public void Run()
        {
            this.RunProcess();
            this.CalcularTiempos();
            this.CreateIMG();
        }

    }
}