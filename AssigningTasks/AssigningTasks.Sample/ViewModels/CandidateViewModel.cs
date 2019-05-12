using System;
using System.ComponentModel;

namespace AssigningTasks.Sample.ViewModels
{
    public class CandidateViewModel
    {
        public string Id { get; set; }

        [DisplayName("Area Teknisi")]
        public string Name { get; set; }

        [DisplayName("Beban")]
        public int Load { get; set; }

        [DisplayName("Jarak")]
        public double DistanceToTarget { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [DisplayName("Sedang Bertugas")]
        public string IsAssigned { get; set; }
    }
}