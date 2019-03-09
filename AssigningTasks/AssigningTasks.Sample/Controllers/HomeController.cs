using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AssigningTasks.Sample.Models;
using AssigningTasks.Sample.Business;

namespace AssigningTasks.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataBusiness _dataBusiness;

        public HomeController(IDataBusiness dataBusiness)
        {
            _dataBusiness = dataBusiness;
        }

        public IActionResult Index()
        {
            IAssignTask nn = new NearestNeighborAlgorithm();
            var nnResult = nn.AssignTo(Mock.Candidates, Mock.Targets[0]);

            IAssignTask rr = new RoundRobinAlgorithm();
            var rrResult = rr.AssignTo(Mock.Candidates, Mock.Targets[1], 3);

            //List<Data.Candidate> mockCandidates = 
            //    new List<Data.Candidate>(Mock.Candidates
            //    .Select(c => new Data.Candidate
            //    {
            //        Load = c.Load,
            //        Latitude = c.Location.Latitude,
            //        Longitude = c.Location.Longitude
            //    }));
            //string[] nameCandidates = { "Abdul", "Bambang", "Chairul", "Deni", "Edo" };
            //int[] travelCandidates = { 1520, 300, 610, 1280, 490 };

            //for (int i = 0; i < mockCandidates.Count ; i++)
            //{
            //    mockCandidates[i].Name = nameCandidates[i];
            //    mockCandidates[i].TotalTravel = travelCandidates[i];

            //    var candidate = _dataBusiness.ModifyCandidate(mockCandidates[i]);
            //}

            //List<Data.Target> mockTargets =
            //    new List<Data.Target>(Mock.Targets
            //    .Select(t => new Data.Target
            //    {
            //        Latitude = t.Location.Latitude,
            //        Longitude = t.Location.Longitude,
            //    }));
            //string[] nameTargets = { "Ujang", "Udin" };

            //for (int i = 0; i < mockTargets.Count; i++)
            //{
            //    mockTargets[i].Name = nameTargets[i];

            //    var target = _dataBusiness.ModifyTarget(mockTargets[i]);
            //}

            var candidates = _dataBusiness.GetCandidates();
            var targets = _dataBusiness.GetTargets();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
