using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIFO.Model;

namespace SO.FIFO
{
    public class FIFO
    {
        private List<FifoModel> Procesos { get; set; }

        private void AddProceso(string proceso, int rafaga, int llegada)
        {
            Procesos.Add(new FifoModel(proceso, rafaga, llegada));
        }
    }
}