using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssigningTasks.Sample.Data
{
    public class Target
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TargetId { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime LastRequest { get; set; }
    }
}
