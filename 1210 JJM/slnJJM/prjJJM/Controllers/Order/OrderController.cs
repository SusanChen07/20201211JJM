using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.OrderVM;

namespace prjJJM.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            var table = from p in new JJMdbEntities().tOrder
                        select p;

            var keyw = from k in new JJMdbEntities().tOrder
                       //where k.memberID.Contains(keyword) || k.o_orderdate.Contains(keyword)

                       select k;

            List<COrderViewModel> list = new List<COrderViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tOrder o in table)
                {
                    list.Add(new COrderViewModel(o));
                }
            }
            else
            {
                foreach (tOrder o in keyw)
                {
                    list.Add(new COrderViewModel(o));
                }
            }

            return View(list);

        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tOrder order = db.tOrder.FirstOrDefault(o => o.orderID == id);

            if (order != null)
            {
                db.tOrder.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tOrder order = (new JJMdbEntities()).tOrder.FirstOrDefault(o => o.orderID == id);
            var orderList = new COrderViewModel(order);


            if (order == null)
                return RedirectToAction("List");

            return View(orderList);
        }
        [HttpPost]
        public ActionResult Edit(tOrder modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tOrder order = db.tOrder.FirstOrDefault(o => o.orderID == modify.orderID);

            if (order != null)
            {
                order.memberID = modify.memberID;
                //order.o_orderdate = modify.o_orderdate;

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tOrder o)
        {
            var orderList = new COrderViewModel(o);

            JJMdbEntities db = new JJMdbEntities();

            o.o_orderdate = DateTime.Now;
            db.tOrder.Add(o);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}