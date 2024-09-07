using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoForm.Models;

namespace SoForm.Mappers
{
    public static class ProcessMappers
    {
        public static List<ProcessModel> ToModel(this List<ProcessDbModel> processDbModel)
        {
            return processDbModel.Select(p => new ProcessModel(
                p.Id ?? 0, // Convierte int? a int, usando 0 como valor predeterminado si es null
                p.Proceso,
                p.Rafaga,
                p.Llegada,
                p.Prioridad
            )).ToList();
        }

        public static ProcessDbModel ToDbModel(this ProcessModel processModel)
        {
            return new ProcessDbModel
            {
                Id = processModel.Id,
                Proceso = processModel.Proceso,
                Rafaga = processModel.Rafaga,
                Llegada = processModel.Llegada,
                Prioridad = processModel.Prioridad
            };
        }
    }
}