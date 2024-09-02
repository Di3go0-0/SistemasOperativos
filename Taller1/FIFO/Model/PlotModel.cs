using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using Taller1.Model;

namespace Taller1.FIFO.Model
{
    public class PlotModelGenerator
    {
        public PlotModel CreatePlotModel(List<ProcessModel> fifoData, int tiempoTotal)
        {
            var plotModel = new PlotModel { Title = "FIFO Process Execution", Background = OxyColors.White };

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
            foreach (var proceso in fifoData)
            {
                yAxis.Labels.Add(proceso.Proceso);
            }
            plotModel.Axes.Add(yAxis);
            foreach (var proceso in fifoData)
            {
                Console.WriteLine("////////////////////////////////////////////");
                Console.WriteLine($"Proceso: {proceso.Proceso}, Comienzo: {proceso.Comienzo}, Finalizacion: {proceso.Finalizacion}");
            }


            // Crear una serie de líneas para cada proceso
            foreach (var proceso in fifoData)
            {
                var lineSeries = new LineSeries
                {
                    Title = proceso.Proceso,
                    MarkerType = MarkerType.None,
                    Color = OxyColors.Black,
                    StrokeThickness = 3 // Línea en negrilla
                };

                // Añadir los puntos de llegada y finalización basados en la ráfaga
                lineSeries.Points.Add(new DataPoint(proceso.Comienzo, fifoData.IndexOf(proceso)));
                lineSeries.Points.Add(new DataPoint(proceso.Comienzo + proceso.Rafaga, fifoData.IndexOf(proceso)));

                plotModel.Series.Add(lineSeries);
            }

            return plotModel;
        }
    }
}