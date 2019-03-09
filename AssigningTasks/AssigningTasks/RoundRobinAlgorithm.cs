using System;
using System.Collections.Generic;

namespace AssigningTasks
{
    public class RoundRobinAlgorithm : IAssignTask
    {
        private readonly Helpers _helpers;

        public RoundRobinAlgorithm()
        {
            _helpers = new Helpers();
        }

        public Candidate AssignTo(IList<Candidate> candidates, Target target, int maxLoad = 0)
        {
            var candidatesMinLoad = new List<Candidate>();

            foreach (Candidate candidate in candidates)
            {
                if (candidate.Load <= maxLoad)
                {
                    candidatesMinLoad.Add(candidate);
                }
            }

            var unscheduledCandidates = _helpers.SortByUnassigned(candidatesMinLoad, target);
            var candidateToAssign = unscheduledCandidates[0];

            for (int i = 0; i < unscheduledCandidates.Count; i++)
            {
                if (candidateToAssign.DistanceToTarget > unscheduledCandidates[i].DistanceToTarget)
                {
                    candidateToAssign = unscheduledCandidates[i];
                }
            }

            return candidateToAssign;
        }
    }
}
