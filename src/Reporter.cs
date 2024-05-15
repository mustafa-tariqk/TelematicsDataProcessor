using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using static DataProcessor;

class Reporter
{
public Dictionary<string, string> Report(VehicleDataStore vehicleDataStore)
    {
    var metrics = new Dictionary<string, string>();
    var vehicleStatusCounts = new Dictionary<string, int>();
    var uniqueDriverIds = new HashSet<int>();
    var uniqueVehicleIds = new HashSet<int>(); // New HashSet for unique VehicleIds

    int totalVehicles = 0;
    double totalSpeed = 0;
    double totalAcceleration = 0;
    double totalEngineRpm = 0;
    double totalFuelLevel = 0;
    double totalBrakeUsage = 0;
    double totalTirePressure = 0;
    double totalTemperature = 0;
    int dataPoints = 0;

    foreach (var vehicleDataList in vehicleDataStore.dataStore.Values)
    {
        foreach (var vehicleData in vehicleDataList.Values)
        {
            dataPoints++;
            if (uniqueVehicleIds.Add(vehicleData.VehicleId)) // Check if VehicleId is unique
            {
                totalVehicles++; // Increment totalVehicles only if VehicleId is unique
            }

            uniqueDriverIds.Add(vehicleData.DriverId);
            totalSpeed += vehicleData.Speed;
            totalAcceleration += vehicleData.Acceleration;
            totalEngineRpm += vehicleData.EngineRpm;
            totalFuelLevel += vehicleData.FuelLevel;
            totalBrakeUsage += vehicleData.BrakeUsage;
            totalTirePressure += vehicleData.TirePressure;
            totalTemperature += vehicleData.Temperature;

            if (vehicleStatusCounts.ContainsKey(vehicleData.VehicleStatus))
            {
                vehicleStatusCounts[vehicleData.VehicleStatus]++;
            }
            else
            {
                vehicleStatusCounts[vehicleData.VehicleStatus] = 1;
            }
        }
    }

    int totalDrivers = uniqueDriverIds.Count;

    metrics["TotalVehicles"] = totalVehicles.ToString();
    metrics["TotalDrivers"] = totalDrivers.ToString();
    metrics["AverageSpeed"] = (totalSpeed / dataPoints).ToString();
    metrics["AverageAcceleration"] = (totalAcceleration / dataPoints).ToString();
    metrics["AverageEngineRpm"] = (totalEngineRpm / dataPoints).ToString();
    metrics["AverageFuelLevel"] = (totalFuelLevel / dataPoints).ToString();
    metrics["AverageBrakeUsage"] = (totalBrakeUsage / dataPoints).ToString();
    metrics["AverageTirePressure"] = (totalTirePressure / dataPoints).ToString();
    metrics["AverageTemperature"] = (totalTemperature / dataPoints).ToString();
    metrics["MostCommonVehicleStatus"] = vehicleStatusCounts.OrderByDescending(v => v.Value).First().Key;

    return metrics;
    }

    public string SaveToFile(VehicleDataStore vehicleDataStore, string directory)
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