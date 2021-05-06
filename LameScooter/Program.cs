using System;

namespace LameScooter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);

            ILameScooterRental rental = new OfflineLameScooterRental("scooters.json"); // Replace with new XXX() later.

            // var count = await rental.GetScooterCountInStation(args[0]); 
            
            Console.WriteLine("Number of Scooters Available at this Station: "); // Add the count that is returned above to the output.
        }
    }
}
