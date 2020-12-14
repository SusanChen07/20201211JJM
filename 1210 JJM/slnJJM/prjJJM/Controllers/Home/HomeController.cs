using prjJJM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using prjJJM.ViewModels.HomeVM;

namespace prjJJM.Controllers
{
    public class HomeController : Controller
    {

        JJMdbEntities db = new JJMdbEntities();

        public ActionResult Index(string txtkey = null)
        {
            var 熱門課程 = (from c in db.tClass
                        join t in db.tTeacher on c.teacherID equals t.teacherID
                        join m in db.tMember on t.memberID equals m.memberID
                        select new popularName
                        {
                            memberID = m.memberID,
                            m_firstName = m.m_firstName,
                            m_lastName = m.m_lastName,
                            m_imgPath = m.m_imgPath,
                            teacherID = t.teacherID,
                            t_title = t.t_title,
                            t_rateAvg = t.t_rateAvg,
                            classID = c.classID,
                            c_level = c.c_level,
                            c_name = c.c_name,
                            c_price = c.c_price,
                            c_hourRate = c.c_hourRate,
                            c_rate = c.c_rate,
                            c_rateTotal = c.c_rateTotal,
                            c_imgPath1 = c.c_imgPath1,
                            c_imgPath2 = c.c_imgPath2
                        }).OrderByDescending(x => x.c_rate).Take(4).ToList();

            var 一對一課程 = (from c in db.tClass
                         join t in db.tTeacher on c.teacherID equals t.teacherID
                         join m in db.tMember on t.memberID equals m.memberID
                         where c.c_maxStudent == 1
                         select new popularName
                         {
                             memberID = m.memberID,
                             m_firstName = m.m_firstName,
                             m_lastName = m.m_lastName,
                             m_imgPath = m.m_imgPath,
                             teacherID = t.teacherID,
                             t_title = t.t_title,
                             t_rateAvg = t.t_rateAvg,
                             classID = c.classID,
                             c_level = c.c_level,
                             c_name = c.c_name,
                             c_price = c.c_price,
                             c_hourRate = c.c_hourRate,
                             c_rate = c.c_rate,
                             c_rateTotal = c.c_rateTotal,
                             c_imgPath1 = c.c_imgPath1,
                             c_imgPath2 = c.c_imgPath2
                         }).OrderByDescending(x => x.c_rate).Take(4).ToList();

            var 團體課程 = (from c in db.tClass
                        join t in db.tTeacher on c.teacherID equals t.teacherID
                        join m in db.tMember on t.memberID equals m.memberID
                        where c.c_maxStudent != 1
                        select new popularName
                        {
                            memberID = m.memberID,
                            m_firstName = m.m_firstName,
                            m_lastName = m.m_lastName,
                            m_imgPath = m.m_imgPath,
                            teacherID = t.teacherID,
                            t_title = t.t_title,
                            t_rateAvg = t.t_rateAvg,
                            classID = c.classID,
                            c_level = c.c_level,
                            c_name = c.c_name,
                            c_price = c.c_price,
                            c_hourRate = c.c_hourRate,
                            c_rate = c.c_rate,
                            c_rateTotal = c.c_rateTotal,
                            c_imgPath1 = c.c_imgPath1,
                            c_imgPath2 = c.c_imgPath2
                        }).OrderByDescending(x => x.c_rate).Take(4).ToList();

            var 熱門教練 = (from t in db.tTeacher 
                        join m in db.tMember on t.memberID equals m.memberID
                        select new popularName
                        {
                            memberID = m.memberID,
                            m_firstName = m.m_firstName,
                            m_lastName = m.m_lastName,
                            m_imgPath = m.m_imgPath,
                            teacherID = t.teacherID,
                            t_title = t.t_title,
                            t_certificateTxt = t.t_certificateTxt,
                            t_rateAvg = t.t_rateAvg,

                        }).OrderByDescending(e => e.t_rateAvg).Take(10).ToList();


            CPopularHomeViewModel vm = new CPopularHomeViewModel()
            {
                popularClass = 熱門課程,
                popular1v1 = 一對一課程,
                popularGroup = 團體課程,
                popularTeacher = 熱門教練,
            };

            return View(vm);

            //return View("Index");
        }

        //登入
        public ActionResult Login()
        {
            return View("Index");
        }
        [HttpPost]
        public ActionResult Login(CUserViewModel person)
        {
            string username = person.Username;
            string password = person.Password;

            var member = db.tMember
                .Where(m => m.m_email == username && m.m_password == password)
                .FirstOrDefault();

            string message = "";
            if (member == null)
            {
                message = "fail";
            }
            else
            {
                var teacher = db.tTeacher
                              .Where(t => t.memberID == member.memberID)
                              .FirstOrDefault();

                Session["WelCome"] = member.m_firstName;
                Session["Member"] = member;
                Session["ID"] = member.memberID;
                Session["Role"] = member.m_role.ToString();
                Session["Image"] = member.m_imgPath;
                if (teacher != null)
                {
                    Session["tID"] = teacher.teacherID;
                }

                message = "pass";
            }

            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        //註冊
        public ActionResult Register()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Register(tMember m)
        {
            string email = m.m_email;
            var member = db.tMember
            .Where(p => p.m_email == email)
            .FirstOrDefault();


            string msg = "";
            if (member != null) /*後端驗證*/
            {
                //TempData["message"] = "Email已經被註冊, 請換一個重新註冊!";
                //return RedirectToAction("Index");
                msg = "emaildouble";
            }
            else 
            { 
            JJMdbEntities db = new JJMdbEntities();

            m.m_imgPath = "/Content/imgMember/defaultMember.png";
            m.m_Jpoint = 0;
            m.m_getNow = DateTime.Now;
            m.m_role = 1;
            db.tMember.Add(m);
            db.SaveChanges();
                //TempData["message"] = "恭喜您註冊成功!";
                //return RedirectToAction("Index");
            msg = "ok";
            }

            if (Request.IsAjaxRequest())
            {
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        //登出
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        //導航列搜尋
        public ActionResult Search(string txtkey)
        {
            JJMdbEntities db = new JJMdbEntities();
            var 所有課程 = (from c in db.tClass
                        join t in db.tTeacher on c.teacherID equals t.teacherID
                        join m in db.tMember on t.memberID equals m.memberID
                        where c.c_name.Contains(txtkey)
                        select new popularName
                        {
                            memberID = m.memberID,
                            m_firstName = m.m_firstName,
                            m_lastName = m.m_lastName,
                            m_imgPath = m.m_imgPath,
                            teacherID = t.teacherID,
                            t_title = t.t_title,
                            t_rateAvg = t.t_rateAvg,
                            classID = c.classID,
                            c_level = c.c_level,
                            c_name = c.c_name,
                            c_price = c.c_price,
                            c_hourRate = c.c_hourRate,
                            c_rate = c.c_rate,
                            c_rateTotal = c.c_rateTotal,
                            c_imgPath1 = c.c_imgPath1,
                            c_imgPath2 = c.c_imgPath2,
                            c_discount=c.c_discount
                        }).ToList();
            CPopularHomeViewModel vm = new CPopularHomeViewModel()
            {
                allClass=所有課程,
                popular1v1=new List<popularName> { }
            };


            return View(vm);
        }























        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}