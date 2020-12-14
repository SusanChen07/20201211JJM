using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.OrderVM
{
    public class COrderDetailMgtViewModel
    {
        public List<tOrder> orderData
        {
            get;
            set;
        }

        public List<tOrder_Detail> DetailData
        {
            get;
            set;
        }

        public int total
        {
            get;
            set;
        }
    }
}