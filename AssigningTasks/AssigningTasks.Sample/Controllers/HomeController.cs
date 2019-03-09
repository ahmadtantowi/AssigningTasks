using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AssigningTasks.Sample.Models;

namespace AssigningTasks.Sample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IAssignTask nn = new NearestNeighborAlgorithm();
            var nnResult = nn.AssignTo(Mock.Candidates, Mock.Targets[0]);

            IAssignTask rr = new RoundRobinAlgorithm();
            var rrResult = rr.AssignTo(Mock.Candidates, Mock.Targets[1], 3);

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
