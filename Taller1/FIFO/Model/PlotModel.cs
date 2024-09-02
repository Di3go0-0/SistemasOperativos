using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;

namespace Taller1.FIFO.Model
{
    public class PlotModelGenerator
    {
        public PlotModel CreatePlotModel(List<FifoModel> fifoData)
        {
            var plotModel = new PlotModel { Title = "FIFO Process Execution" };

            var lineSeries = new LineSeries
            {
                Title = "Procesos",
                MarkerType = MarkerType.Circle
            };

            foreach (var proceso in fifoData)
            {
                lineSeries.Points.Add(new DataPoint(proceso.Llegada, proceso.Rafaga));
            }

            plotModel.Series.Add(lineSeries);
            return plotModel;
        }
    }
}