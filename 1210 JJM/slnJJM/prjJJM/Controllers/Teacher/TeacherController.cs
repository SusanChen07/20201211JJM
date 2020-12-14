using prjJJM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjJJM.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            List<CTeacherViewModel> list = new List<CTeacherViewModel>();

            if (string.IsNullOrEmpty(keyword))
            {
                var table = from t in (new JJMdbEntities()).tTeacher
                            select t;
                foreach (tTeacher t in table)
                {
                    list.Add(new CTeacherViewModel(t));
                }
            }
            else
            {
                var keywordsearch = from t in (new JJMdbEntities()).tTeacher
                                    where t.t_title.Contains(keyword)
                                    select t;
                foreach (tTeacher t in keywordsearch)
                {
                    list.Add(new CTeacherViewModel(t));
                }
            }

            return View(list);
        }


        public ActionResult Edit(int id)
        {
            tTeacher tT = (new JJMdbEntities()).tTeacher.FirstOrDefault(t => t.teacherID == id);
            var teacherList = new CTeacherViewModel(tT);
            
            if (tT == null)
            {
                return RedirectToAction("List");
            }
            return View(teacherList);
        }
        [HttpPost]
        public ActionResult Edit(tTeacher modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tTeacher tT = db.tTeacher.FirstOrDefault(t => t.teacherID == modify.teacherID);

            if (modify.image1 != null)
            {
                int point = modify.image1.FileName.IndexOf(".");
                string extention = modify.image1.FileName.Substring(point, modify.image1.FileName.Length - point);
                string photoName = Guid.NewGuid().ToString() + extention;
                string oldphoto = tT.t_certificateImg1;
                modify.image1.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName));
                try
                {
                    if (oldphoto != "/Content/imgTeacher/defaultTeacher.png")
                    {
                        System.IO.File.Delete(Server.MapPath(@"~/Content/imgTeacher/" + oldphoto));
                    }
                    
                }
                catch
                {
                }
                modify.t_certificateImg1 = @"/Content/imgTeacher/" + photoName;
                tT.t_certificateImg1 = modify.t_certificateImg1;
            }

            if (modify.image2 != null)
            {
                int point = modify.image2.FileName.IndexOf(".");
                string extention = modify.image2.FileName.Substring(point, modify.image2.FileName.Length - point);
                string photoName = Guid.NewGuid().ToString() + extention;
                string oldphoto = tT.t_certificateImg2;
                modify.image2.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName));
                try
                {
                    if (oldphoto != "/Content/imgTeacher/defaultTeacher.png")
                    {
                        System.IO.File.Delete(Server.MapPath(@"~/Content/imgTeacher/" + oldphoto));
                    }
                }
                catch
                {
                }
                modify.t_certificateImg2 = @"/Content/imgTeacher/" + photoName;
                tT.t_certificateImg2 = modify.t_certificateImg2;
            }

            if (modify.image3 != null)
            {
                int point = modify.image3.FileName.IndexOf(".");
                string extention = modify.image3.FileName.Substring(point, modify.image3.FileName.Length - point);
                string photoName = Guid.NewGuid().ToString() + extention;
                string oldphoto = tT.t_certificateImg3;
                modify.image3.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName));
                try
                {
                    if (oldphoto != "/Content/imgTeacher/defaultTeacher.png")
                    {
                        System.IO.File.Delete(Server.MapPath(@"~/Content/imgTeacher/" + oldphoto));
                    }
                }
                catch
                {
                }
                modify.t_certificateImg3 = @"/Content/imgTeacher/" + photoName;
                tT.t_certificateImg3 = modify.t_certificateImg3;
            }

            if (tT != null)
            {
                tT.t_title = modify.t_title;
                tT.t_intro = modify.t_intro;
                tT.t_certificateTxt = modify.t_certificateTxt;
                tT.t_experience = modify.t_experience;
                tT.t_expertise = modify.t_expertise;
                tT.t_socialMedia1 = modify.t_socialMedia1;
                tT.t_socialMedia2 = modify.t_socialMedia2;
                tT.t_socialMedia3 = modify.t_socialMedia3;
                tT.t_socialMedia4 = modify.t_socialMedia4;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tTeacher tT = db.tTeacher.FirstOrDefault(t => t.teacherID == id);

            if (tT != null)
            {
                db.tTeacher.Remove(tT);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tTeacher t)
        {
            if (t.image1 != null)
            {
                int point1 = t.image1.FileName.IndexOf(".");
                string extention1 = t.image1.FileName.Substring(point1, t.image1.FileName.Length - point1);
                string photoName1 = Guid.NewGuid().ToString() + extention1;
                t.image1.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName1));
                t.t_certificateImg1 = @"/Content/imgTeacher/" + photoName1;
            }
            else
            {
                t.t_certificateImg1 = @"/Content/imgTeacher/defaultTeacher.png";
            }

            if (t.image2 != null)
            {
                int point2 = t.image2.FileName.IndexOf(".");
                string extention2 = t.image2.FileName.Substring(point2, t.image2.FileName.Length - point2);
                string photoName2 = Guid.NewGuid().ToString() + extention2;
                t.image2.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName2));
                t.t_certificateImg2 = @"/Content/imgTeacher/" + photoName2;
            }
            else
            {
                t.t_certificateImg2 = @"/Content/imgTeacher/defaultTeacher.png";
            }

            if (t.image3 != null)
            {
                int point3 = t.image3.FileName.IndexOf(".");
                string extention3 = t.image3.FileName.Substring(point3, t.image3.FileName.Length - point3);
                string photoName3 = Guid.NewGuid().ToString() + extention3;
                t.image3.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName3));
                t.t_certificateImg3 = @"/Content/imgTeacher/" + photoName3;
            }
            else
            {
                t.t_certificateImg3 = @"/Content/imgTeacher/defaultTeacher.png";
            }


            JJMdbEntities db = new JJMdbEntities();
            t.t_getNow = DateTime.Now;
            db.tTeacher.Add(t);
            db.SaveChanges();

            return RedirectToAction("List");
        }

    }


}