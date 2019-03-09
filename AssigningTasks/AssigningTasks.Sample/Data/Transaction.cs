using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssigningTasks.Sample.Data
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TransactionId { get; set; }

        public virtual Target From { get; set; }

        public virtual Candidate To { get; set; }

        public double Distance { get; set; }

        public bool IsFinished { get; set; }

        public DateTime RequestAt { get; set; }

        public DateTime AssigneeAt { get; set; }

        public DateTime FinishAt { get; set; }
    }
}
