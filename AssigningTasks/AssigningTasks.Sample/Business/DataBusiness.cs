using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AssigningTasks.Sample.Data;
using AssigningTasks.Sample.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AssigningTasks.Sample.Business
{
    public class DataBusiness : IDataBusiness
    {
        private readonly string _filePath;
        private readonly SampleDataContext _dbContext;
        private readonly IHostingEnvironment _environment;

        public DataBusiness(SampleDataContext dbContext, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
            _filePath = Path.Combine(_environment.ContentRootPath, "Files");
        }

        public Data.Candidate GetCandidate(string id)
        {
            try
            {
                return (from candidate in _dbContext.Candidates
                        where candidate.CandidateId.Equals(id)
                        select candidate)
                        .FirstOrDefault();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public List<Data.Candidate> GetCandidates()
        {
            try
            {
                return (from candidate in _dbContext.Candidates
                        select candidate).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Data.Target GetTarget(string id)
        {
            try
            {
                return (from target in _dbContext.Targets
                        where target.TargetId.Equals(id)
                        select target)
                        .FirstOrDefault();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public List<Data.Target> GetTargets()
        {
            try
            {
                return (from target in _dbContext.Targets
                        select target).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public List<TransactionHistoryViewModel> GetTransactionHistories()
        {
            try
            {
                return (from ts in _dbContext.Transactions
                        join t in _dbContext.Targets on ts.From.TargetId equals t.TargetId
                        join c in _dbContext.Candidates on ts.To.CandidateId equals c.CandidateId
                        orderby ts.RequestAt descending
                        select new TransactionHistoryViewModel
                        {
                            Id = ts.TransactionId,
                            TargetName = t.Name,
                            CandidateName = c.Name,
                            Distance = ts.Distance,
                            RequestTime = ts.RequestAt.ToString("yy-MMM-dd HH:mm"),
                            TargetId = ts.From.TargetId,
                            CandidateId = ts.To.CandidateId,
                            Algorithm = ts.Algorithm,
                            AlgorithmExecutionTime = ts.AlgorithmExecutionTime.TotalMilliseconds
                        })
                        .ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public List<Transaction> GetTransactions()
        {
            try
            {
                return (from transaction in _dbContext.Transactions
                        orderby transaction.RequestAt descending
                        select transaction).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<Data.Candidate> ModifyCandidate(Data.Candidate candidate)
        {
            try
            {
                Data.Candidate existCandidate = 
                    await _dbContext
                    .Candidates
                    .SingleOrDefaultAsync(c => c.CandidateId.Equals(candidate.CandidateId));

                if (existCandidate == null)
                {
                    await _dbContext.Candidates.AddAsync(candidate);
                }
                else
                {
                    _dbContext.Candidates.Update(candidate);
                }

                await _dbContext.SaveChangesAsync();
                return existCandidate ?? candidate;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<Data.Target> ModifyTarget(Data.Target target)
        {
            try
            {
                Data.Target existTarget =
                    await _dbContext
                    .Targets
                    .SingleOrDefaultAsync(t => t.TargetId.Equals(target.TargetId));

                if (existTarget == null)
                {
                    await _dbContext.Targets.AddAsync(target);
                }
                else
                {
                    _dbContext.Targets.Update(target);
                }

                await _dbContext.SaveChangesAsync();
                return existTarget ?? target;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<Transaction> ModifyTransaction(Transaction transaction)
        {
            try
            {
                Data.Transaction existTransaction =
                    await _dbContext
                    .Transactions
                    .SingleOrDefaultAsync(t => t.TransactionId.Equals(transaction.TransactionId));

                if (existTransaction == null)
                {
                    await _dbContext.Transactions.AddAsync(transaction);
                }
                else
                {
                    _dbContext.Transactions.Update(transaction);
                }

                await _dbContext.SaveChangesAsync();
                return existTransaction ?? transaction;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public  async Task DeleteCandidates(IEnumerable<Data.Candidate> candidates)
        {
            try
            {
                _dbContext.Candidates.RemoveRange(candidates);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task DeleteTargets(IEnumerable<Data.Target> targets)
        {
            try
            {
                _dbContext.Targets.RemoveRange(targets);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task DeleteTransactions(IEnumerable<Data.Transaction> transactions)
        {
            try
            {
                _dbContext.Transactions.RemoveRange(transactions);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<(bool, string)> CreateJsonFile(string fileName, object rawObject)
        {
            string fileNameFull = Path.Combine(_filePath, $"{fileName}.json");

            try
            {
                if (!Directory.Exists(_filePath))
                {
                    Directory.CreateDirectory(_filePath);
                }
                await File.WriteAllTextAsync(fileNameFull, JsonConvert.SerializeObject(rawObject));

                return (true, $"{nameof(CreateJsonFile)}: {fileNameFull} Sukses bos!");
            }
            catch (System.Exception exc)
            {
                return (false, $"{nameof(CreateJsonFile)}: {fileNameFull} Gagal bos! {exc.Message}");
            }
        }

        public async Task<(bool ,string, T)> GetJsonFile<T>(string fileName)
        {
            string fileNameFull = Path.Combine(_filePath, $"{fileName}.json");

            try
            {
                return (true,
                    $"{nameof(GetJsonFile)}: {fileNameFull} Sukses bos!",
                    JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(Path.Combine(_filePath, fileName))));
            }
            catch (System.Exception exc)
            {
                return (false, $"{nameof(GetJsonFile)}: {fileNameFull} Gagal bos! {exc.Message}", default(T));
            }
        }

        public (bool, string) DeleteJsonFile(string fileName)
        {
            string fileNameFull = Path.Combine(_filePath, $"{fileName}.json");

            try
            {
                File.Delete(fileNameFull);

                return (true, $"{nameof(DeleteJsonFile)}: {fileNameFull} Sukses bos!");
            }
            catch (System.Exception exc)
            {
                return (false, $"{nameof(DeleteJsonFile)}: {fileNameFull} Gagal bos! {exc.Message}");
            }
        }
    }
}
