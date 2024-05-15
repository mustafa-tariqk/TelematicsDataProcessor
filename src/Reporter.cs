using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using static DataProcessor;

class Reporter
{
    public Dictionary<string, string> Report(VehicleDataStore vehicleDataStore)
    {
        // get all data from vehicleDataStore
        var example = new Dictionary<string, string> { {"example", "json"} };
        return example;
    }

    public string SaveToFile(VehicleDataStore vehicleDataStore, string directory) // store example json to directory
    {
        var data = Report(vehicleDataStore);
        var json = JsonSerializer.Serialize(data);
        // json name should be datetime .json
        var name = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";
        var path = Path.Combine(directory, name);
        File.WriteAllText(path, json);
        return name;
    }
}