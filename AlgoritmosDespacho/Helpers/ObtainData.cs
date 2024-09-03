using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Taller.Model;

namespace Taller.Helpers
{
    public class ObtainData
    {
        public ObtainData(){}
        public List<ProcessModel> GetFifoData()
        {
            string jsonData = File.ReadAllText("Data/data.json");
            return JsonConvert.DeserializeObject<List<ProcessModel>>(jsonData) ?? new List<ProcessModel>();
        }
    }
}