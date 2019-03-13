using System;
using System.ComponentModel;

namespace AssigningTasks.Sample.ViewModels
{
    public class TransactionHistoryViewModel
    {
        [DisplayName("Id Penugasan")]
        public string Id { get; set; }

        [DisplayName("Nama Pengguna")]
        public string TargetName { get; set; }

        [DisplayName("Nama Karyawan")]
        public string CandidateName { get; set; }

        [DisplayName("Jarak Tempuh")]
        public double Distance { get; set; }

        [DisplayName("Waktu Permintaan")]
        public string RequestTime { get; set; }
    }
}
