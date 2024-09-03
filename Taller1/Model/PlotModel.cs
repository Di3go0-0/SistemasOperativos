using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using Taller1.Model;

namespace Taller1.Model
{
    public class PlotModelGenerator
    {
        public PlotModel CreatePlotFIFOModel(List<ProcessModel> fifoData, int tiempoTotal)
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

        public PlotModel CreatePlotSJFModel(List<ProcessModel> sjfData, int tiempoTotal)
        {
            var plotModel = new PlotModel { Title = "SJF Process Execution", Background = OxyColors.White };

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
            foreach (var proceso in sjfData)
            {
                yAxis.Labels.Add(proceso.Proceso);
            }
            plotModel.Axes.Add(yAxis);

            // Crear una serie de líneas para cada proceso
            foreach (var proceso in sjfData)
            {
                var lineSeries = new LineSeries
                {
                    Title = proceso.Proceso,
                    MarkerType = MarkerType.None,
                    Color = OxyColors.Blue,
                    StrokeThickness = 3 // Línea en negrilla
                };

                // Añadir los puntos de llegada y finalización basados en la ráfaga
                lineSeries.Points.Add(new DataPoint(proceso.Comienzo, sjfData.IndexOf(proceso)));
                lineSeries.Points.Add(new DataPoint(proceso.Comienzo + proceso.Rafaga, sjfData.IndexOf(proceso)));

                plotModel.Series.Add(lineSeries);
            }

            return plotModel;
        }
    }
}