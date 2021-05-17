using System;

namespace LameScooter
{
    class Program
    {
        static void Main(string[] args) {
            string station = args[0];
            string database = args[1];
            database = database.ToLower();
            ILameScooterRental rental;
            string uri;

            switch (database) {
                case "offline":
                    rental = new OfflineLameScooterRental();
                    uri = "scooters.json";
                    break;
                case "deprecated":
                    rental = new DeprecatedLameScooterRental();
                    uri = "scooters.txt";
                    break;
                case "realtime":
                    rental = new RealTimeLameScooterRental();
                    uri = "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
                    break;
                default:
                    rental = new OfflineLameScooterRental();
                    uri = "scooters.json";
                    break;
            }

            rental.GetScooterDatabase(uri).GetAwaiter().GetResult();

            try {
                Console.WriteLine($"Number of Scooters Available at {station} Station: " +
                                  $"{rental.GetScooterCountInStation(station).Result}");
            }
            catch (ArgumentException e) {
                Console.WriteLine($"Error message: {e.Message}");
            }
            catch (NotFoundException e) {
                Console.WriteLine($"Error message: {e.Message}");
            }
        }
    }
}
