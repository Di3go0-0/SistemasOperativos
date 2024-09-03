using OxyPlot;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using Taller.Model;

namespace Taller.Helpers
{
    public class ImageGenerator
    {
        private const int BitmapWidth = 1200;
        private const int BitmapHeight = 400;
        private const int PlotWidth = 800;
        private const int PlotHeight = 400;
        private const int TextSize = 20;

        public void GenerateImage(PlotModel plotModel, List<ProcessModel> procesos, double promedioTiempoEspera, double promedioTiempoSistema, string imagePath)
        {
            string plotFilePath = $"{imagePath}_plot.png";
            ExportPlotToFile(plotModel, plotFilePath);
            DrawTableAndSaveImage(plotModel, procesos, promedioTiempoEspera, promedioTiempoSistema, $"{imagePath}_plot.png");
            Console.WriteLine($"Gráfica y tabla generadas y guardadas como {imagePath}_plot.png");
        }

        private void ExportPlotToFile(PlotModel plotModel, string filePath)
        {
            using (var stream = File.Create(filePath))
            {
                var pngExporter = new OxyPlot.SkiaSharp.PngExporter { Width = 1600, Height = 600 };
                pngExporter.Export(plotModel, stream);
            }
        }

        private void DrawTableAndSaveImage(PlotModel plotModel, List<ProcessModel> procesos, double promedioTiempoEspera, double promedioTiempoSistema, string filePath)
        {
            using (var bitmap = new SKBitmap(BitmapWidth, BitmapHeight))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);
                DrawPlotOnCanvas(plotModel, canvas);
                DrawTableOnCanvas(procesos, promedioTiempoEspera, promedioTiempoSistema, canvas);
                SaveBitmapToFile(bitmap, filePath);
            }
        }

        private void DrawPlotOnCanvas(PlotModel plotModel, SKCanvas canvas)
        {
            using (var stream = new MemoryStream())
            {
                var oxyPlotRenderer = new OxyPlot.SkiaSharp.PngExporter { Width = PlotWidth, Height = PlotHeight };
                oxyPlotRenderer.Export(plotModel, stream);
                stream.Seek(0, SeekOrigin.Begin);

                using (var skiaImage = SKImage.FromEncodedData(stream))
                {
                    var skiaBitmap = SKBitmap.FromImage(skiaImage);
                    canvas.DrawBitmap(skiaBitmap, 0, 0);
                }
            }
        }

        private void DrawTableOnCanvas(List<ProcessModel> procesos, double promedioTiempoEspera, double promedioTiempoSistema, SKCanvas canvas)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = TextSize,
                IsAntialias = true
            };

            int startX = 820;
            int startY = 20;
            int rowHeight = 30;

            DrawTableHeaders(canvas, startX, startY, paint);
            DrawTableRows(canvas, procesos, startX, startY + rowHeight, rowHeight, paint);
            DrawTableAverages(canvas, procesos.Count, promedioTiempoEspera, promedioTiempoSistema, startX, startY, rowHeight, paint);
        }

        private void DrawTableHeaders(SKCanvas canvas, int startX, int startY, SKPaint paint)
        {
            canvas.DrawText("Proceso", startX, startY, paint);
            canvas.DrawText("Espera", startX + 100, startY, paint);
            canvas.DrawText("Sistema", startX + 250, startY, paint);
        }

        private void DrawTableRows(SKCanvas canvas, List<ProcessModel> procesos, int startX, int startY, int rowHeight, SKPaint paint)
        {
            for (int i = 0; i < procesos.Count; i++)
            {
                var proceso = procesos[i];
                canvas.DrawText(proceso.Proceso, startX, startY + i * rowHeight, paint);
                canvas.DrawText(proceso.TiempoEspera.ToString(), startX + 100, startY + i * rowHeight, paint);
                canvas.DrawText(proceso.TiempoSistema.ToString(), startX + 250, startY + i * rowHeight, paint);
            }
        }

        private void DrawTableAverages(SKCanvas canvas, int rowCount, double promedioTiempoEspera, double promedioTiempoSistema, int startX, int startY, int rowHeight, SKPaint paint)
        {
            canvas.DrawText("Promedio", startX, startY + (rowCount + 1) * rowHeight, paint);
            canvas.DrawText(promedioTiempoEspera.ToString("F2"), startX + 100, startY + (rowCount + 1) * rowHeight, paint);
            canvas.DrawText(promedioTiempoSistema.ToString("F2"), startX + 250, startY + (rowCount + 1) * rowHeight, paint);
        }

        private void SaveBitmapToFile(SKBitmap bitmap, string filePath)
        {
            using (var image = SKImage.FromBitmap(bitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = File.OpenWrite(filePath))
            {
                data.SaveTo(stream);
            }
        }
    }
}
