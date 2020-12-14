using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjJJM.ViewModels.DepositVM
{
    public class CDepositViewModel
    {
        private tDeposit _deposit;
        public CDepositViewModel(tDeposit d)
        {
            _deposit = d;
        }
        public int depositID { get { return _deposit.depositID; } set { _deposit.depositID = value; } }
        [DisplayName("會員編號")]
        [StringLength(500)]
        public Nullable<int> memberID { get { return _deposit.memberID; } set { _deposit.memberID = value; } }
        [DisplayName("儲值點數")]
        [Required(ErrorMessage = "請輸入儲值點數")] /*後端驗證*/
        public Nullable<int> d_point { get { return _deposit.d_point; } set { _deposit.d_point = value; } }
        [DisplayName("儲值方式")]
        /*[Required(ErrorMessage = "請輸入儲值方式")]*/ /*後端驗證*/
        public string d_method { get { return _deposit.d_method; } set { _deposit.d_method = value; } }
        [DisplayName("儲值註記")]
        /*[Required(ErrorMessage = "請輸入儲值註記")]*/ /*後端驗證*/
        public string d_memo { get { return _deposit.d_memo; } set { _deposit.d_memo = value; } }        
        [DisplayName("儲值時間")]
        public Nullable<System.DateTime> d_getNow { get { return _deposit.d_getNow; } set { _deposit.d_getNow = value; } }
    }
}