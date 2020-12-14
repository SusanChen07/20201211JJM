using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.DepositVM;

namespace prjJJM.Controllers.Deposit
{
    public class DepositController : Controller
    {
        // GET: Deposit
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            var table = from p in new JJMdbEntities().tDeposit
                        select p;

            var keyw = from k in new JJMdbEntities().tDeposit
                       where k.d_method.Contains(keyword) || k.d_memo.Contains(keyword)

                       select k;

            List<CDepositViewModel> list = new List<CDepositViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tDeposit d in table)
                {
                    list.Add(new CDepositViewModel(d));
                }
            }
            else
            {
                foreach (tDeposit d in keyw)
                {
                    list.Add(new CDepositViewModel(d));
                }
            }

            return View(list);

        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tDeposit deposit = db.tDeposit.FirstOrDefault(d => d.depositID == id);

            if (deposit != null)
            {
                db.tDeposit.Remove(deposit);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tDeposit deposit = (new JJMdbEntities()).tDeposit.FirstOrDefault(d => d.depositID == id);
            var depositList = new CDepositViewModel(deposit);


            if (deposit == null)
                return RedirectToAction("List");

            return View(depositList);
        }
        [HttpPost]
        public ActionResult Edit(tDeposit modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tDeposit deposit = db.tDeposit.FirstOrDefault(d => d.depositID == modify.depositID);

            if (deposit != null)
            {
                deposit.memberID = modify.memberID;
                deposit.d_point = modify.d_point;
                deposit.d_method = modify.d_method;
                deposit.d_memo = modify.d_memo;
                //rating.d_getNow = modify.d_getNow;              

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tDeposit d)
        {
            var depositList = new CDepositViewModel(d);

            JJMdbEntities db = new JJMdbEntities();
            d.d_getNow = DateTime.Now;
            db.tDeposit.Add(d);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}