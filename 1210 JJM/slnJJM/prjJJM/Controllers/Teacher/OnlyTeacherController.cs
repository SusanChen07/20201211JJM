using prjJJM.ViewModels.TeacherVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;

namespace prjJJM.Controllers.Teacher
{
    public class OnlyTeacherController : Controller
    {
        JJMdbEntities db = new JJMdbEntities();

        // GET: OnlyTeacher
        public ActionResult List(int id)
        {
            var tT = db.tTeacher.FirstOrDefault(m => m.memberID == id);
            CTeacherViewModel CTeacher = new CTeacherViewModel(tT);

            tMember tM = db.tMember.FirstOrDefault(m => m.memberID == id);
            CTeacher.memberForOnlyT = tM;

            //tMember mem = db.tMember.FirstOrDefault(m => m.memberID == id);

            //var record = from p in db.tMember
            //             where p.memberID == mem.memberID
            //             select p;

            //tTeacher teacher = db.tTeacher.FirstOrDefault(t => t.memberID == id);


            //if (teacher != null)
            //{
            //    var record = from t in new JJMdbEntities().tTeacher
            //                 where t.teacherID == teacher.teacherID
            //                 select t;

            //    foreach (tTeacher t in record)
            //    {
            //        list.Add(new CTeacherMgtViewModel(t));
            //    }

            //}

            return View(CTeacher);

        }

        public ActionResult DeleteClass(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tClass classData = db.tClass.FirstOrDefault(x => x.classID == id);
            if(classData != null)
            {
                db.tClass.Remove(classData);
                db.SaveChanges();
            }

            return RedirectToAction("myClass");
        }

        public ActionResult OnlyList(int id)
        {
            COnlyTeacherViewModel tVM = new COnlyTeacherViewModel();

            var teacherD = db.tTeacher.FirstOrDefault(m => m.memberID == id);
            var classD = db.tClass.Where(m => m.teacherID == id).ToList();

            tVM.teacherForOnlyT = teacherD;
            tVM.classForOnlyT = classD;

            return View(tVM);
        }
        public ActionResult EditClass(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tClass classData = db.tClass.FirstOrDefault(x => x.classID == id);
            if (classData == null)
            {
                return RedirectToAction("myClass");
            }
            CClassViewModel cVM = new CClassViewModel(classData);


            return View(cVM);
        }
        [HttpPost]
        public ActionResult EditClass(tClass modify)
        {
            //這整段是抄class控制器的編輯的 沒有測試過
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
                clas.c_endTime = modify.c_endTime;
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
                //clas.c_rate = modify.c_rate;
                clas.c_registerEnd = modify.c_registerEnd;
                clas.c_registerStart = modify.c_registerStart;
                clas.c_requirement = modify.c_requirement;
                clas.c_startTime = modify.c_startTime;
                //clas.c_student = modify.c_student;
                //clas.teacherID = modify.teacherID;
                db.SaveChanges();

            }

            return RedirectToAction("myClass");
        }
        public ActionResult Edit(int id)
        {
            tTeacher tT = (new JJMdbEntities()).tTeacher.FirstOrDefault(t => t.teacherID == id);
            var COnlyT = new CTeacherViewModel(tT);

            if (tT == null)
            {
                return RedirectToAction("List");
            }
            return View(COnlyT);
        }

        [HttpPost]
        public ActionResult Edit(tTeacher modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tTeacher tT = db.tTeacher.FirstOrDefault(t => t.teacherID == modify.teacherID);
            CTeacherViewModel CTeacher = new CTeacherViewModel(tT);

            tMember tM = db.tMember.FirstOrDefault(m => m.memberID == modify.memberID);
            CTeacher.memberForOnlyT = tM;


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
            return RedirectToAction("List", "OnlyTeacher", new { id = tT.memberID });
        }

        public ActionResult myClass()
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = (int)Session["ID"];

