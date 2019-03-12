using System;
using System.Collections.Generic;

namespace AssigningTasks
{
    public class NearestNeighborAlgorithm : IAssignTask
    {
        private readonly Helpers _helpers;

        public NearestNeighborAlgorithm()
        {
            _helpers = new Helpers();
        }

        public (IList<Candidate>, Candidate) AssignTo(IList<Candidate> candidates, Target target, int maxLoad = 0)
        {
            var unscheduledCandidates = _helpers.SortByUnassigned(candidates, target);
            var nearestCandidate = unscheduledCandidates[0];

            for (int i = 0; i < unscheduledCandidates.Count; i++)
            {
                if (nearestCandidate.DistanceToTarget > unscheduledCandidates[i].DistanceToTarget)
                {
                    nearestCandidate = unscheduledCandidates[i];
                }
            }

            return (unscheduledCandidates, nearestCandidate);
        }
    }
}
