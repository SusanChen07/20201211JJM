using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Xunit.Sdk;

namespace prjJJM.ViewModels
{
    public class CClassMgtViewModel
    {

        private tClass _class;
        public CClassMgtViewModel(tClass c)
        {
            _class = c;
        }

        public int classID { get { return _class.classID; } set { _class.classID = value; } }
        [DisplayName("課程名稱")]
        //[Required(ErrorMessage = "請輸入課程名稱")]
        public string c_name { get { return _class.c_name; } set { _class.c_name = value; } }
        [DisplayName("課程副標")]
        public string c_nameText { get { return _class.c_nameText; } set { _class.c_nameText = value; } }
        [DisplayName("課程簡介")]
        public string c_intro { get { return _class.c_intro; } set { _class.c_intro = value; } }
        [DisplayName("開課時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_startTime { get { return (DateTime)_class.c_startTime; } set { _class.c_startTime = value; } }
        [DisplayName("完課時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_endTime { get { return (DateTime)_class.c_endTime; } set { _class.c_endTime = value; } }
        [DisplayName("課程時數")]
        public Nullable<int> c_hourRate { get { return _class.c_hourRate; } set { _class.c_hourRate = value; } }
        [DisplayName("開始報名時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_registerStart { get { return (DateTime)_class.c_registerStart; } set { _class.c_registerStart = value; } }
        [DisplayName("報名結束時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_registerEnd { get { return (DateTime)_class.c_registerEnd; } set { _class.c_registerEnd = value; } }
        [DisplayName("課程人數上限")]
        public Nullable<int> c_maxStudent { get { return _class.c_maxStudent; } set { _class.c_maxStudent = value; } }
        [DisplayName("最少招生人數")]
        public Nullable<int> c_minStudent { get { return _class.c_minStudent; } set { _class.c_minStudent = value; } }
        [DisplayName("已報名人數")]
        public Nullable<int> c_student { get { return _class.c_student; } set { _class.c_student = value; } }
        [DisplayName("教室位置")]
        public string c_location { get { return _class.c_location; } set { _class.c_location = value; } }
        [DisplayName("課程價格")]
        public Nullable<int> c_price { get { return _class.c_price; } set { _class.c_price = value; } }
        [DisplayName("優惠開始時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_onsaleStart { get { return (DateTime)_class.c_onsaleStart; } set { _class.c_onsaleStart = value; } }
        [DisplayName("優惠結束時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime c_onsaleEnd { get { return (DateTime)_class.c_onsaleEnd; } set { _class.c_onsaleEnd = value; } }
        [DisplayName("優惠價格")]
        public Nullable<int> c_discount { get { return _class.c_discount; } set { _class.c_discount = value; } }
        [DisplayName("課程分級")]
        public string c_level { get { return _class.c_level; } set { _class.c_level = value; } }
        [DisplayName("上課基本要求")]
        public string c_requirement { get { return _class.c_requirement; } set { _class.c_requirement = value; } }
        [DisplayName("課程評價")]
        public Nullable<double> c_rate { get { return _class.c_rate; } set { _class.c_rate = value; } }
        [DisplayName("課程評價次數")]
        public Nullable<int> c_rateTotal { get { return _class.c_rateTotal; } set { _class.c_rateTotal = value; } }
        [DisplayName("課程照片1")]
        public string c_imgPath1 { get { return _class.c_imgPath1; } set { _class.c_imgPath1 = value; } }
        public HttpPostedFileBase image1 { get; set; }
        [DisplayName("課程照片2")]
        public string c_imgPath2 { get { return _class.c_imgPath2; } set { _class.c_imgPath2 = value; } }
        public HttpPostedFileBase image2 { get; set; }
        [DisplayName("課程照片3")]
        public string c_imgPath3 { get { return _class.c_imgPath3; } set { _class.c_imgPath3 = value; } }
        public HttpPostedFileBase image3 { get; set; }
        [DisplayName("搜尋標籤1")]
        public string c_lable1 { get { return _class.c_lable1; } set { _class.c_lable1 = value; } }
        [DisplayName("搜尋標籤2")]
        public string c_lable2 { get { return _class.c_lable2; } set { _class.c_lable2 = value; } }
        [DisplayName("搜尋標籤3")]
        public string c_lable3 { get { return _class.c_lable3; } set { _class.c_lable3 = value; } }
        [DisplayName("搜尋標籤4")]
        public string c_lable4 { get { return _class.c_lable4; } set { _class.c_lable4 = value; } }
        [DisplayName("搜尋標籤5")]
        public string c_lable5 { get { return _class.c_lable5; } set { _class.c_lable5 = value; } }
        [DisplayName("教練編號")]
        public Nullable<int> teacherID { get { return _class.teacherID; } set { _class.teacherID = value; } }
        [DisplayName("發佈時間")]
        public Nullable<System.DateTime> c_getNow { get { return _class.c_getNow; } set { _class.c_getNow = value; } }

        public tMember memForClassListT { get; set; }

        public tRating ratingtList
        {
            get;
            set;
        }
    }
}