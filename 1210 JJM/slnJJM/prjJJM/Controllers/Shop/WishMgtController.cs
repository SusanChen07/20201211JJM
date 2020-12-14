using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.ShopVM;

namespace prjJJM.Controllers.Shop
{
    public class WishMgtController : Controller
    {
        // GET: WishMgt
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            var table = from p in new JJMdbEntities().tWish
                        select p;

            var keyw = from k in new JJMdbEntities().tWish
                           //where k.p_memo.Contains(keyword)

                       select k;

            List<CWishMgtViewModel> list = new List<CWishMgtViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tWish w in table)
                {
                    list.Add(new CWishMgtViewModel(w));
                }
            }
            else
            {
                foreach (tWish w in keyw)
                {
                    list.Add(new CWishMgtViewModel(w));
                }
            }

            return View(list);

        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tWish wish = db.tWish.FirstOrDefault(w => w.WishID == id);

            if (wish != null)
            {
                db.tWish.Remove(wish);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tWish wish = (new JJMdbEntities()).tWish.FirstOrDefault(w => w.WishID == id);
            var wishList = new CWishMgtViewModel(wish);


            if (wish == null)
                return RedirectToAction("List");

            return View(wishList);
        }
        [HttpPost]
        public ActionResult Edit(tWish modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tWish wish = db.tWish.FirstOrDefault(w => w.WishID == modify.WishID);

            if (wish != null)
            {
                wish.classID = modify.classID;
                wish.memberID = modify.memberID;
                //wish.s_getNow = modify.s_getNow;

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tWish w)
        {
            var wishList = new CWishMgtViewModel(w);

            JJMdbEntities db = new JJMdbEntities();
            w.s_getNow = DateTime.Now;
            db.tWish.Add(w);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}