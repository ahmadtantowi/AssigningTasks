using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AssigningTasks.Sample.Models;
using AssigningTasks.Sample.Business;
using AssigningTasks.Sample.ViewModels;

namespace AssigningTasks.Sample.Controllers
{
    public class AssigneeController : Controller
    {
        private readonly IDataBusiness _dataBusiness;

        public AssigneeController(IDataBusiness dataBusiness)
        {
            _dataBusiness = dataBusiness;
        }

        public IActionResult Simulation()
        {
            var candidates = _dataBusiness.GetCandidates()
                .Select(c => new Candidate
                {
                    Id = c.CandidateId,
                    IsAssigned = c.IsAssigned,
                    Load = c.Load,
                    Location = new Location
                    {
                        Latitude = c.Latitude,
                        Longitude = c.Longitude
                    }
                })
                .ToList();
            var targets = _dataBusiness.GetTargets()
                .Select(t => new Target
                {
                    Id = t.TargetId,
                    Location = new Location
                    {
                        Latitude = t.Latitude,
                        Longitude = t.Longitude
                    }
                })
                .ToList();

            var simulationVM = new SimulationViewModel
            {
                EmployeeTable = candidates,
                UserTable = targets
            };

            IAssignTask nn = new NearestNeighborAlgorithm();
            ViewBag.NnRequest = targets[0];
            ViewBag.NnResult = nn.AssignTo(candidates, targets[0]);

            IAssignTask rr = new RoundRobinAlgorithm();
            ViewBag.RrRequest = targets[1];
            ViewBag.RrResult = rr.AssignTo(candidates, targets[1], maxLoad: 3);

            return View(simulationVM);
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
    }
}
