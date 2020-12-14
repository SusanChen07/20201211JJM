using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjJJM.ViewModels.RatingVM
{
    public class CRatingMgtViewModel
    {
        public List<tRating> ratingData { get; set; }

        public tClass classData { get; set; }

        public int countS { get; set; }

    }
}