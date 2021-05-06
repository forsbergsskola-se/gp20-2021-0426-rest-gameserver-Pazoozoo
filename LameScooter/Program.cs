using System;

namespace LameScooter
{
    class Program
    {
        static void Main(string[] args) {
            ILameScooterRental rental = new OfflineLameScooterRental("scooters.json");
            var count = rental.GetScooterCountInStation(args[0]); 
            
            if (count != null)
                Console.WriteLine($"Number of Scooters Available at {args[0]} Station: {count.Result}");
        }
    }
}
