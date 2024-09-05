using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using Taller.Helpers;
using Taller.Model;

namespace Taller.Prioridad
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
            // Lista temporal para guardar los procesos no ejecutados
            List<ProcessModel> procesosNoEjecutados = new List<ProcessModel>(Procesos);

            // Mientras haya procesos por ejecutar
            while (procesosNoEjecutados.Count > 0)
            {
                // Filtrar los procesos que han llegado hasta el tiempo actual
                var procesosValidos = procesosNoEjecutados
                    .Where(p => p.Llegada <= Tiempo)
                    .OrderBy(p => p.Prioridad)  // Ordenar por prioridad
                    .ThenBy(p => p.Llegada)     // Si tienen la misma prioridad, ordenar por tiempo de llegada
                    .ToList();

                if (procesosValidos.Count == 0)
                {
                    // Si no hay procesos disponibles para ejecutar, avanzar el tiempo
                    Tiempo++;
                    continue;
                }

                // Seleccionar el proceso con mayor prioridad
                var procesoAEjecutar = procesosValidos.First();

                // Registrar el tiempo de comienzo y de finalización del proceso
                procesoAEjecutar.Comienzo = Tiempo;
                procesoAEjecutar.Finalizacion = Tiempo + procesoAEjecutar.Rafaga;

                // Avanzar el tiempo según la ráfaga del proceso ejecutado
                Tiempo += procesoAEjecutar.Rafaga;

                // Marcar el proceso como ejecutado
                procesoAEjecutar.Ejecutado = true;

                // Remover el proceso ejecutado de la lista de procesos no ejecutados
                procesosNoEjecutados.Remove(procesoAEjecutar);
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
            var plotModel = GeneratePlotModel();
            var imgGenerator = new ImageGenerator();
            imgGenerator.GenerateImage(plotModel, Procesos, PromedioTiempoEspera, PromedioTiempoSistema, "IMG/prioridad");
        }

        public void Run()
        {
            this.RunProcess();
            this.CalcularTiempos();
            this.CreateIMG();
            Console.WriteLine("Prioridad");
            for (int i = 0; i < Procesos.Count; i++)
            {
                var proceso = Procesos[i];
                Console.WriteLine($"Proceso: {proceso.Proceso}, Tiempo de llegada: {proceso.Llegada}, Tiempo de finalización: {proceso.Finalizacion}, Tiempo de inicio: {proceso.Comienzo}, Tiempo de espera: {proceso.TiempoEspera}, Tiempo de sistema: {proceso.TiempoSistema}");
            }
        }
    }
}
