using System;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);

            ILameScooterRental rental = null; // Replace with new XXX() later.

            // var count = await rental.GetScooterCountInStation(args[0]); 
            
            Console.WriteLine("Number of Scooters Available at this Station: "); // Add the count that is returned above to the output.
        }
    }

    public interface ILameScooterRental {
        Task<int> GetScooterCountInStation(string stationName);
    }
}
