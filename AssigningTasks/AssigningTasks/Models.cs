using System;
using System.Collections.Generic;

namespace AssigningTasks
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Target
    {
        public string Id { get; set; }
        public DateTime RequestTime { get; set; }
        public Location Location { get; set; }
    }

    public class Candidate
    {
        public string Id { get; set; }
        public int Load { get; set; }
        public double DistanceToTarget { get; set; }
        public bool IsAssigned { get; set; }
        public Location Location { get; set; }
    }
}
