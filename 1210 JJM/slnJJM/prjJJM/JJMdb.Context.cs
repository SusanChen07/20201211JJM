﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JJMdbEntities : DbContext
    {
        public JJMdbEntities()
            : base("name=JJMdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tClass> tClass { get; set; }
        public virtual DbSet<tDeposit> tDeposit { get; set; }
        public virtual DbSet<tMember> tMember { get; set; }
        public virtual DbSet<tOrder> tOrder { get; set; }
        public virtual DbSet<tOrder_Detail> tOrder_Detail { get; set; }
        public virtual DbSet<tPay> tPay { get; set; }
        public virtual DbSet<tRating> tRating { get; set; }
        public virtual DbSet<tShop> tShop { get; set; }
        public virtual DbSet<tTeacher> tTeacher { get; set; }
        public virtual DbSet<tWish> tWish { get; set; }
    }
}
