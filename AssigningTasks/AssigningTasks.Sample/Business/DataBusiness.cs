﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssigningTasks.Sample.Data;
using AssigningTasks.Sample.ViewModels;
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
                            CandidateId = ts.To.CandidateId
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
    }
}
