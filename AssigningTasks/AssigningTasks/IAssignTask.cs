using System;
using System.Collections.Generic;

namespace AssigningTasks
{
    public interface IAssignTask
    {
        (IList<Candidate>, Candidate) AssignTo(IList<Candidate> candidates, Target target, int maxLoad = 0);
    }
}
