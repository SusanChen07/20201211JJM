using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Xunit.Sdk;

namespace prjJJM.ViewModels.ClassVM
{
    public class CClassMgtImgViewModel
    {
        public List<classMgtList> classData { get; set; }
        public List<ratingList> ratingData { get; set; }
		
        public string keywordPost { get; set; }
        public string 種類Post { get; set; }
        public DateTime startdatePost { get; set; }
        public DateTime enddatePost { get; set; }
        public string areaPost { get; set; }
        public string optionsRadios { get; set; }
        public int classID { get; set; }
        [DisplayName("課程名稱")]
        public string c_name { get; set; }
    }

    public class classMgtList
    {
        public int classID { get; set; }
        [DisplayName("課程照片1")]
        public string c_imgPath1 { get; set; }
        [DisplayName("教練編號")]
        public Nullable<int> teacherID { get; set; }
        [DisplayName("教練證照列表")]
        public string t_certificateTxt { get; set; }
        [DisplayName("教練頭銜")]
        public string t_title { get; set; }
        public int memberID { get; set; }
        [DisplayName("個人照片")]
        public string m_imgPath { get; set; }

        [DisplayName("課程名稱")]
        public string c_name { get; set; }
        [DisplayName("課程副標")]
        public string c_nameText { get; set; }
        [DisplayName("課程簡介")]
        public string c_intro { get; set; }
        [DisplayName("教室位置")]
        public string c_location { get; set; }
        [DisplayName("課程分級")]
        public string c_level { get; set; }
        [DisplayName("上課基本要求")]
        public string c_requirement { get; set; }
        [DisplayName("開課時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_startTime { get; set; }
        [DisplayName("完課時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_endTime { get; set; }
        [DisplayName("課程人數上限")]
        public Nullable<int> c_maxStudent { get; set; }
        [DisplayName("最少招生人數")]
        public Nullable<int> c_minStudent { get; set; }
        [DisplayName("報名結束時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_registerEnd { get; set; }
        [DisplayName("課程照片2")]
        public string c_imgPath2 { get; set; }
        [DisplayName("課程照片3")]
        public string c_imgPath3 { get; set; }
        [DisplayName("搜尋標籤1")]
        public string c_lable1 { get; set; }
        [DisplayName("搜尋標籤2")]
        public string c_lable2 { get; set; }
        [DisplayName("搜尋標籤3")]
        public string c_lable3 { get; set; }
        [DisplayName("搜尋標籤4")]
        public string c_lable4 { get; set; }
        [DisplayName("搜尋標籤5")]
        public string c_lable5 { get; set; }
        [DisplayName("名")]
        public string m_firstName { get; set; }

        [DisplayName("姓")]
        public string m_lastName { get; set; }
        [DisplayName("課程評價")]
        public Nullable<double> c_rate { get; set; }
        [DisplayName("課程評價次數")]
        public Nullable<int> c_rateTotal { get; set; }

        [DisplayName("課程時數")]
        public Nullable<int> c_hourRate { get; set; }

        [DisplayName("課程價格")]
        public Nullable<int> c_price { get; set; }

        [DisplayName("優惠價格")]
        public Nullable<int> c_discount { get; set; }
        [DisplayName("優惠結束時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_onsaleEnd { get; set; }

    }

    public class ratingList 
    {
        public int classID { get; set; }
        public int memberID { get; set; }
        [DisplayName("名")]
        public string m_firstName { get; set; }
        [DisplayName("姓")]
        public string m_lastName { get; set; }
        [DisplayName("個人照片")]
        public string m_imgPath { get; set; }
        public int ratingID { get; set; }
        [DisplayName("評價分數")]
        public Nullable<int> r_star { get; set; }
        [DisplayName("評價內容")]
        public string r_sendText { get; set; }
        [DisplayName("評價時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> r_sendTime { get; set; }
    }

}