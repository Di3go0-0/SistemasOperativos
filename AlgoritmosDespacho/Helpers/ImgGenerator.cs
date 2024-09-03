using OxyPlot;
using SkiaSharp;
using System.Collections.Generic;
using Taller.Model;

namespace Taller.Helpers
{
    public class ImageGenerator
    {
        public void GenerateImage(PlotModel plotModel, List<ProcessModel> procesos, double promedioTiempoEspera, double promedioTiempoSistema, string imagePath)
        {
            // Exportar la gráfica a un archivo PNG
            using (var stream = System.IO.File.Create(imagePath + "_plot.png"))
            {
                var pngExporter = new OxyPlot.SkiaSharp.PngExporter { Width = 1600, Height = 600 };
                pngExporter.Export(plotModel, stream);
            }

            // Crear un bitmap y dibujar la tabla
            using (var bitmap = new SKBitmap(1200, 400))
            using (var canvas = new SKCanvas(bitmap))
            {
                // Llenar el fondo de blanco
                canvas.Clear(SKColors.White);

                // Exportar la gráfica al lienzo (canvas)
                var oxyPlotRenderer = new OxyPlot.SkiaSharp.PngExporter { Width = 800, Height = 400 };
                using (var stream = new System.IO.MemoryStream())
                {
                    oxyPlotRenderer.Export(plotModel, stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);

                    using (var skiaImage = SKImage.FromEncodedData(stream))
                    {
                        var skiaBitmap = SKBitmap.FromImage(skiaImage);
                        canvas.DrawBitmap(skiaBitmap, 0, 0);
                    }
                }

                // Dibujar la tabla
                var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 20,
                    IsAntialias = true
                };

                int startX = 820;
                int startY = 20;
                int rowHeight = 30;

                // Dibujar encabezados
                canvas.DrawText("Proceso", startX, startY, paint);
                canvas.DrawText("Tiempo Espera", startX + 100, startY, paint);
                canvas.DrawText("Tiempo Sistema", startX + 250, startY, paint);

                // Dibujar filas
                for (int i = 0; i < procesos.Count; i++)
                {
                    var proceso = procesos[i];
                    canvas.DrawText(proceso.Proceso, startX, startY + (i + 1) * rowHeight, paint);
                    canvas.DrawText(proceso.TiempoEspera.ToString(), startX + 100, startY + (i + 1) * rowHeight, paint);
                    canvas.DrawText(proceso.TiempoSistema.ToString(), startX + 250, startY + (i + 1) * rowHeight, paint);
                }

                // Dibujar fila de promedios
                canvas.DrawText("Promedio", startX, startY + (procesos.Count + 1) * rowHeight, paint);
                canvas.DrawText(promedioTiempoEspera.ToString("F2"), startX + 100, startY + (procesos.Count + 1) * rowHeight, paint);
                canvas.DrawText(promedioTiempoSistema.ToString("F2"), startX + 250, startY + (procesos.Count + 1) * rowHeight, paint);

                // Guardar el bitmap final en un archivo
                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = System.IO.File.OpenWrite(imagePath + "_plot.png"))
                {
                    data.SaveTo(stream);
                }
            }

            Console.WriteLine($"Gráfica y tabla generadas y guardadas como {imagePath}_plot_with_table.png");
        }
    }
}
