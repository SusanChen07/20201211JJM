using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.OrderVM;

namespace prjJJM.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        //購物車頁面
        public ActionResult List()
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);
            var member = db.tMember.FirstOrDefault(x => x.memberID == id);
            var shop = db.tShop.Where(x => x.memberID == id).ToList();
            var total = 0;

            CShopWishViewModel ShopVM = new CShopWishViewModel();
            ShopVM.Shop = shop;
            ShopVM.memberData = member;

            List<tClass> list = new List<tClass>();
            foreach (var i in ShopVM.Shop)
            {
                tClass classD = new tClass();
                classD = db.tClass.FirstOrDefault(x => x.classID == i.classID);
                total += Convert.ToInt32(classD.c_price);
                list.Add(classD);
            }
            ShopVM.classData = list;
            ShopVM.total = total;
            return View(ShopVM);
        }

        //確認餘額ok 但是不知道怎麼跟確認付款結合
        public ActionResult Ordercheck()
        {
            return View("check");
        }
        [HttpPost]
        public ActionResult Ordercheck(CShopWishViewModel cartT)
        {

            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);

            tMember MemP = db.tMember.FirstOrDefault(p => p.memberID == id);

            //如果總額大於餘額
            if (cartT.total > (int)MemP.m_Jpoint)
            {

                string msg = "tooless";
                if (Request.IsAjaxRequest())
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("order");
                }
                
            }
            else
            {
                return RedirectToAction("order");
            }
        }

        [HttpPost]
        public ActionResult Orderone(CShopWishViewModel cartT)
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);

            tMember MemP = db.tMember.FirstOrDefault(p => p.memberID == id);

            //如果總額大於餘額
            if (cartT.total > (int)MemP.m_Jpoint)
            {

                string msg = "tooless";
                if (Request.IsAjaxRequest())
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("check");
                }

            }
            else
            {
               
                string msg = "go";
                if (Request.IsAjaxRequest())
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("order", "Shop");
                }
            }          
        }

        public ActionResult orderDetail()
        {
            var id = Convert.ToInt32(Session["ID"]);
            JJMdbEntities db = new JJMdbEntities();
            var od = db.tOrder.Where(x => x.memberID == id).ToList();
            COrderDetailMgtViewModel odvm = new COrderDetailMgtViewModel();
            List<tOrder_Detail> odList = new List<tOrder_Detail>();
            foreach (var i in od)
            {
                var detail = db.tOrder_Detail.Where(x => x.orderID == i.orderID).ToList();
                foreach(var j in detail)
                {
                    tOrder_Detail oDetail = db.tOrder_Detail.FirstOrDefault(x => x.od_itemID == j.od_itemID);
                    odList.Add(oDetail);
                }
            }
            odvm.orderData = od;
            odvm.DetailData = odList;


            return View(odvm);
            
        }
        //結帳
        public ActionResult order()
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);
            
            var total = 0;
            tMember MemP = db.tMember.FirstOrDefault(p => p.memberID == id);
            

            var cart = db.tShop.Where(x => x.memberID == id).ToList();

            CShopWishViewModel ShopVM = new CShopWishViewModel();
            tDeposit point = new tDeposit();
            point.d_method = "/";
            ShopVM.Shop = cart;
            //存order
            tOrder order = new tOrder();
            order.memberID = id;
            //會員下單時間
            order.o_orderdate = DateTime.Now;

            //var dateT = order.o_orderdate;
            db.tOrder.Add(order);
            db.SaveChanges();
            var orderid = order.orderID;
            var o = db.tOrder.FirstOrDefault(x => x.orderID == orderid);

            List<tClass> list = new List<tClass>();
            foreach (var i in cart)
            {
                tRating ratingD = new tRating();
                tOrder_Detail od = new tOrder_Detail();
                tClass classD = new tClass();               
                classD = db.tClass.FirstOrDefault(x => x.classID == i.classID);
                list.Add(classD);
                point.d_memo += classD.c_name.ToString();
                point.d_memo += "/";
                ratingD.memberID = id;
                ratingD.classID = i.classID;
                ratingD.r_send = i.tMember.m_fullName;
                od.orderID = o.orderID;
                od.classID = i.classID;
                od.c_name = i.tClass.c_name;
                od.c_price = i.tClass.c_price;
                total += (int)i.tClass.c_price;
                od.c_discount = i.tClass.c_discount;
                var profit = Math.Round(((double)i.tClass.c_price) * 0.01);
                od.od_profit = Convert.ToInt32(profit);
                db.tRating.Add(ratingD);
                db.tOrder_Detail.Add(od);
                db.tShop.Remove(i);
            }

            point.memberID = MemP.memberID;
            point.d_getNow = DateTime.Now;
            point.d_method = "點數扣款";
            point.d_point = -(total);
            
            db.tDeposit.Add(point);

            MemP.m_Jpoint = ((int)MemP.m_Jpoint) - total;
            db.SaveChanges();

            ShopVM.total = total;
            ShopVM.Order = o;
            ShopVM.classData = list;

            return RedirectToAction("orderDetailPage", "Shop", new { oid = orderid });

            //return View(ShopVM);
        }

        public ActionResult orderDetailPage(int oid)
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);
            CShopWishViewModel ShopVM = new CShopWishViewModel();
            var total = 0;
            var detail = db.tOrder_Detail.Where(a => a.orderID == oid);
            List<tClass> classList = new List<tClass>();

            foreach (var i in detail)
            {
                var classD = db.tClass.FirstOrDefault(b => b.classID == i.classID);
                classList.Add(classD);
                total += (int)i.c_price;

            }
            var od = db.tOrder.FirstOrDefault(c => c.orderID == oid);

            ShopVM.Order = od;
            ShopVM.classData = classList;
            ShopVM.total = total;

            return View(ShopVM);
        }

        //結帳頁面的儲值功能
        [HttpPost]
        public ActionResult check(CShopWishViewModel VM)
        {
            var id = Convert.ToInt32(Session["ID"]);

            JJMdbEntities db = new JJMdbEntities();

            tMember MemP = db.tMember.FirstOrDefault(p => p.memberID == id);
            tDeposit point = new tDeposit();
            point.d_point = VM.Point;
            point.d_method = "信用卡";
            point.d_getNow = DateTime.Now;
            point.memberID = MemP.memberID;
            db.tDeposit.Add(point);
            
            MemP.m_Jpoint = ((int)MemP.m_Jpoint) + VM.Point;

            db.SaveChanges();


            return RedirectToAction("check");
        }

        //結帳頁面
        public ActionResult check()
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);
            var member = db.tMember.FirstOrDefault(x => x.memberID == id);
            var shop = db.tShop.Where(x => x.memberID == id).ToList();
            var total = 0;

            if(member.m_Jpoint == null)
            {
                member.m_Jpoint = 0;
                db.SaveChanges();
            }

            CShopWishViewModel ShopVM = new CShopWishViewModel();
            ShopVM.Shop = shop;
            ShopVM.memberData = member;

            List<tClass> list = new List<tClass>();
            foreach (var i in ShopVM.Shop)
            {
                tClass classD = new tClass();
                classD = db.tClass.FirstOrDefault(x => x.classID == i.classID);
                total += Convert.ToInt32(classD.c_price);
                list.Add(classD);
            }
            ShopVM.classData = list;
            ShopVM.memberData = member;
            ShopVM.total = total;
            return View(ShopVM);
        }

        //加入購物車 在願望清單頁面用的
        public ActionResult SwitchToCart(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            var memID = Convert.ToInt32(Session["ID"]);
            var exis = db.tShop.FirstOrDefault(x => x.classID == id && x.memberID == memID);
            //string msg = "";
            if (exis == null)
            {
                tShop shop = new tShop();
                shop.memberID = memID;
                shop.classID = id;
                shop.s_getNow = DateTime.Now;
                db.tShop.Add(shop);
                tWish wish = db.tWish.FirstOrDefault(x => x.classID == id && x.memberID == memID);
                if (wish != null)
                {
                    db.tWish.Remove(wish);
                }
                db.SaveChanges();

            }

            return RedirectToAction("List", "Wish");

        }

        

        


        //加入購物車
        [HttpPost]
        public ActionResult AddToCart(CShopWishViewModel cart)
        {
            JJMdbEntities db = new JJMdbEntities();
            var memID = Convert.ToInt32(Session["ID"]);
            var exis = db.tShop.FirstOrDefault(x => x.classID == cart.ClassID && x.memberID == memID);
            string msg = "";
            var classExist = from od in db.tOrder_Detail
                             join o in db.tOrder on od.orderID equals o.orderID
                             where o.memberID == memID && od.classID == cart.ClassID
                             select o;

            if (classExist.FirstOrDefault() != null)
            {
                msg = "classExist";
            }
            else
            {
                if (exis == null)
                {
                    tShop shop = new tShop();
                    shop.memberID = memID;
                    shop.classID = cart.ClassID;
                    shop.s_getNow = DateTime.Now;
                    db.tShop.Add(shop);
                    tWish wish = db.tWish.FirstOrDefault(x => x.classID == cart.ClassID && x.memberID == memID);
                    if (wish != null)
                    {
                        db.tWish.Remove(wish);
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

        public ActionResult Delete(int id)
        {
            var memID = Convert.ToInt32(Session["ID"]);
            JJMdbEntities db = new JJMdbEntities();
            tShop shop = db.tShop.FirstOrDefault(x => x.classID == id && x.memberID == memID);
            if (shop != null)
            {
                db.tShop.Remove(shop);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}