using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels
{
    public class CShopWishViewModel
    {
        public int total
        {
            get;set;
        }
        public int ClassID
        {
            get;set;
        }
        public int Point
        {
            get;
            set;
        }

        public tMember memberData
        {
            get;
            set;
        }

        public IEnumerable<tShop> Shop
        {
            get;
            set;
        }
        public IEnumerable<tWish> Wish
        {
            get;
            set;
        }

        public IEnumerable<tClass> classData
        {
            get;
            set;
        }

        public IEnumerable<tOrder_Detail> orderDetail
        {
            get;
            set;
        }
        public tOrder Order
        {
            get;
            set;
        }

    }
}