using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class OfflineLameScooterRental : ILameScooterRental {

        public OfflineLameScooterRental(string uri) {
            Console.WriteLine($"Loading from path: {uri}");
            if (!File.Exists(uri)) {
                Console.Write($"File not found in path: {uri}");
                return;
            }

            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonString = File.ReadAllText(uri);
            var scooterList = JsonSerializer.Deserialize<List<LameScooterStationList>>(jsonString, options);

            foreach (var scooter in scooterList) {
                Console.WriteLine(scooter);
            }
        }
        public Task<int> GetScooterCountInStation(string stationName) {
            throw new NotImplementedException();
        }
    }
}