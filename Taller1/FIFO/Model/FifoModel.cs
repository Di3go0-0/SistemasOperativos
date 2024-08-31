using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIFO.Model
{
    public class FifoModel
    {
        private string Proceso { get; set; }
        private int Rafaga { get; set; }
        private int Llegada { get; set; }
        public FifoModel(string proceso, int rafaga, int llegada)
        {
            Proceso = proceso;
            Rafaga = rafaga;
            Llegada = llegada;
        }
    }
}