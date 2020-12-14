using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjJJM.ViewModels.ShopVM
{
    public class CWishViewModel
    {
        private tWish _wish;
        public CWishViewModel(tWish w)
        {
            _wish = w;
        }

        public int WishID { get { return _wish.WishID; } set { _wish.WishID = value; } }
        [DisplayName("課程編號")]
        [StringLength(500)]
        public Nullable<int> classID { get { return _wish.classID; } set { _wish.classID = value; } }
        [DisplayName("會員編號")]
        [StringLength(500)]
        public Nullable<int> memberID { get { return _wish.memberID; } set { _wish.memberID = value; } }
        [DisplayName("加入時間")]
        public Nullable<System.DateTime> s_getNow { get { return _wish.s_getNow; } set { _wish.s_getNow = value; } }
    }
}