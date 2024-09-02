using System;
using System.Collections.Generic;
using System.IO;
using Taller1.Helpers;
using Taller1.FIFO;
using Taller1.FIFO.Model;
using OxyPlot;
using OxyPlot.SkiaSharp;

namespace SO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ObtainData obtainData = new ObtainData(AlgorithmType.Fifo);
            List<FifoModel> fifoData = obtainData.GetFifoData() ?? new List<FifoModel>();

            FIFO fifo = new FIFO();
            fifo.LoadProcesos(fifoData);

            // Mostrar por consola los datos de cada FifoModel
            foreach (var proceso in fifo.GetProcesos())
            {
                Console.WriteLine($"Proceso: {proceso.Proceso}, Rafaga: {proceso.Rafaga}, Llegada: {proceso.Llegada}");
            }
            Console.WriteLine("Procesos ejecutados");
            fifo.PrintModels();
            Console.WriteLine("Tiempos");
            fifo.PrintTiempos();

            // Crear y guardar la gráfica usando el nuevo método en FIFO
            var plotModel = fifo.GeneratePlotModel();

            using (var stream = File.Create("fifo_plot.png"))
            {       
                var pngExporter = new PngExporter { Width = 600, Height = 400 };
                pngExporter.Export(plotModel, stream);
            }

            Console.WriteLine("Gráfica generada y guardada como fifo_plot.png");
        }
    }
}