using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjJJM.ViewModels.RatingVM
{
    public class CRatingViewModel
    {
        private tRating _rating;
        public CRatingViewModel(tRating r)
        {
            _rating = r;
        }
        public int ratingID { get { return _rating.ratingID; } set { _rating.ratingID = value; } }
        [DisplayName("會員編號")]
        [StringLength(500)]
        public Nullable<int> memberID { get { return _rating.memberID; } set { _rating.memberID = value; } }
        [DisplayName("課程編號")]
        [StringLength(500)]
        public Nullable<int> classID { get { return _rating.classID; } set { _rating.classID = value; } }
        [DisplayName("評價人")]
        [StringLength(20)]
        [Required(ErrorMessage = "請輸入評價人")] /*後端驗證*/
        public string r_send { get { return _rating.r_send; } set { _rating.r_send = value; } }
        [DisplayName("評價內容")]
        [Required(ErrorMessage = "請輸入評價內容")] /*後端驗證*/
        public string r_sendText { get { return _rating.r_sendText; } set { _rating.r_sendText = value; } }
        [DisplayName("評價分數")]
        [Required(ErrorMessage = "請輸入評價分數")] /*後端驗證*/
        public Nullable<int> r_star { get { return _rating.r_star; } set { _rating.r_star = value; } }
        [DisplayName("評價時間")]
        public Nullable<System.DateTime> r_sendTime { get { return _rating.r_sendTime; } set { _rating.r_sendTime = value; } }

    }
}