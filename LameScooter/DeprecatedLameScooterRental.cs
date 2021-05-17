using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter {
    public class DeprecatedLameScooterRental : ILameScooterRental {
        Dictionary<string, int> _scooterDictionary;
        public DeprecatedLameScooterRental(string uri) {
            if (!File.Exists(uri)) {
                Console.Write($"File not found in path: {uri}");
                return;
            }
            ReadFile(uri);
        }

        void ReadFile(string uri) {
            _scooterDictionary = new Dictionary<string, int>();
            
            using var streamReader = new StreamReader(uri);
            
            while (streamReader.Peek() >= 0) {
                var line = streamReader.ReadLine();
                if (line == null) continue;
                var index = line.IndexOf(':');
                var station = line.Substring(0, index - 1);
                var bikesAvailable = int.Parse(line.Substring(index + 2));
                _scooterDictionary[station] = bikesAvailable;
            }
        }
        
        public Task<int> GetScooterCountInStation(string stationName) {
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException($"{stationName} contains a digit.");

            try {
                return Task.FromResult(_scooterDictionary[stationName]);
            }
            catch (KeyNotFoundException e) {
                throw new NotFoundException(stationName);
            }
        }
    }
}