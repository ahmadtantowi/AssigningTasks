using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssigningTasks.Sample.ViewModels;
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

        Data.Transaction GetTransaction(string id);

        List<TransactionHistoryViewModel> GetTransactionHistories();

        TransactionHistoryViewModel GetTransactionHistory(string id);

        Task<Data.Target> ModifyTarget(Data.Target target);

        Task<Data.Candidate> ModifyCandidate(Data.Candidate candidate);

        Task<Data.Transaction> ModifyTransaction(Data.Transaction transaction);

        Task DeleteTargets(IEnumerable<Data.Target> targets);

        Task DeleteCandidates(IEnumerable<Data.Candidate> candidates);

        Task DeleteTransactions(IEnumerable<Data.Transaction> transactions);

        Task<(bool, string)> CreateJsonFile(string fileName, object rawObject);

        Task<(bool, string, T)> GetJsonFile<T>(string fileName);

        (bool, string) DeleteJsonFile(string fileName);
    }
}
