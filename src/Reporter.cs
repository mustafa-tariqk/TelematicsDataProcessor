using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using static DataProcessor;

class Reporter
{
    public Dictionary<string, string> Report(VehicleDataStore vehicleDataStore)
    {
        var metrics = new ConcurrentDictionary<string, string>();
        var vehicleStatusCounts = new ConcurrentDictionary<string, int>();
        var uniqueDriverIds = new ConcurrentDictionary<int, byte>();
        var uniqueVehicleIds = new ConcurrentDictionary<int, byte>();

        double totalSpeed = 0;
        double totalAcceleration = 0;
        double totalEngineRpm = 0;
        double totalFuelLevel = 0;
        double totalBrakeUsage = 0;
        double totalTirePressure = 0;
        double totalTemperature = 0;
        int dataPoints = 0;

        object lockObject = new object();

        Parallel.ForEach(vehicleDataStore.dataStore.Values, vehicleDataList =>
        {
            foreach (var vehicleData in vehicleDataList.Values)
            {
                lock (lockObject)
                {
                    dataPoints++;
                    if (uniqueVehicleIds.TryAdd(vehicleData.VehicleId, 0))
                    {
                        metrics["TotalVehicles"] = (int.Parse(metrics.GetOrAdd("TotalVehicles", "0")) + 1).ToString();
                    }

                    uniqueDriverIds.TryAdd(vehicleData.DriverId, 0);
                    totalSpeed += vehicleData.Speed;
                    totalAcceleration += vehicleData.Acceleration;
                    totalEngineRpm += vehicleData.EngineRpm;
                    totalFuelLevel += vehicleData.FuelLevel;
                    totalBrakeUsage += vehicleData.BrakeUsage;
                    totalTirePressure += vehicleData.TirePressure;
                    totalTemperature += vehicleData.Temperature;

                    vehicleStatusCounts.AddOrUpdate(vehicleData.VehicleStatus, 1, (key, oldValue) => oldValue + 1);
                }
            }
        });

        int totalDrivers = uniqueDriverIds.Count;

        metrics["TotalDrivers"] = totalDrivers.ToString();
        metrics["AverageSpeed"] = (totalSpeed / dataPoints).ToString();
        metrics["AverageAcceleration"] = (totalAcceleration / dataPoints).ToString();
        metrics["AverageEngineRpm"] = (totalEngineRpm / dataPoints).ToString();
        metrics["AverageFuelLevel"] = (totalFuelLevel / dataPoints).ToString();
        metrics["AverageBrakeUsage"] = (totalBrakeUsage / dataPoints).ToString();
        metrics["AverageTirePressure"] = (totalTirePressure / dataPoints).ToString();
        metrics["AverageTemperature"] = (totalTemperature / dataPoints).ToString();
        metrics["MostCommonVehicleStatus"] = vehicleStatusCounts.OrderByDescending(v => v.Value).First().Key;

        return new Dictionary<string, string>(metrics);
    }

    public string SaveToFile(VehicleDataStore vehicleDataStore, string directory)
    {
        var data = Report(vehicleDataStore);
        var json = JsonSerializer.Serialize(data);
        var name = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";
        var path = Path.Combine(directory, name);
        File.WriteAllText(path, json);
        return name;
    }
}