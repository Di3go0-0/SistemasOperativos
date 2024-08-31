using System.Collections.Generic;
using Taller1.FIFO.Model;

namespace Taller1.FIFO
{
    public class FIFO
    {
        private List<FifoModel> Procesos { get; set; } = new List<FifoModel>();
        private int Tiempo { get; set; } = 0;
        private double PromedioTiempoEspera { get; set; } = 0;
        private double PromedioTiempoSistema { get; set; } = 0;

        public void AddProceso(string proceso, int rafaga, int llegada)
        {
            Procesos.Add(new FifoModel(proceso, rafaga, llegada));
        }

        public void LoadProcesos(List<FifoModel> procesos)
        {
            foreach (var proceso in procesos)
            {
                AddProceso(proceso.Proceso, proceso.Rafaga, proceso.Llegada);
            }
        }

        public List<FifoModel> GetProcesos()
        {
            return Procesos;
        }

        public void Run()
        {
            foreach (FifoModel proceso in Procesos)
            {
                if (proceso.Llegada <= Tiempo && !proceso.Ejecutado)
                {
                    proceso.Comienzo = Tiempo;
                    proceso.Finalizacion = Tiempo + proceso.Rafaga;
                    proceso.Ejecutado = true;
                    Tiempo += proceso.Rafaga;
                }
                else
                {
                    Tiempo++;
                }
            }
        }
        public void PrintModels()
        {
            this.Run();
            foreach (FifoModel proceso in Procesos)
            {
                System.Console.WriteLine($"Proceso: {proceso.Proceso}, Rafaga: {proceso.Rafaga}, Llegada: {proceso.Llegada}, Comienzo: {proceso.Comienzo}, Finalizacion: {proceso.Finalizacion}");
            }
        }

        public void Tiempos()
        {
            foreach (FifoModel proceso in Procesos)
            {
                proceso.TiempoEspera = proceso.Comienzo - proceso.Llegada;
                proceso.TiempoSistema = proceso.Finalizacion - proceso.Llegada;
            }
            PromedioTiempoEspera = Procesos.Sum(proceso => proceso.TiempoEspera) / (double)Procesos.Count;
            PromedioTiempoSistema = Procesos.Sum(proceso => proceso.TiempoSistema) / (double)Procesos.Count;
        }

        public void PrintTiempos()
        {
            this.Tiempos();
            foreach (FifoModel proceso in Procesos)
            {
                System.Console.WriteLine($"Proceso: {proceso.Proceso}, Tiempo de espera: {proceso.TiempoEspera}, Tiempo de sistema: {proceso.TiempoSistema}");
            }
            System.Console.WriteLine($"Promedio de tiempo de espera: {PromedioTiempoEspera:F2}");
            System.Console.WriteLine($"Promedio de tiempo de sistema: {PromedioTiempoSistema:F2}");
        }
    }
}