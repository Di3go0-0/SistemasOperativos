using System;
using System.Collections.Generic;
using System.Linq;
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
            var procesosPendientes = new List<ProcessModel>(Procesos);
            var procesosEjecutados = new List<ProcessModel>();

            while (procesosPendientes.Any())
            {
                // Filtramos procesos que ya han llegado y ordenamos por ráfaga
                var procesosDisponibles = procesosPendientes
                    .Where(p => p.Llegada <= Tiempo)
                    .OrderBy(p => p.Rafaga)
                    .ToList();

                if (!procesosDisponibles.Any())
                {
                    // Si no hay procesos disponibles, avanzamos el tiempo al siguiente proceso
                    Tiempo = procesosPendientes.Min(p => p.Llegada);
                    continue;
                }

                var procesoActual = procesosDisponibles.First();
                procesosPendientes.Remove(procesoActual);

                // Establecemos los tiempos de comienzo y finalización
                procesoActual.Comienzo = Tiempo;
                procesoActual.Finalizacion = Tiempo + procesoActual.Rafaga;
                procesoActual.Ejecutado = true;

                // Avanzamos el tiempo en función de la ráfaga del proceso actual
                Tiempo += procesoActual.Rafaga;
                procesosEjecutados.Add(procesoActual);
            }
            
            procesosEjecutados = procesosEjecutados.OrderBy(p => p.Proceso).ToList();


            Procesos = procesosEjecutados;
            
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
            imgGenerator.GenerateImage(plotModel, Procesos, PromedioTiempoEspera, PromedioTiempoSistema, "IMG/sfj");
        }

        public void Run()
        {
            this.RunProcess();
            this.CalcularTiempos();
            Console.WriteLine("SFJ");
            for (int i = 0; i < Procesos.Count; i++)
            {
                var proceso = Procesos[i];
                Console.WriteLine($"Proceso: {proceso.Proceso}, Tiempo de llegada: {proceso.Llegada}, Tiempo de finalización: {proceso.Finalizacion}, Tiempo de inicio: {proceso.Comienzo}, Tiempo de espera: {proceso.TiempoEspera}, Tiempo de sistema: {proceso.TiempoSistema}");
            }
            this.CreateIMG();
        }
    }
}
