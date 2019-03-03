using System;
using System.Collections.Generic;
using System.Linq;

namespace AssigningTasks
{
    public static class Task
    {
        public static IList<Candidate> Candidates =>
            new List<Candidate>()
            {
                 new Candidate()
                 {
                     Id = "C-01",
                     Load = 2,
                     Location = new Location()
                     {
                         // Jl. Tikukur No.4A, Sadang Serang, Coblong, Kota Bandung, Jawa Barat 40133
                         Latitude = -6.8972522,
                         Longitude = 107.6191821,
                     }
                 },
                 new Candidate()
                 {
                     Id = "C-02",
                     Load = 2,
                     Location = new Location()
                     {
                         // Jl. Supratman No.49, Cihapit, Bandung Wetan, Kota Bandung, Jawa Barat 40114
                         Latitude = -6.9033567,
                         Longitude = 107.6253853,
                     }
                 },
                 new Candidate()
                 {
                     Id = "C-03",
                     Load = 2,
                     Location = new Location()
                     {
                         // Jl. Sadang Saip No.22, Sadang Serang, Coblong, Kota Bandung, Jawa Barat 40133
                         Latitude = -6.8940733,
                         Longitude = 107.6225947,
                     }
                 },
                 new Candidate()
                 {
                     Id = "C-04",
                     Load = 2,
                     Location = new Location()
                     {
                         // Jl. Surapati No.129 H, Sukaluyu, Cibeunying Kaler, Kota Bandung, Jawa Barat 40123
                         Latitude = -6.8991873,
                         Longitude = 107.6254247,
                     }
                 },
                 new Candidate()
                 {
                     Id = "C-05",
                     Load = 2,
                     Location = new Location()
                     {
                         // Jl. Gagak No.43, Sadang Serang, Coblong, Kota Bandung, Jawa Barat 40133
                         Latitude = -6.8952063,
                         Longitude = 107.6244494,
                     }
                 },
            };

        public static IList<Target> Targets =>
            new List<Target>()
            {
                new Target()
                {
                    Id = "T-01",
                    RequestTime = new DateTime(2019, 03, 29, 10, 30, 0),
                    Location = new Location()
                    {
                        // Jl. Dederuk No.32, Sadang Serang, Coblong, Kota Bandung, Jawa Barat 40133
                        Latitude = -6.8986037,
                        Longitude = 107.6225108,
                    }
                }
            };
    }

    public class NearestNeighbor : IAssignTask
    {
        private Helpers _Helpers;

        public Candidate AssignTo(IList<Candidate> candidates, Target target)
        {
            _Helpers = new Helpers();

            var unscheduledCandidates = _Helpers.SortByUnassigned(Task.Candidates, Task.Targets[0]);
            var nearestToTarget = unscheduledCandidates.Min(x => x.DistanceToTarget);

            return unscheduledCandidates.FirstOrDefault(x => x.DistanceToTarget == nearestToTarget);
        }
    }

    public class RoundRobin : IAssignTask
    {
        public Candidate AssignTo(IList<Candidate> candidates, Target target)
        {
            throw new NotImplementedException();
        }
    }
}
