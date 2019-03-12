using System;
using System.Linq;
using System.Collections.Generic;

namespace AssigningTasks.Sample.Extensions
{
    public static class DataExtension
    {
        public static Candidate ToLibCandidate(this Data.Candidate candidate)
        {
            return new Candidate
            {
                Id = candidate.CandidateId,
                IsAssigned = candidate.IsAssigned,
                Load = candidate.Load,
                Location = new Location
                {
                    Latitude = candidate.Latitude,
                    Longitude = candidate.Longitude
                }
            };
        }


        public static List<Candidate> ToLibCandidates(this List<Data.Candidate> candidates)
        {
            return candidates
                .Select(c => c.ToLibCandidate())
                .ToList();
        }

        public static Target ToLibTarget(this Data.Target target)
        {
            return new Target
            {
                Id = target.TargetId,
                Location = new Location
                {
                    Latitude = target.Latitude,
                    Longitude = target.Longitude
                }
            };
        }

        public static List<Target> ToLibTargets(this List<Data.Target> targets)
        {
            return targets
                .Select(t => t.ToLibTarget())
                .ToList();
        }
    }
}
