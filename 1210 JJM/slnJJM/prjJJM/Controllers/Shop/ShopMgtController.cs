using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.ShopVM;

namespace prjJJM.Controllers.Shop
{
    public class ShopMgtController : Controller
    {
        // GET: ShopMgt
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            var table = from p in new JJMdbEntities().tShop
                        select p;

            var keyw = from k in new JJMdbEntities().tShop
                       //where k.p_memo.Contains(keyword)

                       select k;

            List<CShopMgtViewModel> list = new List<CShopMgtViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tShop s in table)
                {
                    list.Add(new CShopMgtViewModel(s));
                }
            }
            else
            {
                foreach (tShop s in keyw)
                {
                    list.Add(new CShopMgtViewModel(s));
                }
            }

            return View(list);

        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tShop shop = db.tShop.FirstOrDefault(s => s.shopID == id);

            if (shop != null)
            {
                db.tShop.Remove(shop);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tShop shop = (new JJMdbEntities()).tShop.FirstOrDefault(s => s.shopID == id);
            var shopList = new CShopMgtViewModel(shop);


            if (shop == null)
                return RedirectToAction("List");

            return View(shopList);
        }
        [HttpPost]
        public ActionResult Edit(tShop modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tShop shop = db.tShop.FirstOrDefault(s => s.shopID == modify.shopID);

            if (shop != null)
            {
                shop.classID = modify.classID;
                shop.memberID = modify.memberID;
                //shop.s_getNow = modify.s_getNow;
                
                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tShop s)
        {
            var shopList = new CShopViewModel(s);

            JJMdbEntities db = new JJMdbEntities();
            s.s_getNow = DateTime.Now;
            db.tShop.Add(s);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}