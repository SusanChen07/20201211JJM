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
    
    public partial class tPay
    {
        public int payID { get; set; }
        public Nullable<int> p_money { get; set; }
        public Nullable<System.DateTime> p_getNow { get; set; }
        public string p_status { get; set; }
        public string p_method { get; set; }
        public Nullable<System.DateTime> p_getMoneyTime { get; set; }
        public string p_memo { get; set; }
        public Nullable<int> teacherID { get; set; }
    
        public virtual tTeacher tTeacher { get; set; }
    }
}