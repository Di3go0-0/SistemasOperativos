using SoForm.Data;
using SoForm.Mappers;
using SoForm.Models;
using OxyPlot;
namespace SoForm.Algorithms
{
    public class SJF
    {
        readonly AppDbContext _context;
        private int Tiempo { get; set; } = 0;
        public double PromedioTiempoEspera { get; set; } = 0;
        public double PromedioTiempoSistema { get; set; } = 0;
        public List<ProcessModel> Procesos { get; set; }
        public SJF()
        {
            _context = new AppDbContext();
            Procesos = _context.Process.ToList().ToModel();
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


        public void CalcularTiempos()
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

        public PlotModel GeneratePlotModel()
        {
            this.RunProcess();
            var plotModelGenerator = new PlotModelGenerator();
            return plotModelGenerator.CreatePlotSJFModel(Procesos, Tiempo);
        }
    }
}