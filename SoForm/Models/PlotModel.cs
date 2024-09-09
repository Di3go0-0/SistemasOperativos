using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace SoForm.Models
{
    public class PlotModelGenerator
    {
        public PlotModel CreatePlotFIFOModel(List<ProcessModel> fifoData, int tiempoTotal)
        {
            return CreatePlotModel(fifoData, tiempoTotal, "FIFO Process Execution", OxyColors.Black);
        }

        public PlotModel CreatePlotSJFModel(List<ProcessModel> sjfData, int tiempoTotal)
        {
            return CreatePlotModel(sjfData, tiempoTotal, "SJF Process Execution", OxyColors.Blue);
        }

        public PlotModel CreatePlotPrioridadModel(List<ProcessModel> prioridadData, int tiempoTotal)
        {
            return CreatePlotModel(prioridadData, tiempoTotal, "Prioridad Process Execution", OxyColors.Red);
        }

        private PlotModel CreatePlotModel(List<ProcessModel> processData, int tiempoTotal, string title, OxyColor lineColor)
        {
            var plotModel = new PlotModel { Title = title, Background = OxyColors.White };

                        // Configurar los ejes
            ConfigureAxes(plotModel, tiempoTotal, processData);
            
            // Crear una serie de líneas para cada proceso
            foreach (var proceso in processData)
            {
                // Línea continua desde el comienzo hasta el final basado en la ráfaga
                var lineSeries = new LineSeries
                {
                    Title = proceso.Proceso,
                    MarkerType = MarkerType.None,
                    Color = lineColor,
                    StrokeThickness = 3 // Línea en negrilla
                };
            
                // Añadir los puntos de comienzo y finalización basados en la ráfaga
                lineSeries.Points.Add(new DataPoint(proceso.Comienzo, processData.IndexOf(proceso)));
                lineSeries.Points.Add(new DataPoint(proceso.Comienzo + proceso.Rafaga, processData.IndexOf(proceso)));
            
                plotModel.Series.Add(lineSeries);
            
                // Línea punteada desde el tiempo de llegada hasta el comienzo
                var dashedLineSeries = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    Color = lineColor,
                    StrokeThickness = 1, // Línea no en negrilla
                    LineStyle = LineStyle.Dash // Estilo de línea punteada
                };
            
                // Añadir los puntos de llegada y comienzo
                dashedLineSeries.Points.Add(new DataPoint(proceso.Llegada, processData.IndexOf(proceso)));
                dashedLineSeries.Points.Add(new DataPoint(proceso.Comienzo, processData.IndexOf(proceso)));
            
                plotModel.Series.Add(dashedLineSeries);
            }
            
            return plotModel;
        }

        private void ConfigureAxes(PlotModel plotModel, int tiempoTotal, List<ProcessModel> processData)
        {
            // Configurar el eje X (Tiempo)
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Tiempo",
                Minimum = 0,
                Maximum = tiempoTotal
            };
            plotModel.Axes.Add(xAxis);

            // Configurar el eje Y (Procesos)
            var yAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Procesos"
            };

            // Añadir los nombres de los procesos al eje Y
            foreach (var proceso in processData)
            {
                yAxis.Labels.Add(proceso.Proceso);
            }
            plotModel.Axes.Add(yAxis);
        }
    }
}
