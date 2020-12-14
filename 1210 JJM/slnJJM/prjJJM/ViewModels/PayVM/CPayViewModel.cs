using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjJJM.ViewModels.PayVM
{
    public class CPayViewModel
    {
        private tPay _Pay;
        public CPayViewModel(tPay p)
        {
            _Pay = p;
        }


        public int payID { get { return _Pay.payID; } set { _Pay.payID = value; } }
        [DisplayName("請款總額")]
        [Required(ErrorMessage = "請輸入請款總額")] /*後端驗證*/
        public Nullable<int> p_money { get { return _Pay.p_money; } set { _Pay.p_money = value; } }
        [DisplayName("請款時間")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> p_getNow { get { return _Pay.p_getNow; } set { _Pay.p_getNow = value; } }
        [DisplayName("付款狀態")]
        public string p_status { get { return _Pay.p_status; } set { _Pay.p_status = value; } }
        [DisplayName("付款方法")]
        public string p_method { get { return _Pay.p_method; } set { _Pay.p_method = value; } }
        [DisplayName("付款時間")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> p_getMoneyTime { get { return _Pay.p_getMoneyTime; } set { _Pay.p_getMoneyTime = value; } }
        [DisplayName("款項註記")]
        public string p_memo { get { return _Pay.p_memo; } set { _Pay.p_memo = value; } }
        [DisplayName("教練編號")]
        public Nullable<int> teacherID { get { return _Pay.teacherID; } set { _Pay.teacherID = value; } }

        public virtual tTeacher tTeacher { get; set; }
    }
}