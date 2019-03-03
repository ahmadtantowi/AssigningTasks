using System;
using System.Collections.Generic;
using System.Linq;

namespace AssigningTasks
{
    public class Helpers
    {
        private readonly double _R;
        private IList<Candidate> _Candidates;

        public Helpers(double R = 6371e3)
        {
            _R = R;
        }

        public IList<Candidate> SortByUnassigned(IList<Candidate> candidates, Target target)
        {
            _Candidates = new List<Candidate>();

            foreach (Candidate candidate in candidates)
            {
                if (!candidate.IsAssigned)
                {
                    candidate.DistanceToTarget = Math.Round(Distance(candidate.Location, target.Location));
                    _Candidates.Add(candidate);
                }
            }

            return _Candidates;
        }

        private double Distance(Location source, Location destination)
        {
            double Olat1 = source.Latitude.DegreeToRadian();
            double Olat2 = destination.Latitude.DegreeToRadian();

            double nLng1 = source.Longitude.DegreeToRadian();
            double nLng2 = destination.Longitude.DegreeToRadian();

            double Qo = Olat2 - Olat1;
            double Qn = nLng2 - nLng1;

            var a = Math.Sin(Qo / 2) * Math.Sin(Qo / 2) +
                Math.Cos(Olat1) * Math.Cos(Olat2) *
                Math.Sin(Qn / 2) * Math.Sin(Qn / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return _R * c;
        }
    }
}
