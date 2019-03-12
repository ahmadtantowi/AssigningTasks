using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data = AssigningTasks.Sample.Data;

namespace AssigningTasks.Sample.Business
{
    public interface IDataBusiness
    {
        Data.Target GetTarget(string id);

        List<Data.Target> GetTargets();

        Data.Candidate GetCandidate(string id);

        List<Data.Candidate> GetCandidates();

        List<Data.Transaction> GetTransactions();

        Task<Data.Target> ModifyTarget(Data.Target target);

        Task<Data.Candidate> ModifyCandidate(Data.Candidate candidate);

        Task<Data.Transaction> ModifyTransaction(Data.Transaction transaction);
    }
}
