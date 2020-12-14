using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.HomeVM
{
    public class popularName 
    {
        public int memberID { get; set; }
        public int classID { get; set; }
        public string c_level { get; set; }
        [DisplayName("課程名稱")]
        public string c_name { get; set; }
        [DisplayName("課程時數")]
        public int? c_hourRate { get; set; }
        [DisplayName("課程價格")]
        public int? c_price { get; set; }
        [DisplayName("課程評價")]
        public double? c_rate { get; set; }
        [DisplayName("課程評價次數")]
        public Nullable<int> c_rateTotal { get; set; }
        public string m_firstName { get; set;}
        public string m_lastName { get; set; }
        [DisplayName("課程照片1")]
        public string c_imgPath1 { get; set; }
        [DisplayName("課程照片2")]
        public string c_imgPath2 { get; set; }
        [DisplayName("教練編號")]
        public int? teacherID { get; set; }
        [DisplayName("教練頭銜")]
        public string t_title { get; set; }
        [DisplayName("教練證照列表")]
        public string t_certificateTxt { get; set; }
        [DisplayName("教練課程平均評價")]
        public Nullable<double> t_rateAvg { get; set; }
        [DisplayName("個人照片")]
        public string m_imgPath { get ; set ; }
        public int? c_discount { get; set; }
    } 

    public class CPopularHomeViewModel
    {
        public List<popularName> popularClass { get; set; }
        public List<popularName> popular1v1 { get; set; }
        public List<popularName> popularGroup { get; set; }
        public List<popularName> popularTeacher { get; set; }
        public List<popularName> allClass { get; set; }
        public tMember 會員 { get; set; }
        public tTeacher 教練 { get; set; }
        public tClass 課程 { get; set; }
    }
}