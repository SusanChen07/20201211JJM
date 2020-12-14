using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.OrderVM
{
    public class COrderDetailViewModel
    {
        private tOrder_Detail _OrderDetail;
        public COrderDetailViewModel(tOrder_Detail od)
        {
            _OrderDetail = od;
        }

        public int od_itemID { get { return _OrderDetail.od_itemID; } set { _OrderDetail.od_itemID = value; } }
        [DisplayName("訂單編號")]
        [StringLength(500)]
        public Nullable<int> orderID { get { return _OrderDetail.orderID; } set { _OrderDetail.orderID = value; } }
        [DisplayName("課程編號")]
        [StringLength(500)]
        public Nullable<int> classID { get { return _OrderDetail.classID; } set { _OrderDetail.classID = value; } }
        [DisplayName("課程名稱")]
        [Required(ErrorMessage = "請輸入課程名稱")] /*後端驗證*/
        public string c_name { get { return _OrderDetail.c_name; } set { _OrderDetail.c_name = value; } }
        [DisplayName("課程價格")]
        [Required(ErrorMessage = "請輸入課程價格")] /*後端驗證*/
        public Nullable<int> c_price { get { return _OrderDetail.c_price; } set { _OrderDetail.c_price = value; } }
        [DisplayName("優惠價格")]
        public Nullable<int> c_discount { get { return _OrderDetail.c_discount; } set { _OrderDetail.c_discount = value; } }
        [DisplayName("課程利潤")]
        public Nullable<int> od_profit { get { return _OrderDetail.od_profit; } set { _OrderDetail.od_profit = value; } }
    }
}