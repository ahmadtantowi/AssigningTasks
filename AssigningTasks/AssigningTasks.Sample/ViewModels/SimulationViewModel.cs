using System;
using System.Collections.Generic;

namespace AssigningTasks.Sample.ViewModels

{
    public class SimulationViewModel
    {
        // public List<Target> UserTable { get; set; }

        // public List<Candidate> EmployeeTable { get; set; }

        public List<TargetViewModel> TargetTable { get; set; }
        public List<CandidateViewModel> CandidateTable { get; set; }

        public List<TransactionHistoryViewModel> TransactionHistory { get; set; }

        public TransactionHistoryViewModel SelectedCandidate { get; set; }

        //public List<Data.Target> Users { get; set; }

        //public List<Data.Candidate> Candidates { get; set; }

        //public Data.Candidate EmployeeSelected { get; set; }
    }
}
