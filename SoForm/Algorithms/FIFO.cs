using SoForm.Data;
using SoForm.Mappers;
using SoForm.Models;
using OxyPlot;


namespace SoForm.Algorithms
{
    public class FIFO
    {
        readonly AppDbContext _context;
        private int Tiempo { get; set; } = 0;
        private double PromedioTiempoEspera { get; set; } = 0;
        private double PromedioTiempoSistema { get; set; } = 0;
        public List<ProcessModel> Procesos { get; set; } 
        public FIFO()
        {
            _context = new AppDbContext();
            Procesos = _context.Process.ToList().ToModel();
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
            return plotModelGenerator.CreatePlotFIFOModel(Procesos, Tiempo);
        }
    }
}
