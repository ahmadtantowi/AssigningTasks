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
        private readonly IHereMaps _hereMaps;

        public AssigneeController(IDataBusiness dataBusiness, IHereMaps hereMaps)
        {
            _dataBusiness = dataBusiness;
            _hereMaps = hereMaps;
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

            // //New candidates data
            // var candidates = _dataBusiness.GetCandidates();
            // var transactions = _dataBusiness.GetTransactions();
            // await _dataBusiness.DeleteCandidates(candidates);
            // await _dataBusiness.DeleteTransactions(transactions);

            // candidates = await Helpers.GeneratorHelper.GetCandidatesFromMinimarket(_hereMaps);
            // candidates.ForEach(c => _dataBusiness.ModifyCandidate(c));
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
            ModifyTable(assigned.Item1, assigned.Item2, currentUser, requestTime, assignedTime, stopWatch.Elapsed, algo, maxLoad);

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

            ViewBag.LoadVisibility = algo == 2 ? "visible" : "hidden";
            return PartialView("_CandidatesToAssign", assigned.Item1
                .Select(x => new CandidateViewModel
                {
                    Id = x.Id,
                    Name = _dataBusiness.GetCandidate(x.Id).Name,
                    DistanceToTarget = x.DistanceToTarget,
                    Load = x.Load,
                    Latitude = x.Location.Latitude,
                    Longitude = x.Location.Longitude, 
                    IsAssigned = x.IsAssigned ? "Ya" : "Tidak"
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

        public IActionResult TransactionDetail(string id)
        {
            var targets = _dataBusiness.GetCandidates();

            return View(new TransactionDetailViewModel
            {
                TransactionHistory = _dataBusiness.GetTransactionHistory(id),
                Candidates = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<Candidate>>(_dataBusiness.GetTransaction(id).Candidates)
                    .Select(x => new CandidateViewModel
                    {
                        Id = x.Id,
                        DistanceToTarget = x.DistanceToTarget,
                        Latitude = x.Location.Latitude,
                        Longitude = x.Location.Longitude,
                        Load = x.Load,
                        Name = targets.Where(y => y.CandidateId.Equals(x.Id)).FirstOrDefault()?.Name
                    })
                    .OrderBy(x => x.DistanceToTarget)
                    .ToList()
            });
        }

        private void ModifyTable(ICollection<Candidate> candidatesToAssign, Candidate candidate, Target target, DateTime requestTime, DateTime assignedTime, TimeSpan algoExecution, int algo, int maxLoad)
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
                MaxLoad = maxLoad,
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
                foreach (var item in _dataBusiness.GetTransactionHistories().OrderBy(x => x.RequestDateTime))
                {
                    worksheet.Cells["A" + row].Value = row - 1;  
                    worksheet.Cells["B" + row].Value = item.Id;  
                    worksheet.Cells["C" + row].Value = item.TargetName;  
                    worksheet.Cells["D" + row].Value = item.CandidateName;  
                    worksheet.Cells["E" + row].Value = item.Distance;  
                    worksheet.Cells["F" + row].Value = item.Algorithm;  
                    worksheet.Cells["G" + row].Value = item.AlgorithmExecutionTime;  
                    row++;
                }

                result = package.GetAsByteArray();
            }

            return File(result, "application/ms-excel", "AssigningTasks.Sample.xlsx");
        }

        public IActionResult GetTransactionDetailExcelFile(int take = 5)
        {
            byte[] result = default(byte[]);
            var candidates = _dataBusiness.GetCandidates();
            List<TransactionHistoryViewModel> transactions = default(List<TransactionHistoryViewModel>);
            int maxLoad = 0;

            using (var package = new ExcelPackage())
            {

                for (int i = 0; i < 5; i++)
                {
                    //New worksheet & workbook
                    maxLoad = i == 0 ? 0 : maxLoad + 5;
                    var worksheet = package.Workbook.Worksheets.Add(i == 0 ? "Nearest Neighbor" : $"Round Robin (beban={maxLoad})");
                    int row = 1;

                    if (i == 0)
                    {
                        transactions = _dataBusiness.GetTransactionHistories()
                            .Where(x => x.Algorithm.Equals("Nearest Neighbor"))
                            .ToList();
                    }
                    else
                    {
                        transactions = _dataBusiness.GetTransactionHistories()
                            .Where(x => x.Algorithm.Equals("Round Robin") && x.MaxLoad == maxLoad)
                            .ToList();
                    }

                    foreach (var item in transactions.OrderBy(x => x.RequestDateTime))
                    {
                        //Detail
                        worksheet.Cells.Style.Font.Size = 7;
                        worksheet.Cells[$"A{row}:G{row}"].Merge = true;
                        worksheet.Cells[$"A{row}"].Style.WrapText = true;
                        worksheet.Cells[$"A{row}"].Value = $"IdPenugasan: {item.Id}, Pengguna: {item.TargetName}, Karyawan: {item.CandidateName}, " +
                            $"Jarak: {item.Distance}m, Waktu: {item.RequestTime}, Algoritma: {item.Algorithm}, Beban Maks.: {item.MaxLoad}, Lama: {item.AlgorithmExecutionTime}ms";
                        worksheet.Row(row).Height = 20;
                        row ++;
                        
                        //Add headers
                        worksheet.Cells[row, 1].Value = "No.";
                        worksheet.Cells[row, 2].Value = "Id";
                        worksheet.Cells[row, 3].Value = "Karyawan";
                        worksheet.Cells[row, 4].Value = "Latitude";
                        worksheet.Cells[row, 5].Value = "Longitue";
                        worksheet.Cells[row, 6].Value = "Jarak";
                        worksheet.Cells[row, 7].Value = "Beban";

                        var details = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<Candidate>>(_dataBusiness.GetTransaction(item.Id).Candidates);
                        var currRow = 1;
                        row++;

                        //Add values
                        foreach (var detail in details.OrderBy(x => x.DistanceToTarget).Take(take))
                        {
                            worksheet.Cells["A" + row].Value = currRow;  
                            worksheet.Cells["B" + row].Value = detail.Id;  
                            worksheet.Cells["C" + row].Value = candidates.Where(x => x.CandidateId.Equals(detail.Id)).FirstOrDefault().Name;
                            worksheet.Cells["D" + row].Value = detail.Location.Latitude;  
                            worksheet.Cells["E" + row].Value = detail.Location.Longitude;  
                            worksheet.Cells["F" + row].Value = detail.DistanceToTarget;  
                            worksheet.Cells["G" + row].Value = detail.Load; 
                            currRow++; 
                            row++;
                        }
                        worksheet.Cells[$"A{row}:G{row}"].Merge = true;
                        row++;
                    }
                }
                result = package.GetAsByteArray();
            }

            return File(result, "application/ms-excel", "AssigningTasks.Sample.TransactionDetails.xlsx");
        }
    }
}
