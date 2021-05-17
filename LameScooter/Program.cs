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
                default:
                    rental = new OfflineLameScooterRental("scooters.json");
                    break;
            }

            try {
                Console.WriteLine($"Number of Scooters Available at {args[0]} Station: " +
                                  $"{rental.GetScooterCountInStation(args[0]).Result}");
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
