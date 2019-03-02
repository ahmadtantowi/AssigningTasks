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
                         // Jl. Puyuh 15-55, Sadang Serang, Coblong, Kota Bandung, Jawa Barat 40133
                         Latitude = -6.8967153,
                         Longitude = 107.6218184,
                     }
                 },
                 new Candidate()
                 {
                     Id = "C-03",
                     Load = 2,
                     Location = new Location()
                     {
                         // Jl. Diponegoro No.34, RW.12, Citarum, Bandung Wetan, Kota Bandung, Jawa Barat 40115
                         Latitude = -6.9013799,
                         Longitude = 107.6212204,
                     }
                 },
                 new Candidate()
                 {
                     Id = "C-04",
                     Load = 2,
                     Location = new Location()
                     {
                         // Gg. Tilil II 28, Sukaluyu, Cibeunying Kaler, Kota Bandung, Jawa Barat
                         Latitude = -6.8976273,
                         Longitude = 107.6237703,
                     }
                 },
                 new Candidate()
                 {
                     Id = "C-05",
                     Load = 2,
                     Location = new Location()
                     {
                         // Jl. Melania 1, Cihaur Geulis, Cibeunying Kaler, Kota Bandung, Jawa Barat 40122
                         Latitude = -6.9009291,
                         Longitude = 107.6224671,
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
