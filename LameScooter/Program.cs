using System;

namespace LameScooter
{
    class Program
    {
        static void Main(string[] args) {
            ILameScooterRental rental = new OfflineLameScooterRental("scooters.json");

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
