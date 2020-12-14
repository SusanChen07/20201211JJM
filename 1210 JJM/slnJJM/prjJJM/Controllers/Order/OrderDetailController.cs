using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.OrderVM;

namespace prjJJM.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetails
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            var table = from p in new JJMdbEntities().tOrder_Detail
                        select p;

            var keyw = from k in new JJMdbEntities().tOrder_Detail
                       where k.c_name.Contains(keyword)

                       select k;

            List<COrderDetailViewModel> list = new List<COrderDetailViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tOrder_Detail od in table)
                {
                    list.Add(new COrderDetailViewModel(od));
                }
            }
            else
            {
                foreach (tOrder_Detail od in keyw)
                {
                    list.Add(new COrderDetailViewModel(od));
                }
            }

            return View(list);

        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tOrder_Detail orderDetail = db.tOrder_Detail.FirstOrDefault(od => od.od_itemID == id);

            if (orderDetail != null)
            {
                db.tOrder_Detail.Remove(orderDetail);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tOrder_Detail orderDetail = (new JJMdbEntities()).tOrder_Detail.FirstOrDefault(od => od.od_itemID == id);
            var orderDetailList = new COrderDetailViewModel(orderDetail);


            if (orderDetail == null)
                return RedirectToAction("List");

            return View(orderDetailList);
        }
        [HttpPost]
        public ActionResult Edit(tOrder_Detail modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tOrder_Detail orderDetail = db.tOrder_Detail.FirstOrDefault(od => od.od_itemID == modify.od_itemID);

            if (orderDetail != null)
            {
                orderDetail.orderID = modify.orderID;
                orderDetail.classID = modify.classID;
                orderDetail.c_name = modify.c_name;
                orderDetail.c_price = modify.c_price;
                orderDetail.c_discount = modify.c_discount;
                orderDetail.od_profit = modify.od_profit;

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tOrder_Detail od)
        {
            var orderDetailList = new COrderDetailViewModel(od);

            JJMdbEntities db = new JJMdbEntities();

            db.tOrder_Detail.Add(od);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}