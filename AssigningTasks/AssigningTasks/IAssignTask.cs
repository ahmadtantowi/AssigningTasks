using System;
using System.Collections.Generic;

namespace AssigningTasks
{
    public interface IAssignTask
    {
        Candidate AssignTo(IList<Candidate> candidates, Target target);
    }
}
