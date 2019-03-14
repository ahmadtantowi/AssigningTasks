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
            //var candidates = _dataBusiness
            //    .GetCandidates()
            //    .ToLibCandidates();

            //var targets = _dataBusiness
            //    .GetTargets()
            //    .ToLibTargets();

            //var simulationVM = new SimulationViewModel
            //{
            //    EmployeeTable = candidates,
            //    UserTable = targets,
            //    Candidates = _dataBusiness.GetCandidates(),
            //};

            //IAssignTask nn = new NearestNeighborAlgorithm();
            //ViewBag.NnRequest = targets[0];
            //ViewBag.NnResult = nn.AssignTo(candidates, targets[0]);

            //IAssignTask rr = new RoundRobinAlgorithm();
            //ViewBag.RrRequest = targets[1];
            //ViewBag.RrResult = rr.AssignTo(candidates, targets[1], maxLoad: 3);

            return View(new SimulationViewModel()
            {
                UserTable = _dataBusiness.GetTargets().ToLibTargets(),
                EmployeeTable = new List<Candidate>(),
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

            return PartialView("_CandidatesToAssign", assigned.Item1);
        }

        public IActionResult SelectedCandidate()
        {
            var ata =  PartialView("_SelectedCandidate", _dataBusiness.GetTransactionHistories().FirstOrDefault());return ata;
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