            COnlyTeacherViewModel tVM = new COnlyTeacherViewModel();
            var member = db.tTeacher.FirstOrDefault(x => (int)x.memberID == id);
            var classD = (db.tClass.Where(z => z.teacherID == member.teacherID)).ToList();
            List<COnlyTeacherViewModel.memberForClass> ListMember = new List<COnlyTeacherViewModel.memberForClass>();
            
            foreach (var i in classD)
            {
                var od = db.tOrder_Detail.Where(a => a.classID == i.classID);
                foreach(var j in od)
                {
                    
                    COnlyTeacherViewModel.memberForClass memberData = new COnlyTeacherViewModel.memberForClass();
                    memberData.classID = (int)j.classID;
                    memberData.m_name = j.tOrder.tMember.m_fullName;
                    memberData.m_birth = j.tOrder.tMember.m_birth;
                    memberData.m_email = j.tOrder.tMember.m_email;
                    memberData.m_gender = j.tOrder.tMember.m_gender;
                    memberData.m_phone = j.tOrder.tMember.m_phone;
                    ListMember.Add(memberData);
                }
            }

            tVM.memForT = ListMember;
            tVM.classData = classD;

            return View(tVM);
        }
        public ActionResult becomeT()
        {
            return View();
        }
        [HttpPost]
        public ActionResult becomeT(tTeacher t)
        {
            JJMdbEntities db = new JJMdbEntities();
            var id = (int)Session["ID"];
            tMember tM = db.tMember.FirstOrDefault(m => m.memberID == id);
            if (t.image1 != null)
            {
                int point1 = t.image1.FileName.IndexOf(".");
                string extention1 = t.image1.FileName.Substring(point1, t.image1.FileName.Length - point1);
                string photoName1 = Guid.NewGuid().ToString() + extention1;
                t.image1.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName1));
                t.t_certificateImg1 = @"/Content/imgTeacher/" + photoName1;
            }
            else { t.t_certificateImg1 = @"/Content/imgTeacher/defaultTeacher.png"; }

            if (t.image2 != null)
            {
                int point2 = t.image2.FileName.IndexOf(".");
                string extention2 = t.image2.FileName.Substring(point2, t.image2.FileName.Length - point2);
                string photoName2 = Guid.NewGuid().ToString() + extention2;
                t.image2.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName2));
                t.t_certificateImg2 = @"/Content/imgTeacher/" + photoName2;
            }
            else { t.t_certificateImg2 = @"/Content/imgTeacher/defaultTeacher.png"; }

            if (t.image3 != null)
            {
                int point3 = t.image3.FileName.IndexOf(".");
                string extention3 = t.image3.FileName.Substring(point3, t.image3.FileName.Length - point3);
                string photoName3 = Guid.NewGuid().ToString() + extention3;
                t.image3.SaveAs(Server.MapPath(@"~/Content/imgTeacher/" + photoName3));
                t.t_certificateImg3 = @"/Content/imgTeacher/" + photoName3;
            }
            else { t.t_certificateImg3 = @"/Content/imgTeacher/defaultTeacher.png"; }

            t.t_intro = t.t_intro;
            t.t_expertise = t.t_expertise;
            t.t_title = t.t_title;
            t.t_experience = t.t_experience;
            t.t_certificateTxt = "";
            t.t_socialMedia1 = "";
            t.t_socialMedia2 = "";
            t.t_socialMedia3 = "";
            t.t_socialMedia4 = "";

            t.memberID=(int)Session["ID"];
            t.t_messageTotal = 0;
            t.t_moneyTotal = 0;
            t.t_rateAvg = 0;
            t.t_studentTotal = 0;
            t.t_money = 0;
            t.t_classTotal = 0;
            t.t_getNow = DateTime.Now;

            db.tTeacher.Add(t);

            tM.m_role = 2;
            db.SaveChanges();
            TempData["message"] = "恭喜您成為教練，請重新登入!";
            return RedirectToAction("Logout", "Home");
        }
    }
}