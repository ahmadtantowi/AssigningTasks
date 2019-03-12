using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssigningTasks.Sample.Data;
using Microsoft.EntityFrameworkCore;

namespace AssigningTasks.Sample.Business
{
    public class DataBusiness : IDataBusiness
    {
        private readonly SampleDataContext _dbContext;

        public DataBusiness(SampleDataContext dbContext)
        {
            _dbContext = dbContext;
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

        public List<Transaction> GetTransactions()
        {
            try
            {
                return (from transaction in _dbContext.Transactions
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

        public Task<Transaction> ModifyTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
