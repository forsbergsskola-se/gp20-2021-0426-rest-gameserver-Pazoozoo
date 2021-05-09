using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class OfflineLameScooterRental : ILameScooterRental {
        List<LameScooterStationList> _scooterList;

        public OfflineLameScooterRental(string uri) {
            if (!File.Exists(uri)) {
                Console.Write($"File not found in path: {uri}");
                return;
            }

            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonString = File.ReadAllText(uri);
            _scooterList = JsonSerializer.Deserialize<List<LameScooterStationList>>(jsonString, options);
        }
        
        public Task<int> GetScooterCountInStation(string stationName) {

            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException($"Invalid input: <{stationName}> contains a digit");

            var station = _scooterList.Find(list => list.Name == stationName);
            
            if (station != null) 
                return Task.FromResult(station.BikesAvailable);
            
            Console.WriteLine($"Station not found: {stationName}");
            return null;
        }
    }
}