using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.Model
{
    public class RoundRobinProcessModel
    {
        public string Proceso { get; set; } = "";
        public int Rafaga { get; set; }
        public int Llegada { get; set; }
        public int Prioridad { get; set; }
        public List<int> Comienzo { get; set; } = new List<int>();
        public List<int> Finalizacion { get; set; } = new List<int>();
        public bool Ejecutado { get; set; }
        public int TiempoEspera { get; set; }
        public int TiempoSistema { get; set; }

        public RoundRobinProcessModel()
        {
        }
        public RoundRobinProcessModel(string proceso, int rafaga, int llegada)
        {
            Proceso = proceso;
            Rafaga = rafaga;
            Llegada = llegada;
        }
        public RoundRobinProcessModel(string proceso, int rafaga, int llegada, int prioridad)
        {
            Proceso = proceso;
            Rafaga = rafaga;
            Llegada = llegada;
            Prioridad = prioridad;
        }
    }

}