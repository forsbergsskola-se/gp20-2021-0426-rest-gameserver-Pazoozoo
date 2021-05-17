using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LameScooter {
    public class DeprecatedLameScooterRental : ILameScooterRental {
        Dictionary<string, LameScooterStationList> _scooterList;
        public DeprecatedLameScooterRental(string uri) {
            Console.WriteLine(uri);
            if (!File.Exists(uri)) {
                Console.Write($"File not found in path: {uri}");
                return;
            }
            ReadFile(uri);
        }

        void ReadFile(string uri) {
            using (StreamReader sr = new StreamReader(uri)) {
                string line;
                while ((line = sr.ReadLine()) != null) {
                    Console.WriteLine(line);
                }
            }
        }
        
        public Task<int> GetScooterCountInStation(string stationName) {
            throw new NotImplementedException();
        }
    }
}