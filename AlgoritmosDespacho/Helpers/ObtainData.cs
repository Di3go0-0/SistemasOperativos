using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Taller.Model;

namespace Taller.Helpers
{
    public enum AlgorithmType
    {
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

        public List<ProcessModel> GetFifoData()
        {
            if (SelectedAlgorithm != AlgorithmType.Fifo)
                throw new InvalidOperationException("Selected algorithm is not FIFO");

            string jsonData = File.ReadAllText("Data/data.json");
            return JsonConvert.DeserializeObject<List<ProcessModel>>(jsonData) ?? new List<ProcessModel>();
        }
    }
}