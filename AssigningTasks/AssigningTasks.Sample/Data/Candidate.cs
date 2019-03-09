using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssigningTasks.Sample.Data
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CandidateId { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Load { get; set; }

        public bool IsAssigned { get; set; }

        public int TotalTravel { get; set; }
    }
}
