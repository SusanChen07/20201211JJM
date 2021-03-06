//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjJJM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class tMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tMember()
        {
            this.tDeposit = new HashSet<tDeposit>();
            this.tOrder = new HashSet<tOrder>();
            this.tRating = new HashSet<tRating>();
            this.tShop = new HashSet<tShop>();
            this.tTeacher = new HashSet<tTeacher>();
            this.tWish = new HashSet<tWish>();
        }

        public int memberID { get; set; }
        [DisplayName("名")]
        [Required(ErrorMessage = "請輸入名子")] /*前端驗證*/
        public string m_firstName { get; set; }
        [DisplayName("姓")]
        [Required(ErrorMessage = "請輸入姓氏")] /*前端驗證*/
        public string m_lastName { get; set; }
        [DisplayName("生日")]
        [Required(ErrorMessage = "請輸入生日")] /*前端驗證*/
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> m_birth { get; set; }
        [DisplayName("性別")]
        [Required(ErrorMessage = "請輸入性別")] /*前端驗證*/
        public string m_gender { get; set; }
        [DisplayName("電子信箱(帳號)")]
        [Required(ErrorMessage = "請輸入電子信箱")] /*前端驗證*/
        public string m_email { get; set; }
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")] /*前端驗證*/
        public string m_password { get; set; }
        [DisplayName("手機")]
        [Required(ErrorMessage = "請輸入手機號碼")] /*前端驗證*/
        public string m_phone { get; set; }
        [DisplayName("地區")]
        public string m_district { get; set; }
        [DisplayName("地址")]
        public string m_address { get; set; }
        [DisplayName("會員權限")]
        public Nullable<int> m_role { get; set; }
        [DisplayName("興趣")]
        public string m_hobby { get; set; }
        [DisplayName("個人照片")]
        public string m_imgPath { get; set; }
        public HttpPostedFileBase image { get; set; }
        [DisplayName("JJM點數")]
        public Nullable<int> m_Jpoint { get; set; }
        [DisplayName("註冊時間")]
        public Nullable<System.DateTime> m_getNow { get; set; }
        [DisplayName("Email確認")]
        public string m_emailConfirm { get; set; }

        public string m_fullName { get { return (string)(m_lastName + m_firstName); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tDeposit> tDeposit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tOrder> tOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRating> tRating { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tShop> tShop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tTeacher> tTeacher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tWish> tWish { get; set; }
    }
}
