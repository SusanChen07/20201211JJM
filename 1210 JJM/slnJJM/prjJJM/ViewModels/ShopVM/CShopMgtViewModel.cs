using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjJJM.ViewModels.ShopVM
{
    public class CShopMgtViewModel
    {
        private tShop _shop;
        public CShopMgtViewModel(tShop s)
        {
            _shop = s;
        }

        public int shopID { get { return _shop.shopID; } set { _shop.shopID = value; } }
        [DisplayName("課程編號")]
        public Nullable<int> classID { get { return _shop.classID; } set { _shop.classID = value; } }
        [DisplayName("會員編號")]
        public Nullable<int> memberID { get { return _shop.memberID; } set { _shop.memberID = value; } }
        [DisplayName("加入時間")]
        public Nullable<System.DateTime> s_getNow { get { return _shop.s_getNow; } set { _shop.s_getNow = value; } }
    }
}