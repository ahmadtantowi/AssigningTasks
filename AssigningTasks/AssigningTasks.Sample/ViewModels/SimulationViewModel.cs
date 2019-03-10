using System;
using System.Collections.Generic;

namespace AssigningTasks.Sample.ViewModels

{
    public class SimulationViewModel
    {
        public List<Candidate> EmployeeTable { get; set; }

        public List<Target> UserTable { get; set; }

        public List<string> Algorithm { get; set; }

        public List<Data.Target> Users { get; set; }

        public int MaxLoad { get; set; }

        public Data.Candidate EmployeeSelected { get; set; }
    }
}
