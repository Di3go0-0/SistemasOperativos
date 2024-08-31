using System.Collections.Generic;
using Taller1.FIFO.Model;

namespace Taller1.FIFO
{
    public class FIFO
    {
        private List<FifoModel> Procesos { get; set; } = new List<FifoModel>();

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
    }
}