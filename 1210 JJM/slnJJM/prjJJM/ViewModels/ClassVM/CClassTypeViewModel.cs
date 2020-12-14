using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels
{
    public class CClassTypeViewModel
    {
        [Required]
        public string ClassType { get; set; }
        [Required]
        public string ClassSector { get; set; }
        [Required]
        public string LevelType { get; set; }
        [Required]
        public string LocationType { get; set; }
    }
}