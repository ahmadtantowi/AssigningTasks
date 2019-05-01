﻿using System;
using System.ComponentModel;

namespace AssigningTasks.Sample.ViewModels
{
    public class TransactionHistoryViewModel
    {
        [DisplayName("Id Penugasan")]
        public string Id { get; set; }

        [DisplayName("Id Pengguna")]
        public string TargetId { get; set; }

        [DisplayName("Nama Pengguna")]
        public string TargetName { get; set; }

        [DisplayName("Id Minimarket")]
        public string CandidateId { get; set; }

        [DisplayName("Minimarket")]
        public string CandidateName { get; set; }

        [DisplayName("Jarak Tempuh")]
        public double Distance { get; set; }

        [DisplayName("Waktu Permintaan")]
        public string RequestTime { get; set; }

        [DisplayName("Algoritma")]
        public string Algorithm { get; set; }

        [DisplayName("Lama Eksekusi")]
        public double AlgorithmExecutionTime { get; set; }

        [DisplayName("Beban maksimal")]
        public int MaxLoad { get; set; }

        public DateTime RequestDateTime { get; set; }
    }
}
