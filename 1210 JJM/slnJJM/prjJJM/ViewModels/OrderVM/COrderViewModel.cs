using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.OrderVM
{
    public class COrderViewModel
    {
        private tOrder _Order;
        public COrderViewModel(tOrder o)
        {
            _Order = o;
        }

        public int orderID { get { return _Order.orderID; } set { _Order.orderID = value; } }
        [DisplayName("會員編號")]
        [StringLength(500)]
        public Nullable<int> memberID { get { return _Order.memberID; } set { _Order.memberID = value; } }
        [DisplayName("訂購時間")]
        public Nullable<System.DateTime> o_orderdate { get { return _Order.o_orderdate; } set { _Order.o_orderdate = value; } }

        
    }
}