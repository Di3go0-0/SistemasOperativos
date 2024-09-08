using SoForm.Algorithms;
using SoForm.Models;
using System;
using System.Collections.Generic;

namespace SoForm.Helpers
{
    public class TimeCalculator
    {
        public static List<ProcessModel> CalculateTimes(FormType formType)
        {
            List<ProcessModel> processModels = new List<ProcessModel>();

            switch (formType)
            {
                case FormType.FIFO:
                    FIFO fifo = new FIFO();
                    fifo.RunProcess();
                    fifo.CalcularTiempos();
                    processModels = fifo.Procesos;
                    break;
                case FormType.SJF:
                    SJF sjf = new SJF();
                    sjf.RunProcess();
                    sjf.CalcularTiempos();
                    processModels = sjf.Procesos;
                    break;
                case FormType.Prioridad:
                    Prioridad prioridad = new Prioridad();
                    prioridad.RunProcess();
                    prioridad.CalcularTiempos();
                    processModels = prioridad.Procesos;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(formType), formType, null);
            }

            return processModels;
        }
    }
}