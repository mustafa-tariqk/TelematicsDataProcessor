using System;
using System.IO;
using System.Threading.Tasks;
// Data column info: timestamp,vehicle_id,driver_id,latitude,longitude,speed,acceleration,engine_rpm,fuel_level,brake_usage,tire_pressure,temperature,vehicle_status


/// <summary>
/// Represents a data processor that handles streaming input from a file.
/// </summary>
class DataProcessor
{
    private static readonly Random _random = new Random(); // simulate random delay
    public VehicleDataStore vehicleDataStore = new VehicleDataStore();

    public class VehicleData
    {
        public DateTime Timestamp { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Speed { get; set; }
        public int Acceleration { get; set; }
        public int EngineRpm { get; set; }
        public int FuelLevel { get; set; }
        public int BrakeUsage { get; set; }
        public int TirePressure { get; set; }
        public int Temperature { get; set; }
        public string VehicleStatus { get; set; }
    }

    public class VehicleDataStore
    {
        private Dictionary<int, SortedList<DateTime, VehicleData>> dataStore;

        public VehicleDataStore()
        {
            dataStore = new Dictionary<int, SortedList<DateTime, VehicleData>>();
        }

        public void Add(string dataLine)
        {
            var parts = dataLine.Split(',');

            var vehicleData = new VehicleData
            {
                // print parts[0]
                Timestamp = DateTime.ParseExact(parts[0], "yyyy-MM-dd-HH-mm-ss" , null),
                VehicleId = int.Parse(parts[1]),
                DriverId = int.Parse(parts[2]),
                Latitude = double.Parse(parts[3]),
                Longitude = double.Parse(parts[4]),
                Speed = int.Parse(parts[5]),
                Acceleration = int.Parse(parts[6]),
                EngineRpm = int.Parse(parts[7]),
                FuelLevel = int.Parse(parts[8]),
                BrakeUsage = int.Parse(parts[9]),
                TirePressure = int.Parse(parts[10]),
                Temperature = int.Parse(parts[11]),
                VehicleStatus = parts[12]
            };

            if (!dataStore.ContainsKey(vehicleData.VehicleId))
            {
                dataStore[vehicleData.VehicleId] = new SortedList<DateTime, VehicleData>();
            }

            dataStore[vehicleData.VehicleId].Add(vehicleData.Timestamp, vehicleData);
        }

        public SortedList<DateTime, VehicleData> Get(int vehicleId)
        {
            if (dataStore.ContainsKey(vehicleId))
            {
                return dataStore[vehicleId];
            }

            return null;
        }
    }

    /// <summary>
    /// Reads a file line by line and processes each line asynchronously.
    /// Assumptions made about incoming data:
    /// All input is valid, no corruption, missing data, incorrect format, etc.
    /// Data is not too large to fit in memory.
    /// Data is not sensitive, no need to worry about encryption.
    /// Data is not ordered in any way, coming all over the place from the planet.
    /// Duplicate data is not sent.
    /// </summary>
    public async Task StreamingInput(string filePath)

    {
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                // Parse the line into a VehicleData object
                vehicleDataStore.Add(line);


                // getting data on the other side of the planet is approx 300ms, extra for some processing time.
                int delay = _random.Next(300, 601);
                await Task.Delay(delay);
            }
        }
    }

}