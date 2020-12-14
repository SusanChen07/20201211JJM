using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels
{
    public class CUserViewModel
    {

        public CUserViewModel()
        {

        }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string ID { get; set; }
        public int Role { get; set; }
    }
}