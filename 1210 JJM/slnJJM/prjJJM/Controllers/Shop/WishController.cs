using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;

namespace prjJJM.Controllers
{
    public class WishController : Controller
    {
        // GET: Wish
        public ActionResult List()
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);
            var member = db.tMember.FirstOrDefault(x => x.memberID == id);//須改Session
            var wish = db.tWish.Where(x => x.memberID == id);
            CShopWishViewModel WishVM = new CShopWishViewModel();
            WishVM.Wish = wish;
            WishVM.memberData = member;

            List<tClass> list = new List<tClass>();
            foreach (var i in WishVM.Wish)
            {
                tClass classD = new tClass();
                classD = db.tClass.FirstOrDefault(x => x.classID == i.classID);
                list.Add(classD);
            }
            WishVM.classData = list;
            return View(WishVM);

        }
        [HttpPost]
        public ActionResult AddToWish(CShopWishViewModel wishV)
        {
            JJMdbEntities db = new JJMdbEntities();
            var mid = Convert.ToInt32(Session["ID"]);
            var exis = db.tWish.FirstOrDefault(x => x.classID == wishV.ClassID && x.memberID == mid);
            string msg = "";
            var classExist = from od in db.tOrder_Detail
                             join o in db.tOrder on od.orderID equals o.orderID
                             where o.memberID == mid && od.classID == wishV.ClassID
                             select od;

            var classExis = db.tOrder_Detail.Join(db.tOrder, a => a.orderID, b => b.orderID, (a, b) => new { a.classID, b.memberID }).Where(x => x.classID == wishV.ClassID && x.memberID == mid).FirstOrDefault();

            
            if (classExist.FirstOrDefault() != null)
            {
                msg = "classExist";
            }
            else
            {
                if (exis == null)
                {
                    tWish wish = new tWish();
                    wish.classID = wishV.ClassID;
                    wish.memberID = mid;
                    wish.s_getNow = DateTime.Now;
                    db.tWish.Add(wish);
                    tShop shop = db.tShop.FirstOrDefault(x => x.classID == wishV.ClassID && x.memberID == mid);
                    if (shop != null)
                    {
                        db.tShop.Remove(shop);
                    }
                    db.SaveChanges();
                    msg = "ok";
                }
                else
                {
                    msg = "no";
                }
            }

            if (Request.IsAjaxRequest())
            {
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("List", "Shop");
            }

        }

        public ActionResult SwitchToWish(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            var mid = Convert.ToInt32(Session["ID"]);
            var exis = db.tWish.FirstOrDefault(x => x.classID == id && x.memberID == mid);
            //string msg = "";
            if (exis == null)
            {
                tWish wish = new tWish();
                wish.classID = id;
                wish.memberID = mid;
                wish.s_getNow = DateTime.Now;
                db.tWish.Add(wish);
                tShop shop = db.tShop.FirstOrDefault(x => x.classID == id && x.memberID == mid);
                if (shop != null)
                {
                    db.tShop.Remove(shop);
                }
                db.SaveChanges();

            }


            return RedirectToAction("List", "Shop");

        }
        public ActionResult Delete(int id)
        {
            var mid = Convert.ToInt32(Session["ID"]);
            JJMdbEntities db = new JJMdbEntities();
            tWish wish = db.tWish.FirstOrDefault(x => x.classID == id && x.memberID == mid);
            if (wish != null)
            {
                db.tWish.Remove(wish);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}