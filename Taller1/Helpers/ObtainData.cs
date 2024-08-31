using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Taller1.FIFO.Model;

namespace Taller1.Helpers
{
    public enum AlgorithmType
    {
        None,
        Fifo,
        Sjf,
        Prioridad
    }

    public class ObtainData
    {
        public AlgorithmType SelectedAlgorithm { get; private set; }

        public ObtainData(AlgorithmType algorithmType)
        {
            SelectedAlgorithm = algorithmType;
        }

        public List<FifoModel> GetFifoData()
        {
            if (SelectedAlgorithm != AlgorithmType.Fifo)
                throw new InvalidOperationException("Selected algorithm is not FIFO");

            string jsonData = File.ReadAllText("FIFO/Data/data.json"); 
            return JsonConvert.DeserializeObject<List<FifoModel>>(jsonData);
        }
    }
}