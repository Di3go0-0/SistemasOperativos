using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using Taller.Model;

namespace Taller.Model
{
    public class PlotModelRoundRobin
    {
        public PlotModel CreatePlotModel(List<RoundRobinProcessModel> processData, int tiempoTotal)
        {
            var plotModel = new PlotModel { Title = "Round Robin Scheduling" };
            var lineColor = OxyColors.Blue;

            // Configurar los ejes
            ConfigureAxes(plotModel, tiempoTotal, processData);

            // Crear una serie de líneas para cada proceso
            foreach (var proceso in processData)
            {
                var lineSeries = new LineSeries
                {
                    Title = proceso.Proceso,
                    MarkerType = MarkerType.None,
                    Color = lineColor,
                    StrokeThickness = 3 // Línea en negrilla
                };

                // Añadir los puntos de inicio y finalización basados en los tiempos de inicio y finalización
                for (int i = 0; i < proceso.Comienzo.Count; i++)
                {
                    lineSeries.Points.Add(new DataPoint(proceso.Comienzo[i], processData.IndexOf(proceso)));
                    lineSeries.Points.Add(new DataPoint(proceso.Finalizacion[i], processData.IndexOf(proceso)));
                }

                plotModel.Series.Add(lineSeries);

                // Línea punteada desde el tiempo de llegada hasta el comienzo
                if (proceso.Comienzo.Any())
                {
                    var dashedLineSeries = new LineSeries
                    {
                        MarkerType = MarkerType.None,
                        Color = lineColor,
                        StrokeThickness = 1, // Línea no en negrilla
                        LineStyle = LineStyle.Dash // Estilo de línea punteada
                    };

                    // Añadir los puntos de llegada y comienzo
                    dashedLineSeries.Points.Add(new DataPoint(proceso.Llegada, processData.IndexOf(proceso)));
                    dashedLineSeries.Points.Add(new DataPoint(proceso.Comienzo.First(), processData.IndexOf(proceso)));

                    plotModel.Series.Add(dashedLineSeries);
                }
            }

            return plotModel;
        }

        private void ConfigureAxes(PlotModel plotModel, int tiempoTotal, List<RoundRobinProcessModel> processData)
        {
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = tiempoTotal,
                Title = "Tiempo"
            };

            var yAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Procesos"
            };

            foreach (var proceso in processData)
            {
                yAxis.Labels.Add(proceso.Proceso);
            }

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);
        }
    }
}