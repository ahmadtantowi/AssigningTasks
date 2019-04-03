using System;
using System.ComponentModel;

namespace AssigningTasks.Sample.ViewModels
{
    public class TargetViewModel
    {
        public string Id { get; set; }

        [DisplayName("Nama Pangguna")]
        public string Name { get; set; }
        
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}