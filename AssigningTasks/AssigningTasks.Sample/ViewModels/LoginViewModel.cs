using System;
using System.ComponentModel.DataAnnotations;

namespace AssigningTasks.Sample.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}
