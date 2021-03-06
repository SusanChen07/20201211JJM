﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels
{
    public class CTeacherViewModel
    {
        private tTeacher _teacher;
        public CTeacherViewModel(tTeacher t)
        {
            _teacher = t;
        }
        public int teacherID { get { return _teacher.teacherID; } set { _teacher.teacherID = value; } }
        [DisplayName("教練證照1")]
        public string t_certificateImg1 { get { return _teacher.t_certificateImg1; } set { _teacher.t_certificateImg1 = value; } }
        public HttpPostedFileBase image1 { get; set; }
        [DisplayName("教練證照2")]
        public string t_certificateImg2 { get { return _teacher.t_certificateImg2; } set { _teacher.t_certificateImg2 = value; } }
        public HttpPostedFileBase image2 { get; set; }
        [DisplayName("教練證照3")]
        public string t_certificateImg3 { get { return _teacher.t_certificateImg3; } set { _teacher.t_certificateImg3 = value; } }
        public HttpPostedFileBase image3 { get; set; }
        [DisplayName("教練證照列表")]
        [StringLength(8000)]
        public string t_certificateTxt { get { return _teacher.t_certificateTxt; } set { _teacher.t_certificateTxt = value; } }
        [DisplayName("教練頭銜")]
        [StringLength(255)]
        [Required(ErrorMessage = "請輸入教練頭銜")] /*後端驗證*/
        public string t_title { get { return _teacher.t_title; } set { _teacher.t_title = value; } }
        [DisplayName("教練自介")]
        [StringLength(500)]
        [Required(ErrorMessage = "請輸入教練自介")] /*後端驗證*/
        public string t_intro { get { return _teacher.t_intro; } set { _teacher.t_intro = value; } }
        [DisplayName("教學經歷")]
        [StringLength(500)]
        [Required(ErrorMessage = "請輸入教練經歷")] /*後端驗證*/
        public string t_experience { get { return _teacher.t_experience; } set { _teacher.t_experience = value; } }
        [DisplayName("教學專長")]
        [StringLength(500)]
        [Required(ErrorMessage = "請輸入教練專長")] /*後端驗證*/
        public string t_expertise { get { return _teacher.t_expertise; } set { _teacher.t_expertise = value; } }
        [DisplayName("教練留言總數")]
        public Nullable<int> t_messageTotal { get { return _teacher.t_messageTotal; } set { _teacher.t_messageTotal = value; } }
        [DisplayName("教練已累積點數")]
        public Nullable<int> t_moneyTotal { get { return _teacher.t_moneyTotal; } set { _teacher.t_moneyTotal = value; } }
        [DisplayName("教練可兌換點數")]
        public Nullable<int> t_money { get { return _teacher.t_money; } set { _teacher.t_money = value; } }
        [DisplayName("教練已累積學生數")]
        public Nullable<int> t_studentTotal { get { return _teacher.t_studentTotal; } set { _teacher.t_studentTotal = value; } }
        [DisplayName("教練已累積課程數")]
        public Nullable<int> t_classTotal { get { return _teacher.t_classTotal; } set { _teacher.t_classTotal = value; } }
        [DisplayName("教練課程平均評價")]
        public Nullable<double> t_rateAvg { get { return _teacher.t_rateAvg; } set { _teacher.t_rateAvg = value; } }
        [DisplayName("教練社群1")]
        [StringLength(500)]
        public string t_socialMedia1 { get { return _teacher.t_socialMedia1; } set { _teacher.t_socialMedia1 = value; } }
        [DisplayName("教練社群2")]
        [StringLength(500)]
        public string t_socialMedia2 { get { return _teacher.t_socialMedia2; } set { _teacher.t_socialMedia2 = value; } }
        [DisplayName("教練社群3")]
        [StringLength(500)]
        public string t_socialMedia3 { get { return _teacher.t_socialMedia3; } set { _teacher.t_socialMedia3 = value; } }
        [DisplayName("教練社群4")]
        [StringLength(500)]
        public string t_socialMedia4 { get { return _teacher.t_socialMedia4; } set { _teacher.t_socialMedia4 = value; } }
        [DisplayName("會員編號")]
        [StringLength(500)]
        public Nullable<int> memberID { get { return _teacher.memberID; } set { _teacher.memberID = value; } }
        [DisplayName("成為教練時間")]
        public Nullable<System.DateTime> t_getNow { get { return _teacher.t_getNow; } set { _teacher.t_getNow = value; } }

        public tMember memberForOnlyT
        {
            get; set;
        }

    }
}