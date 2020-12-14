using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.HomeVM
{
    public class CKeywordHomeViewModel
    {
        [Required]
        public string SearchKey { get; set; }
    }
}