//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjJJM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class tClass
    {
        public int classID { get; set; }
        [DisplayName("課程名稱")]
        [Required(ErrorMessage = "請輸入課程名稱")] /*前端驗證*/
        public string c_name { get; set; }
        [DisplayName("課程副標")]
        public string c_nameText { get; set; }
        [DisplayName("課程簡介")]
        public string c_intro { get; set; }
        [DisplayName("開課時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> c_startTime { get; set; }
        [DisplayName("完課時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> c_endTime { get; set; }
        [DisplayName("課程時數")]
        public Nullable<int> c_hourRate { get; set; }
        [DisplayName("開始報名時間")]
        public Nullable<System.DateTime> c_registerStart { get; set; }
        [DisplayName("報名結束時間")]
        public Nullable<System.DateTime> c_registerEnd { get; set; }
        [DisplayName("課程人數上限")]
        public Nullable<int> c_maxStudent { get; set; }
        [DisplayName("最少招生人數")]
        public Nullable<int> c_minStudent { get; set; }
        [DisplayName("已報名人數")]
        public Nullable<int> c_student { get; set; }
        [DisplayName("教室位置")]
        public string c_location { get; set; }
        [DisplayName("課程價格")]
        public Nullable<int> c_price { get; set; }
        [DisplayName("優惠開始時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> c_onsaleStart { get; set; }
        [DisplayName("優惠結束時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> c_onsaleEnd { get; set; }
        [DisplayName("優惠價格")]
        public Nullable<int> c_discount { get; set; }
        [DisplayName("課程分級")]
        public string c_level { get; set; }
        [DisplayName("上課基本要求")]
        public string c_requirement { get; set; }
        [DisplayName("課程評價")]
        public Nullable<double> c_rate { get; set; }
        [DisplayName("課程照片1")]
        public string c_imgPath1 { get; set; }
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
        [DisplayName("教練編號")]
        public Nullable<int> teacherID { get; set; }

        public virtual tTeacher tTeacher { get; set; }
    }
}
