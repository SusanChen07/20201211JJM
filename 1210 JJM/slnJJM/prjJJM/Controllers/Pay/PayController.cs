using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.PayVM;

namespace prjJJM.Controllers.Pay
{
    public class PayController : Controller
    {
        // GET: Pay
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            var table = from p in new JJMdbEntities().tPay
                        select p;

            var keyw = from k in new JJMdbEntities().tPay
                       where k.p_memo.Contains(keyword)

                       select k;

            List<CPayViewModel> list = new List<CPayViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tPay p in table)
                {
                    list.Add(new CPayViewModel(p));
                }
            }
            else
            {
                foreach (tPay p in keyw)
                {
                    list.Add(new CPayViewModel(p));
                }
            }

            return View(list);

        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tPay pay = db.tPay.FirstOrDefault(p => p.payID == id);

            if (pay != null)
            {
                db.tPay.Remove(pay);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tPay pay = (new JJMdbEntities()).tPay.FirstOrDefault(p => p.payID == id);
            var payList = new CPayViewModel(pay);


            if (pay == null)
                return RedirectToAction("List");

            return View(payList);
        }
        [HttpPost]
        public ActionResult Edit(tPay modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tPay pay = db.tPay.FirstOrDefault(p => p.payID == modify.payID);

            if (pay != null)
            {
                //pay.teacherID = modify.teacherID;
                //pay.p_money = modify.p_money;
                //pay.p_getNow = modify.p_getNow;                       
                pay.p_status = modify.p_status;        
                pay.p_method = modify.p_method;        
                pay.p_getMoneyTime = modify.p_getMoneyTime;
                pay.p_memo = modify.p_memo;

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tPay p)
        {
            var payList = new CPayViewModel(p);

            JJMdbEntities db = new JJMdbEntities();
            p.p_getNow = DateTime.Now;
            db.tPay.Add(p);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}