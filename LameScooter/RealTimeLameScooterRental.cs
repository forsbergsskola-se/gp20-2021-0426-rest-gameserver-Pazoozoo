using System;
using System.Threading.Tasks;

namespace LameScooter {
    public class RealTimeLameScooterRental : ILameScooterRental {
        public Task<int> GetScooterCountInStation(string stationName) {
            throw new NotImplementedException();
        }
    }
}