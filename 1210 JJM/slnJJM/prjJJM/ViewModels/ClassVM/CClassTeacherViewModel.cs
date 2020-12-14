using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.ClassVM
{

    public class ClassList
    {
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
        public string m_firstName { get; set; }
        public string m_lastName { get; set; }
        [DisplayName("課程照片1")]
        public string c_imgPath1 { get; set; }
        [DisplayName("課程照片2")]
        public string c_imgPath2 { get; set; }
        [DisplayName("教練編號")]
        public int? teacherID { get; set; }
        [DisplayName("搜尋標籤2")]
        public string c_lable2 { get; set; }
        [DisplayName("個人照片")]
        public string m_imgPath { get; set; }
        [DisplayName("優惠價格")]
        public Nullable<int> c_discount { get; set; }
    }

    public class CClassTeacherViewModel
    {
        public List<ClassList> Clist { get; set; }
        public List<ClassList> test { get; set; }
    }
}