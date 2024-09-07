using SoForm.Data;
using SoForm.Mappers;
using SoForm.Models;
using OxyPlot;


namespace SoForm.Algorithms
{
    public class Prioridad
    {
        readonly AppDbContext _context;
        private int Tiempo { get; set; } = 0;
        public double PromedioTiempoEspera { get; set; } = 0;
        public double PromedioTiempoSistema { get; set; } = 0;
        public List<ProcessModel> Procesos { get; set; }
        public Prioridad()
        {
            _context = new AppDbContext();
            Procesos = _context.Process.ToList().ToModel();
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


        public void CalcularTiempos()
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
            this.RunProcess();
            var plotModelGenerator = new PlotModelGenerator();
            return plotModelGenerator.CreatePlotPrioridadModel(Procesos, Tiempo);
        }
    }
}