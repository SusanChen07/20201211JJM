using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.PointVM
{
    public class CPointViewModel
    {
        //------view------
        [DisplayName("要儲值點數的點數")]
        public int getPoint { get; set; }
        //------member------
        [DisplayName("會員ID")]
        public int memberID { get; set; }
        [DisplayName("JJM點數")]
        public Nullable<int> m_Jpoint { get; set; }
        [DisplayName("個人照片")]
        public string m_imgPath { get; set; }
        //------Deposit------
        public int depositID { get ; set; }
        [DisplayName("儲值時間")]
        public Nullable<System.DateTime> d_getNow { get; set; }
        [DisplayName("儲值方式")]
        public string d_method { get; set; }
        [DisplayName("本次儲值的點數")]
        public Nullable<int> d_point { get; set; }
        public IEnumerable<tDeposit> 儲值 { get; set; }
        public IEnumerable<tPay> 請款 { get; set; }
        public tMember 會員 { get; set; }
        public tTeacher 教練 { get; set; }
        public Nullable<System.DateTime> p_getNow { get; set; }
        public string p_method { get; set; }
        public Nullable<int> p_money { get; set; }
    }
}