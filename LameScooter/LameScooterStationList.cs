using System;
using System.Collections.Generic;

namespace LameScooter {
    public class LameScooterStationList {
        public string Id { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int BikesAvailable { get; set; }
        public int SpacesAvailable { get; set; }
        public int Capacity { get; set; }
        public bool AllowDropoff { get; set; }
        public bool AllowOverloading { get; set; }
        public bool IsFloatingBike { get; set; }
        public bool IsCarStation { get; set; }
        public string State { get; set; }
        public List<string> Networks { get; set; }
        public bool RealTimeData { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}