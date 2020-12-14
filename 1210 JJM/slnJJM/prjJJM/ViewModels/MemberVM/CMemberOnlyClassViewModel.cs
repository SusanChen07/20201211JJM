using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.MemberVM
{
    public class CMemberOnlyClassViewModel
    {
        public IEnumerable<tClass> classData
        {
            get;
            set;
        }

        public IEnumerable<tOrder_Detail> orderDetail
        {
            get;
            set;
        }
        public IEnumerable<tOrder> Order
        {
            get;
            set;
        }

        public tMember memberData
        {
            get;
            set;
        }
        public tClass ClassID
        {
            get;
            set;
        }
		
		        public IEnumerable<tMember> memberName
        {
            get;
            set;
        }
		
        public tRating RatingData
        {
            get;
            set;
        }

        public List<tRating> tRatingData
        {
            get; set;
        }

        public List<ratingList> CRatingListData
        {
            get; set;
        }

    }

    public class ratingList
    {
        [DisplayName("評價人")]
        public string r_send { get; set; }
        [DisplayName("評價內容")]
        public string r_sendText { get; set; }
        [DisplayName("評價分數")]
        public int r_star { get; set; }
        [DisplayName("評價時間")]
        public int r_sendTime { get; set; }

    }
}