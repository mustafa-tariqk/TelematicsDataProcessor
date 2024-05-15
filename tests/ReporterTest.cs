using NUnit.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TelematicsDataProcessor.Tests
{
    [TestFixture]
    public class ReporterTests
    {
        private Reporter _reporter;
        private DataProcessor.VehicleDataStore _dataStore;

        [SetUp]
        public void Setup()
        {
            _reporter = new Reporter();
            _dataStore = new DataProcessor.VehicleDataStore();
        }

        [Test]
        public void Report_EmptyDataStore_ShouldReturnEmptyMetrics()
        {
            // Arrange
            _dataStore.dataStore = new ConcurrentDictionary<int, ConcurrentDictionary<System.DateTime, DataProcessor.VehicleData>>();

            // Act
            var result = _reporter.Report(_dataStore);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Report_DataStoreWithSingleVehicle_ShouldReturnCorrectMetrics()
        {
            // Arrange
            var vehicleData = new DataProcessor.VehicleData
            {
                VehicleId = 1,
                DriverId = 1,
                Speed = 60,
                Acceleration = 5,
                EngineRpm = 3000,
                FuelLevel = 80,
                BrakeUsage = 20,
                TirePressure = 32,
                Temperature = 25,
                VehicleStatus = "Running"
            };
            _dataStore.dataStore.TryAdd(1, new ConcurrentDictionary<System.DateTime, DataProcessor.VehicleData>());
            _dataStore.dataStore[1].TryAdd(System.DateTime.Now, vehicleData);

            // Act
            var result = _reporter.Report(_dataStore);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count);
            Assert.AreEqual("1", result["TotalVehicles"]);
            Assert.AreEqual("1", result["TotalDrivers"]);
            Assert.AreEqual("60", result["AverageSpeed"]);
            Assert.AreEqual("5", result["AverageAcceleration"]);
            Assert.AreEqual("3000", result["AverageEngineRpm"]);
            Assert.AreEqual("80", result["AverageFuelLevel"]);
            Assert.AreEqual("20", result["AverageBrakeUsage"]);
            Assert.AreEqual("32", result["AverageTirePressure"]);
            Assert.AreEqual("25", result["AverageTemperature"]);
            Assert.AreEqual("Running", result["MostCommonVehicleStatus"]);
        }

        [Test]
        public void SaveToFile_ValidDataStoreAndDirectory_ShouldSaveJsonFileAndReturnFileName()
        {
            // Arrange
            var vehicleData = new DataProcessor.VehicleData
            {
                VehicleId = 1,
                DriverId = 1,
                Speed = 60,
                Acceleration = 5,
                EngineRpm = 3000,
                FuelLevel = 80,
                BrakeUsage = 20,
                TirePressure = 32,
                Temperature = 25,
                VehicleStatus = "Running"
            };
            _dataStore.dataStore.TryAdd(1, new ConcurrentDictionary<System.DateTime, DataProcessor.VehicleData>());
            _dataStore.dataStore[1].TryAdd(System.DateTime.Now, vehicleData);
            string directory = ".";

            // Act
            var result = _reporter.SaveToFile(_dataStore, directory);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.EndsWith(".json"));
        }
    }
}