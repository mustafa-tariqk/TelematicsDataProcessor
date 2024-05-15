using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TelematicsDataProcessor.Tests
{
    [TestFixture]
    public class DataProcessorTests
    {
        private DataProcessor.VehicleDataStore _dataStore;
        private DataProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataStore = new DataProcessor.VehicleDataStore();
            _dataProcessor = new DataProcessor();
            _dataProcessor.vehicleDataStore = _dataStore;
        }

        [Test]
        public void Add_ValidDataLine_ShouldAddDataToDataStore()
        {
            // Arrange
            string dataLine = "2022-01-01-12-00-00,1,1,37.7749,-122.4194,60,5,3000,80,20,32,25,In Motion";

            // Act
            _dataProcessor.vehicleDataStore.Add(dataLine);

            // Assert
            Assert.IsTrue(_dataStore.dataStore.ContainsKey(1));
            Assert.IsTrue(_dataStore.dataStore[1].ContainsKey(DateTime.ParseExact("2022-01-01-12-00-00", "yyyy-MM-dd-HH-mm-ss", null)));
            Assert.AreEqual(1, _dataStore.dataStore[1][DateTime.ParseExact("2022-01-01-12-00-00", "yyyy-MM-dd-HH-mm-ss", null)].VehicleId);
        }

        [Test]
        public void Get_ExistingVehicleId_ShouldReturnSortedListOfTelematicsData()
        {
            // Arrange
            _dataStore.dataStore.TryAdd(1, new ConcurrentDictionary<DateTime, DataProcessor.VehicleData>());
            _dataStore.dataStore[1].TryAdd(DateTime.ParseExact("2022-01-01-12-00-00", "yyyy-MM-dd-HH-mm-ss", null), new DataProcessor.VehicleData { VehicleId = 1 });
            _dataStore.dataStore[1].TryAdd(DateTime.ParseExact("2022-01-01-12-01-00", "yyyy-MM-dd-HH-mm-ss", null), new DataProcessor.VehicleData { VehicleId = 1 });
            _dataStore.dataStore[1].TryAdd(DateTime.ParseExact("2022-01-01-12-02-00", "yyyy-MM-dd-HH-mm-ss", null), new DataProcessor.VehicleData { VehicleId = 1 });

            // Act
            var result = _dataProcessor.vehicleDataStore.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result.Values[0].VehicleId);
            Assert.AreEqual(1, result.Values[1].VehicleId);
            Assert.AreEqual(1, result.Values[2].VehicleId);
        }

        [Test]
        public void Get_NonExistingVehicleId_ShouldReturnNull()
        {
            // Arrange
            _dataStore.dataStore.TryAdd(1, new ConcurrentDictionary<DateTime, DataProcessor.VehicleData>());

            // Act
            var result = _dataProcessor.vehicleDataStore.Get(2);

            // Assert
            Assert.IsNull(result);
        }
    }
}