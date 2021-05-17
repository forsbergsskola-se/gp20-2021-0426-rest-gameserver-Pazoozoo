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

            switch (database) {
                case "offline":
                    rental = new OfflineLameScooterRental("scooters.json");
                    break;
                case "deprecated":
                    rental = new DeprecatedLameScooterRental("scooters.txt");
                    break;
                case "realtime":
                    rental = new RealTimeLameScooterRental(
                        "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json");
                    break;
                default:
                    rental = new OfflineLameScooterRental("scooters.json");
                    break;
            }

            try {
                Console.WriteLine($"Number of Scooters Available at {station} Station: " +
                                  $"{rental.GetScooterCountInStation(station).Result}");
            }
            catch (ArgumentException e) {
                Console.WriteLine($"Error: {e.Message}");
            }
            catch (NotFoundException e) {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
