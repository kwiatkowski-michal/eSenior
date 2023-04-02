using hackathon.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace hackathon.DataAccess
{
    public static class InformacjeZdrowotneContext
    {
        public static List<CukierIcisnienie> GetData()
        {
            string file = System.IO.File.ReadAllText("json.json");
            var people = JsonSerializer.Deserialize<List<CukierIcisnienie>>(file);
            return people;
        }

        public static void SaveData(List<CukierIcisnienie> i)
        {
            var people = JsonSerializer.Serialize<List<CukierIcisnienie>>(i);
            System.IO.File.WriteAllText("json.json", people);
        }
    }
}
