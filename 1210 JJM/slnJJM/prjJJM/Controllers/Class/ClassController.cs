using prjJJM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace prjJJM.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        //public ActionResult List()
        //{
        //    string keyword = Request.Form["txtKeword"];

        //    List<CClassViewModel> list = new List<CClassViewModel>();

        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        var table = from c in (new JJMdbEntities()).tClass select c;

        //        foreach (tClass c in table)
        //            list.Add(new CClassViewModel(c));
        //    }
        //    else
        //    {
        //        var keywordsearch = from k in (new JJMdbEntities()).tClass where k.c_name.Contains(keyword) select k;

        //        foreach (tClass k in keywordsearch)
        //        {
        //            list.Add(new CClassViewModel(k));
        //        }
        //    }
        //    return View(list);
        //}

        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];


            var table = from p in new JJMdbEntities().tClass
                        select p;

            var keyw = from k in new JJMdbEntities().tClass
                       where k.c_name.Contains(keyword)

                       select k;

            List<CClassViewModel> list = new List<CClassViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tClass c in table)
                {
                    list.Add(new CClassViewModel(c));

                }
            }
            else
            {
                foreach (tClass c in keyw)
                {
                    list.Add(new CClassViewModel(c));

                }
            }
            return View(list);

        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tClass clas = db.tClass.FirstOrDefault(c => c.classID == id);

            if (clas != null)
            {
                db.tClass.Remove(clas);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tClass clas = (new JJMdbEntities()).tClass.FirstOrDefault(c=>c.classID==id);
            var classList = new CClassViewModel(clas);


            if (clas == null)
                return RedirectToAction("List");

            return View(classList);
        }
        [HttpPost]
        public ActionResult Edit(tClass modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tClass clas = db.tClass.FirstOrDefault(c => c.classID == modify.classID);

            if (modify.image1 != null)
            {
                int point1 = modify.image1.FileName.IndexOf(".");
                string extention = modify.image1.FileName.Substring(point1, modify.image1.FileName.Length - point1);
                string photoName = Guid.NewGuid().ToString() + extention;
                string oldphoto = clas.c_imgPath1;
                modify.image1.SaveAs(Server.MapPath(@"~/Content/imgClass/" + photoName));
                try
                {
                    if (oldphoto != "/Content/imgClass/defaultClass.png")
                    {
                        System.IO.File.Delete(Server.MapPath(@"~/Content/imgClass/" + oldphoto));
                    }
                    
                }
                catch
                {
                }
                modify.c_imgPath1 = @"/Content/imgClass/" + photoName;
                clas.c_imgPath1 = modify.c_imgPath1;
            }

            if (modify.image2 != null)
            {
                int point2 = modify.image2.FileName.IndexOf(".");
                string extention = modify.image2.FileName.Substring(point2, modify.image2.FileName.Length - point2);
                string photoName = Guid.NewGuid().ToString() + extention;
                string oldphoto = clas.c_imgPath2;
                modify.image2.SaveAs(Server.MapPath(@"~/Content/imgClass/" + photoName));
                try
                {
                    if (oldphoto != "/Content/imgClass/defaultClass.png")
                    {
                        System.IO.File.Delete(Server.MapPath(@"~/Content/imgClass/" + oldphoto));
                    }
                }
                catch
                {
                }
                modify.c_imgPath2 = @"/Content/imgClass/" + photoName;
                clas.c_imgPath2 = modify.c_imgPath2;
            }

            if (modify.image3 != null)
            {
                int point3 = modify.image3.FileName.IndexOf(".");
                string extention = modify.image3.FileName.Substring(point3, modify.image3.FileName.Length - point3);
                string photoName = Guid.NewGuid().ToString() + extention;
                string oldphoto = clas.c_imgPath3;
                modify.image3.SaveAs(Server.MapPath(@"~/Content/imgClass/" + photoName));
                try
                {
                    if (oldphoto != "/Content/imgClass/defaultClass.png")
                    {
                        System.IO.File.Delete(Server.MapPath(@"~/Content/imgClass/" + oldphoto));
                    }
                }
                catch
                {
                }
                modify.c_imgPath3 = @"/Content/imgClass/" + photoName;
                clas.c_imgPath3 = modify.c_imgPath3;
            }

            if (clas != null)
            {
                clas.c_discount = modify.c_discount;
                clas.c_endTime  = modify.c_endTime;
                clas.c_hourRate = modify.c_hourRate;
                clas.c_intro = modify.c_intro;
                clas.c_lable1 = modify.c_lable1;
                clas.c_lable2 = modify.c_lable2;
                clas.c_lable3 = modify.c_lable3;
                clas.c_lable4 = modify.c_lable4;
                clas.c_lable5 = modify.c_lable5;
                clas.c_level = modify.c_level;
                clas.c_location = modify.c_location;
                clas.c_maxStudent = modify.c_maxStudent;
                clas.c_minStudent = modify.c_minStudent;
                clas.c_name = modify.c_name;
                clas.c_nameText = modify.c_nameText;
                clas.c_onsaleEnd = modify.c_onsaleEnd;
                clas.c_onsaleStart = modify.c_onsaleStart;
                clas.c_price = modify.c_price;
                clas.c_rate = modify.c_rate;
                clas.c_rateTotal = modify.c_rateTotal;
                clas.c_registerEnd = modify.c_registerEnd;
                clas.c_registerStart = modify.c_registerStart;
                clas.c_requirement = modify.c_requirement;
                clas.c_startTime = modify.c_startTime;
                clas.c_student = modify.c_student;
                clas.teacherID = modify.teacherID;
                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tClass c)
        {
            if (c.image1 != null) 
            {
                int point1 = c.image1.FileName.IndexOf(".");
                string extention1 = c.image1.FileName.Substring(point1, c.image1.FileName.Length - point1);
                string photoName1 = Guid.NewGuid().ToString() + extention1;
                c.image1.SaveAs(Server.MapPath(@"~/Content/imgClass/" + photoName1));
                c.c_imgPath1 = @"/Content/imgClass/" + photoName1;
            }
            else
            {
                c.c_imgPath1 = @"/Content/imgClass/defaultClass.png";
            }

            if (c.image2 != null)
            {
                int point2 = c.image2.FileName.IndexOf(".");
                string extention2 = c.image2.FileName.Substring(point2, c.image2.FileName.Length - point2);
                string photoName2 = Guid.NewGuid().ToString() + extention2;
                c.image2.SaveAs(Server.MapPath(@"~/Content/imgClass/" + photoName2));
                c.c_imgPath2 = @"/Content/imgClass/" + photoName2;
            }
            else
            {
                c.c_imgPath2 = @"/Content/imgClass/defaultClass.png";
            }

            if (c.image3 != null)
            {
                int point3 = c.image3.FileName.IndexOf(".");
                string extention3 = c.image3.FileName.Substring(point3, c.image3.FileName.Length - point3);
                string photoName3 = Guid.NewGuid().ToString() + extention3;
                c.image3.SaveAs(Server.MapPath(@"~/Content/imgClass/" + photoName3));
                c.c_imgPath3 = @"/Content/imgClass/" + photoName3;
            }
            else
            {
                c.c_imgPath3 = @"/Content/imgClass/defaultClass.png";
            }

            //if ((string.IsNullOrEmpty(c.c_imgPath1)) && (string.IsNullOrEmpty(c.c_imgPath2)) && (string.IsNullOrEmpty(c.c_imgPath3)))
            //{
            //    return RedirectToAction("Create");
            //}

            var classList = new CClassViewModel(c);

            JJMdbEntities db = new JJMdbEntities();
            
            c.c_getNow = DateTime.Now;
                db.tClass.Add(c);
                db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}