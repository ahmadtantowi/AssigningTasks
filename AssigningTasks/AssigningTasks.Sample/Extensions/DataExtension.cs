using AssigningTasks.Sample.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AssigningTasks.Sample.Extensions
{
    public static class DataExtension
    {
        private static Random _rnd;

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

        public static Data.Candidate ToCandidate(this Item item) 
        {
            _rnd = _rnd ?? new Random();
            int load = _rnd.Next(0, 10);

            return new Data.Candidate
            {
                Name = $"{item.title}, {item.vicinity.Replace("<br/>", ", ")}",
                Latitude = item.position[0],
                Longitude = item.position[1],
                Load = load,
                TotalTravel = Helpers.GeneratorHelper.GenerateTotalTravel(load),
            };
        }
    }
}
