using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller.Model;
using OxyPlot;
using Taller.Helpers;

namespace Taller.FIFO
{
    public class RoundRobinFIFO
    {
        private List<RoundRobinProcessModel> Procesos { get; set; } = new List<RoundRobinProcessModel>();
        private int Tiempo { get; set; } = 0;
        private double PromedioTiempoEspera { get; set; } = 0;
        private double PromedioTiempoSistema { get; set; } = 0;
        private int Quantum { get; set; } = 1;

        private void AddProceso(string proceso, int rafaga, int llegada)
        {
            Procesos.Add(new RoundRobinProcessModel(proceso, rafaga, llegada));
        }
        public void LoadProcesos (List<RoundRobinProcessModel> procesos)
        {
            foreach (var proceso in procesos)
            {
                AddProceso(proceso.Proceso, proceso.Rafaga, proceso.Llegada);
            }
        }

        public List<RoundRobinProcessModel> GetProcesos()
        {
            return Procesos;
        }

        public void RunProcess()
        {
            // Ordenamos los procesos por el tiempo de llegada
            var procesosOrdenados = Procesos.OrderBy(p => p.Llegada).ToList();

            while (Procesos.Any(p => !p.Ejecutado))
            {
                foreach (var proceso in procesosOrdenados)
                {
                    // Si el proceso aún no ha llegado, el tiempo debe avanzar hasta su llegada
                    if (Tiempo < proceso.Llegada)
                    {
                        Tiempo = proceso.Llegada;
                    }

                    // Una vez que el proceso puede empezar, actualizamos sus tiempos
                    proceso.Comienzo.Add(Tiempo);
                    proceso.Finalizacion.Add(Tiempo + Quantum);
                    proceso.Ejecutado = proceso.Finalizacion.Last() >= proceso.Rafaga + proceso.Llegada;

                    // Avanzamos el tiempo según el quantum
                    Tiempo += Quantum;
                }
            }

            // Calcular los tiempos de espera y sistema una vez que todos los procesos han sido ejecutados
            CalcularTiempos();
        }
        private void CalcularTiempos(){
            foreach (var proceso in Procesos)
            {
                proceso.TiempoEspera = proceso.Comienzo.First() - proceso.Llegada;
                proceso.TiempoSistema = proceso.Finalizacion.Last() - proceso.Llegada;
                PromedioTiempoEspera += proceso.TiempoEspera;
                PromedioTiempoSistema += proceso.TiempoSistema;
            }

            PromedioTiempoEspera /= Procesos.Count;
            PromedioTiempoSistema /= Procesos.Count;
        }

        private PlotModel GeneratePlotModel()
        {
            var plotModelGenerator = new PlotModelRoundRobin();
            return plotModelGenerator.CreatePlotModel(Procesos, Tiempo);
        }
        public void CreateIMG(){
            var plotModel = GeneratePlotModel();
            var imageGenerator = new ImageRoundRobinGenerator();
            imageGenerator.CreateImage(plotModel, Procesos, PromedioTiempoEspera, PromedioTiempoSistema, "IMG/RoundRobinFIFO"); 
        }

        public void Run()
        {
            RunProcess();
            CreateIMG();
        }
    }
}