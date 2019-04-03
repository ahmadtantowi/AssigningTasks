using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AssigningTasks.Sample.Models;
using AssigningTasks.Sample.Business;
using AssigningTasks.Sample.ViewModels;
using AssigningTasks.Sample.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AssigningTasks.Sample.Controllers
{
    [Authorize]
    public class AssigneeController : Controller
    {
        private readonly IDataBusiness _dataBusiness;

        public AssigneeController(IDataBusiness dataBusiness)
        {
            _dataBusiness = dataBusiness;
        }

        public IActionResult Simulation()
        {
            #if DEBUG
            // var stopWatch = Stopwatch.StartNew();

            // //Seed target
            // for (int i = 0; i < 50; i++)
            // {
            //     var location = Helpers.GeneratorHelper.GenerateNearbyLocation(-6.8986037, 107.6225108, 10000);
            //     var target = new Data.Target
            //     {
            //         Name = Helpers.GeneratorHelper.GenerateName(7),
            //         Latitude = location.Latitude,
            //         Longitude = location.Longitude
            //     };
            //     _dataBusiness.ModifyTarget(target);
            // }

            // //Seed candidate
            // for (int i = 0; i < 1000; i++)
            // {
            //     var load = Helpers.GeneratorHelper.GenerateLoad(0, 20);
            //     var location = Helpers.GeneratorHelper.GenerateNearbyLocation(-6.8986037, 107.6225108, 10000);
            //     var candidate = new Data.Candidate
            //     {
            //         Name = Helpers.GeneratorHelper.GenerateName(7),
            //         Latitude = location.Latitude,
            //         Longitude = location.Longitude,
            //         Load = load,
            //         TotalTravel = Helpers.GeneratorHelper.GenerateTotalTravel(load)
            //     };
            //     _dataBusiness.ModifyCandidate(candidate);
            // }

            // stopWatch.Stop();
            // var elapsedTime = stopWatch.Elapsed;
            #endif

            return View(new SimulationViewModel()
            {
                TargetTable = _dataBusiness.GetTargets()
                    .Select(x => new TargetViewModel
                    {
                        Id = x.TargetId,
                        Name = x.Name,
                        Latitude = x.Latitude,
                        Longitude = x.Longitude
                    })
                    .ToList(),
                CandidateTable = new List<CandidateViewModel>(),
                TransactionHistory = _dataBusiness.GetTransactionHistories(),
                SelectedCandidate = new TransactionHistoryViewModel()
            });
        }

        public IActionResult Candidate()
        {
            var candidates = _dataBusiness.GetCandidates();
            return View(candidates);
        }

        public IActionResult Target()
        {
            var targets = _dataBusiness.GetTargets();
            return View(targets);
        }

        public IActionResult RequestCandidate(int algo, int maxLoad, string id)
        {
            IAssignTask assignTask = default(IAssignTask);
            DateTime requestTime = DateTime.Now;
            Target currentUser = _dataBusiness.GetTarget(id).ToLibTarget();

            if (algo == 1)
            {
                assignTask = new NearestNeighborAlgorithm();
            }
            else if (algo == 2)
            {
                assignTask = new RoundRobinAlgorithm();
            }

            (IList<Candidate>, Candidate) assigned = assignTask.AssignTo(_dataBusiness.GetCandidates().ToLibCandidates(), currentUser, maxLoad);
            DateTime assignedTime = DateTime.Now;

            _ = ModifyTable(assigned.Item2, currentUser, requestTime, assignedTime);

            return PartialView("_CandidatesToAssign", assigned.Item1
                .Select(x => new CandidateViewModel
                {
                    Id = x.Id,
                    Name = _dataBusiness.GetCandidate(x.Id).Name,
                    DistanceToTarget = x.DistanceToTarget,
                    Load = x.Load,
                    Latitude = x.Location.Latitude,
                    Longitude = x.Location.Longitude
                })
                .ToList());
        }

        public IActionResult SelectedCandidate()
        {
            return PartialView("_SelectedCandidate", _dataBusiness.GetTransactionHistories().FirstOrDefault());
        }

        public IActionResult TransactionHistory()
        {
            return PartialView("_TransactionHistory", _dataBusiness.GetTransactionHistories());
        }

        private async Task ModifyTable(Candidate candidate, Target target, DateTime requestTime, DateTime assignedTime)
        {
            Data.Candidate modCandidate = _dataBusiness.GetCandidate(candidate.Id);
            modCandidate.TotalTravel += (int)candidate.DistanceToTarget;
            modCandidate.IsAssigned = true;
            modCandidate.Load++;

            Data.Target modTarget = _dataBusiness.GetTarget(target.Id);
            modTarget.LastRequest = requestTime;

            Data.Transaction modTransaction = new Data.Transaction
            {
                From = modTarget,
                To = modCandidate,
                RequestAt = requestTime,
                AssigneeAt = assignedTime,
                Distance = candidate.DistanceToTarget,
            };

            await _dataBusiness.ModifyCandidate(modCandidate);
            await _dataBusiness.ModifyTarget(modTarget);
            await _dataBusiness.ModifyTransaction(modTransaction);
        }
    }
}
