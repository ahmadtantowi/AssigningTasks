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
using OfficeOpenXml;

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

            // //Delete candidate in database
            // _dataBusiness.DeleteCandidates(_dataBusiness.GetCandidates().TakeLast(500));

            // stopWatch.Stop();
            // var elapsedTime = stopWatch.Elapsed;

            // var writed = await _dataBusiness.CreateJsonFile($"Percobaan isi target", _dataBusiness.GetTargets());

            // //Clean database
            // _ = _dataBusiness.DeleteTargets(_dataBusiness.GetTargets().Take(2));
            // _ = _dataBusiness.DeleteCandidates(_dataBusiness.GetCandidates().Take(7));
            // var modif = _dataBusiness.GetCandidates().Where(x => x.IsAssigned);
            // foreach (var item in modif)
            // {
            //     item.IsAssigned = false;
            //     _ = _dataBusiness.ModifyCandidate(item);
            // }
            // _ = _dataBusiness.DeleteTransactions(_dataBusiness.GetTransactions());

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

            var stopWatch = Stopwatch.StartNew();
            (IList<Candidate>, Candidate) assigned = assignTask.AssignTo(_dataBusiness.GetCandidates().ToLibCandidates(), currentUser, maxLoad);
            stopWatch.Stop();

            DateTime assignedTime = DateTime.Now;
            ModifyTable(assigned.Item1, assigned.Item2, currentUser, requestTime, assignedTime, stopWatch.Elapsed, algo);

            if (algo == 1)
            {
                assigned.Item1 = assigned.Item1
                    .OrderBy(x => x.DistanceToTarget)
                    .ToList();
            }
            else if (algo == 2) 
            {
                assigned.Item1 = assigned.Item1
                    .OrderBy(x => x.Load)
                    .ThenBy(x => x.DistanceToTarget)
                    .ToList();
            }

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

        public IActionResult UnassignedCandidateCount()
        {
            return Json(_dataBusiness.GetCandidates().Where(x => !x.IsAssigned).Count());
        }

        public IActionResult SelectedCandidate()
        {
            return PartialView("_SelectedCandidate", _dataBusiness.GetTransactionHistories().FirstOrDefault());
        }

        public IActionResult TransactionHistory()
        {
            return PartialView("_TransactionHistory", _dataBusiness.GetTransactionHistories());
        }

        private void ModifyTable(ICollection<Candidate> candidatesToAssign, Candidate candidate, Target target, DateTime requestTime, DateTime assignedTime, TimeSpan algoExecution, int algo)
        {
            Data.Candidate modCandidate = _dataBusiness.GetCandidate(candidate.Id);
            modCandidate.TotalTravel += (int)candidate.DistanceToTarget;
            modCandidate.IsAssigned = true;
            modCandidate.Load++;

            Data.Target modTarget = _dataBusiness.GetTarget(target.Id);
            modTarget.LastRequest = requestTime;

            string transactionId = Guid.NewGuid().ToString();

            Data.Transaction modTransaction = new Data.Transaction
            {
                TransactionId = transactionId,
                From = modTarget,
                To = modCandidate,
                RequestAt = requestTime,
                AssigneeAt = assignedTime,
                Distance = candidate.DistanceToTarget,
                AlgorithmExecutionTime = algoExecution,
                Algorithm = algo == 1 ? "Nearest Neighbor" : "Round Robin",
                Candidates = Newtonsoft.Json.JsonConvert.SerializeObject(candidatesToAssign),
            };

            _ = _dataBusiness.ModifyCandidate(modCandidate);
            _ = _dataBusiness.ModifyTarget(modTarget);
            _ = _dataBusiness.ModifyTransaction(modTransaction);
            // _ = _dataBusiness.CreateJsonFile($"Transaction_{transactionId}", candidatesToAssign);
        }

        public IActionResult GetExcelFile()
        {
            byte[] result = default(byte[]);

            using (var package = new ExcelPackage())
            {
                //New worksheet & workbook
                var worksheet = package.Workbook.Worksheets.Add("Transaksi");
                using (var cells = worksheet.Cells[1, 1, 1, 7])
                {
                    cells.Style.Font.Bold = true;
                }

                //Add headers
                worksheet.Cells[1, 1].Value = "No.";
                worksheet.Cells[1, 2].Value = "Id Transaksi";
                worksheet.Cells[1, 3].Value = "Pengguna";
                worksheet.Cells[1, 4].Value = "Karyawan";
                worksheet.Cells[1, 5].Value = "Jarak (m)";
                worksheet.Cells[1, 6].Value = "Algoritma";
                worksheet.Cells[1, 7].Value = "Lama Eksekusi (ms)";

                //Add values
                int row = 2;
                foreach (var item in _dataBusiness.GetTransactions())
                {
                    worksheet.Cells["A" + row].Value = row - 1;  
                    worksheet.Cells["B" + row].Value = item.TransactionId;  
                    worksheet.Cells["C" + row].Value = _dataBusiness.GetTarget(item.From.TargetId);  
                    worksheet.Cells["D" + row].Value = _dataBusiness.GetCandidate(item.To.CandidateId);  
                    worksheet.Cells["E" + row].Value = item.Distance;  
                    worksheet.Cells["F" + row].Value = item.Algorithm;  
                    worksheet.Cells["G" + row].Value = item.AlgorithmExecutionTime.TotalMilliseconds;  
                    row++;
                }

                result = package.GetAsByteArray();
            }

            return File(result, "application/ms-excel", "AssigningTasks.Sample.xlsx");
        }
    }
}
