using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels
{
    //[MetadataType(typeof(CMemberMgtViewModel))]
    public class CMemberMgtViewModel
    {
        private tMember _Member;
        public CMemberMgtViewModel(tMember m)
        {
            _Member = m;
        }

        public int memberID { get { return _Member.memberID; } set { _Member.memberID = value; } }
        [DisplayName("名")]
        public string m_firstName { get { return _Member.m_firstName; } set { _Member.m_firstName = value; } }
        [DisplayName("姓")]
        public string m_lastName { get { return _Member.m_lastName; } set { _Member.m_lastName = value; } }
        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> m_birth { get { return (DateTime)_Member.m_birth; } set { _Member.m_birth = value; } }
        [DisplayName("性別")]
        public string m_gender { get { return _Member.m_gender; } set { _Member.m_gender = value; } }
        [DisplayName("電子信箱(帳號)")]
        [Required(ErrorMessage = "請輸入電子信箱")] /*前端驗證*/
        public string m_email { get { return _Member.m_email; } set { _Member.m_email = value; } }
        [DisplayName("密碼")]
        public string m_password { get { return _Member.m_password; } set { _Member.m_password = value; } }
        [DisplayName("手機")]
        public string m_phone { get { return _Member.m_phone; } set { _Member.m_phone = value; } }
        [DisplayName("地區")]
        public string m_district { get { return _Member.m_district; } set { _Member.m_district = value; } }
        [DisplayName("地址")]
        public string m_address { get { return _Member.m_address; } set { _Member.m_address = value; } }
        [DisplayName("會員權限")]
        public Nullable<int> m_role { get { return _Member.m_role; } set { _Member.m_role = value; } }
        [DisplayName("興趣")]
        public string m_hobby { get { return _Member.m_hobby; } set { _Member.m_hobby = value; } }
        [DisplayName("個人照片")]
        public string m_imgPath { get { return _Member.m_imgPath; } set { _Member.m_imgPath = value; } }
        [DisplayName("JJM點數")]
        public Nullable<int> m_Jpoint { get { return _Member.m_Jpoint; } set { _Member.m_Jpoint = value; } }
        [DisplayName("註冊時間")]
        public Nullable<System.DateTime> m_getNow { get { return _Member.m_getNow; } set { _Member.m_getNow = value; } }
        [DisplayName("Email確認")]
        public string m_emailConfirm { get { return _Member.m_emailConfirm; } set { _Member.m_emailConfirm = value; } }

        [DisplayName("姓名")]
        public string m_fullName { get { return _Member.m_lastName + _Member.m_firstName; } }
    }
}