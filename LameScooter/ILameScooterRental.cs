using System.Threading.Tasks;

namespace LameScooter {
    public interface ILameScooterRental {
        Task GetScooterDatabase(string uri);
        Task<int> GetScooterCountInStation(string stationName);
    }
}