using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjJJM.ViewModels.PayVM
{
    public class CPayMgtViewModel
    {
        private tPay _Pay;
        public CPayMgtViewModel(tPay p)
        {
            _Pay = p;
        }

        public int payID { get { return _Pay.payID; } set { _Pay.payID = value; } }
        [DisplayName("請款總額")]
        public Nullable<int> p_money { get; set; }
        [DisplayName("請款時間")]
        public Nullable<System.DateTime> p_getNow { get; set; }
        [DisplayName("付款狀態")]
        public string p_status { get; set; }
        [DisplayName("付款方法")]
        public string p_method { get; set; }
        [DisplayName("付款時間")]
        public Nullable<System.DateTime> p_getMoneyTime { get; set; }
        [DisplayName("款項註記")]
        public string p_memo { get; set; }
        [DisplayName("教練編號")]
        public Nullable<int> teacherID { get; set; }

        public virtual tTeacher tTeacher { get; set; }
    }
}