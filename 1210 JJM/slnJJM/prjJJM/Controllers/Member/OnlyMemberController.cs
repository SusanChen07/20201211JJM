using prjJJM.ViewModels;
using prjJJM.ViewModels.MemberVM;
using prjJJM.ViewModels.RatingVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjJJM.Controllers
{
    public class OnlyMemberController : Controller
    {
        // GET: OnlyMember
        public ActionResult List(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tMember mem = db.tMember.FirstOrDefault(c => c.memberID == id);

            CMemberViewModel COnlyM = new CMemberViewModel(mem);

            //List<CMemberViewModel> list = new List<CMemberViewModel>();

            //if (mem != null)
            //{
            //    var table = from p in new JJMdbEntities().tMember
            //                where p.memberID == mem.memberID
            //                select p;

            //    foreach (tMember c in table)
            //    {
            //        list.Add(new CMemberViewModel(c));

            //    }
            //}

            return View(COnlyM);
        }

        //修改圖片
        public ActionResult EditImg()
        {            
            return View("EditImg");            
        }

        [HttpPost]
        public ActionResult EditImg(string imgID, HttpPostedFileBase img)
        {

            int imgid = Convert.ToInt32(imgID);

            JJMdbEntities db = new JJMdbEntities();
            tMember mem = db.tMember.FirstOrDefault(c => c.memberID == imgid);

            var memMgt = new CMemberViewModel(mem);


            if (img != null)
            {
                //var fileName = System.IO.Path.GetFileName(img.FileName);
                //System.Diagnostics.Debug.WriteLine(fileName);
                //int point = fileName.IndexOf(".");
                //Console.WriteLine("point");
                //System.Diagnostics.Debug.WriteLine(point);
                //string extention = fileName.Substring(point, fileName.Length - point);
                //Console.WriteLine("extention");
                //string photoName = Guid.NewGuid().ToString() + extention;
                string photoName = Guid.NewGuid().ToString() + ".jpeg";
                string oldphoto = memMgt.m_imgPath;


                img.SaveAs(Server.MapPath(@"~/Content/imgMember/" + photoName));
                try
                {
                    if (oldphoto != "/Content/imgMember/defaultMember.png")
                    {
                        System.IO.File.Delete(Server.MapPath(@"~" + oldphoto));
                    }
                }
                catch
                {
                }
                memMgt.m_imgPath = @"/Content/imgMember/" + photoName;
                
            }

            if (memMgt != null)
            {
                mem.m_imgPath = memMgt.m_imgPath;
                Session["Image"] = memMgt.m_imgPath;
               db.SaveChanges();
            }

            //if (Request.IsAjaxRequest())
            //{
            //    return Json("ok", JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return RedirectToAction(actionName: "List", controllerName: "OnlyMember", new { id = memMgt.memberID });
            //}
            return Json("ok");
            //return RedirectToAction("List", "OnlyMember", new { id = memMgt.memberID });
            //return View("Edit", new { id = mem.memberID });

        }



        //修改
        public ActionResult Edit(int id)
        {
            tMember mem = new JJMdbEntities().tMember.FirstOrDefault(c => c.memberID == id);

            var COnlyM = new CMemberViewModel(mem);


            //var memberList = new CMemberViewModel(mem);


            if (mem == null)
            {
                return RedirectToAction("List");
            }

            return View(COnlyM);
        }

        //修改
        [HttpPost]
        public ActionResult Edit(tMember modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tMember mem = db.tMember.FirstOrDefault(c => c.memberID == modify.memberID);

            CMemberViewModel CMember = new CMemberViewModel(mem);


            if (mem != null)
            {
                mem.m_firstName = modify.m_firstName;
                mem.m_lastName = modify.m_lastName;
                mem.m_birth = modify.m_birth;
                mem.m_gender = modify.m_gender;
                mem.m_password = modify.m_password;
                mem.m_phone = modify.m_phone;
                mem.m_address = modify.m_address;
                mem.m_hobby = modify.m_hobby;
                //mem.m_imgPath = modify.m_imgPath;
                db.SaveChanges();
            }

            return RedirectToAction("List", "OnlyMember", new { id = mem.memberID });
        }

        //個人課程
        public ActionResult OnlyClass()
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);
            var member = db.tMember.FirstOrDefault(x => x.memberID == id);
            var orderList = db.tOrder.Where(x => x.memberID == id).ToList();

            tRating ratingRecord = new tRating();
            var order = db.tOrder.Where(x => x.memberID == id).ToList();
            foreach (var t in order) 
            {
                var odetail = db.tOrder_Detail.Where(d => d.orderID == t.orderID).ToList();
                foreach (var e in odetail) 
                {
                    var rating = db.tRating.FirstOrDefault(s => s.classID == e.classID);
                }
            }


            CMemberOnlyClassViewModel MemClass = new CMemberOnlyClassViewModel();

            MemClass.Order = orderList;

            List<tClass> list2 = new List<tClass>();

            foreach (var i in MemClass.Order)
            {

                var Odeta = db.tOrder_Detail.Where(x => x.orderID == i.orderID).ToList();
                foreach(var j in Odeta)
                {
                    
                    var clas = db.tClass.FirstOrDefault(x => x.classID == j.classID);
                    list2.Add(clas);
                }

            }

            MemClass.classData = list2;
            MemClass.memberData = member;
            MemClass.RatingData = ratingRecord;
            return View(MemClass);
        }

        //個人課程評論
        [HttpPost]
        public ActionResult OnlyClass(string txtStar, int inStar, tRating rat)
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = Convert.ToInt32(Session["ID"]);
            int clasID = Convert.ToInt32(Request.Form["clasID"]);

            var clasRated = db.tClass.FirstOrDefault(c => c.classID == clasID);
            var rating = db.tRating.Where(x => x.memberID == id).FirstOrDefault(x => x.classID == clasID);
            var mem = db.tRating.Where(x => x.memberID == id).Join(db.tMember, a => a.memberID, b => b.memberID, (a, b) => new { b.m_firstName, b.m_lastName }).ToList();

            int f星星個數 = inStar;
            string f評論 = txtStar;
            //int f課程ID = Convert.ToInt32(Request.Form["clasID"]);

            foreach (var q2 in mem)
            {
                if (rating != null)
                {
                    rating.r_send = q2.m_lastName +""+ q2.m_firstName;
                    rating.r_star = f星星個數;
                    rating.r_sendText = f評論;
                    rating.r_sendTime = DateTime.Now;
                    clasRated.c_rateTotal += 1;
                    db.SaveChanges();
                }
                else
                {
                    TempData["message"] = "已重複評論";
                }
            }
            return RedirectToAction("OnlyClass", "OnlyMember");
        }
    }
}