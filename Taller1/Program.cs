using System;
using System.Collections.Generic;
using Taller1.Helpers;
using Taller1.FIFO;
using Taller1.FIFO.Model;

namespace SO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ObtainData obtainData = new ObtainData(AlgorithmType.Fifo);
            List<FifoModel> fifoData = obtainData.GetFifoData();

            FIFO fifo = new FIFO();
            fifo.LoadProcesos(fifoData);

            // Mostrar por consola los datos de cada FifoModel
            foreach (var proceso in fifo.GetProcesos())
            {
                Console.WriteLine($"Proceso: {proceso.Proceso}, Rafaga: {proceso.Rafaga}, Llegada: {proceso.Llegada}");
            }
        }
    }
}