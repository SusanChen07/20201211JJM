using prjJJM.ViewModels;
using prjJJM.ViewModels.ClassVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjJJM.Controllers
{

    public class ClassMgtController : Controller
    {
        // GET: ClassMgt
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];


            var table = from p in new JJMdbEntities().tClass
                        select p;

            var keyw = from k in new JJMdbEntities().tClass
                       where k.c_name.Contains(keyword)

                       select k;

            List<CClassMgtViewModel> list = new List<CClassMgtViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tClass c in table)
                {
                    list.Add(new CClassMgtViewModel(c));

                }
            }
            else
            {
                foreach (tClass c in keyw)
                {
                    list.Add(new CClassMgtViewModel(c));

                }
            }
            return View(list);

        }

        public ActionResult CreateClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateClass(tClass c, CClassTypeViewModel ctype)
        {

            if (c.image1 != null)
            {
                int point1 = c.image1.FileName.IndexOf(".");
                string extention1 = c.image1.FileName.Substring(point1, c.image1.FileName.Length - point1);
                string photoName1 = Guid.NewGuid().ToString() + extention1;
                c.image1.SaveAs(Server.MapPath("~/Content/imgClass/" + photoName1));
                c.c_imgPath1 = "/Content/imgClass/" + photoName1;
            }
            else 
            {
                c.c_imgPath1 = "/Content/imgClass/defaultClass.png";
            }
            if (c.image2 != null)
            {
                int point2 = c.image2.FileName.IndexOf(".");
                string extention2 = c.image2.FileName.Substring(point2, c.image2.FileName.Length - point2);
                string photoName2 = Guid.NewGuid().ToString() + extention2;
                c.image2.SaveAs(Server.MapPath("~/Content/imgClass/" + photoName2));
                c.c_imgPath2 = "/Content/imgClass/" + photoName2;
            }
            else
            {
                c.c_imgPath2 = "/Content/imgClass/defaultClass.png";
            }
            if (c.image3 != null)
            {
                int point3 = c.image3.FileName.IndexOf(".");
                string extention3 = c.image3.FileName.Substring(point3, c.image3.FileName.Length - point3);
                string photoName3 = Guid.NewGuid().ToString() + extention3;
                c.image3.SaveAs(Server.MapPath("~/Content/imgClass/" + photoName3));
                c.c_imgPath3 = "/Content/imgClass/" + photoName3;
            }
            else 
            {
                c.c_imgPath3 = "/Content/imgClass/defaultClass.png";
            }

            string lab = ctype.ClassType;
            string lab2 = ctype.ClassSector;
            c.c_lable1 = lab;
            c.c_lable2 = lab2;
            c.c_level = ctype.LevelType;
            c.c_lable4 = ctype.LocationType; //新增地區tag

            if (string.IsNullOrEmpty(c.c_name)) /*後端驗證*/
                return RedirectToAction("List");

            JJMdbEntities db = new JJMdbEntities();
            c.teacherID = (int)Session["tID"];
            c.c_getNow = DateTime.Now;
            c.c_rate = 0;
            c.c_student = 0;
            c.c_rateTotal = 0; //新增課程被評價次數
            db.tClass.Add(c);
            db.SaveChanges();

            return RedirectToAction("List","OnlyClass",new { id=c.classID});
        }
    


        public ActionResult TrxList()
        {

            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                   join t in db.tTeacher on m.memberID equals t.memberID
                                   join c in db.tClass on t.teacherID equals c.teacherID
                                   where c.c_lable2 == "TRX"
                                   select new classMgtList
                                   {
                                       classID = c.classID,
                                       c_imgPath1 = c.c_imgPath1,
                                       c_level = c.c_level,
                                       c_name = c.c_name,
                                       c_hourRate = c.c_hourRate,
                                       c_price = c.c_price,
                                       c_discount = c.c_discount,
                                       c_rate = c.c_rate,
                                       c_rateTotal = c.c_rateTotal,
                                       teacherID = c.teacherID,
                                       memberID = m.memberID,
                                       m_firstName = m.m_firstName,
                                       m_lastName = m.m_lastName,
                                       m_imgPath = m.m_imgPath

                                   }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
		
        [HttpPost]
        public ActionResult TrxList(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if(search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where   c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost)||m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1&&(m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1&&(m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where  c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1&&(m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList()?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost 
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost 
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List瑜珈()
        {

            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "瑜珈"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);

        }
        [HttpPost]
        public ActionResult List瑜珈(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List重訓()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "重訓"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List重訓(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List武術()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "武術"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List武術(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List拳擊()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "拳擊"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List拳擊(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List其它()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "其它"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List其它(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List籃球()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "籃球"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List籃球(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List足球()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "足球"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List足球(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List衝浪()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "衝浪"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List衝浪(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List游泳()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "游泳"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List游泳(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List網球()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "網球"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List網球(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
        public ActionResult List棒球()
        {
            JJMdbEntities db = new JJMdbEntities();

            var imgForClassListT = (from m in db.tMember
                                    join t in db.tTeacher on m.memberID equals t.memberID
                                    join c in db.tClass on t.teacherID equals c.teacherID
                                    where c.c_lable2 == "棒球"
                                    select new classMgtList
                                    {
                                        classID = c.classID,
                                        c_imgPath1 = c.c_imgPath1,
                                        c_level = c.c_level,
                                        c_name = c.c_name,
                                        c_hourRate = c.c_hourRate,
                                        c_price = c.c_price,
                                        c_discount = c.c_discount,
                                        c_rate = c.c_rate,
                                        c_rateTotal = c.c_rateTotal,
                                        teacherID = c.teacherID,
                                        memberID = m.memberID,
                                        m_firstName = m.m_firstName,
                                        m_lastName = m.m_lastName,
                                        m_imgPath = m.m_imgPath

                                    }).ToList();


            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            cMgtVM.classData = imgForClassListT;

            return View(cMgtVM);
        }
        [HttpPost]
        public ActionResult List棒球(CClassMgtImgViewModel vm)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel search = new CClassMgtImgViewModel();
            search.areaPost = vm.areaPost;//地區
            search.keywordPost = vm.keywordPost;//關鍵字
            search.種類Post = vm.種類Post;//關鍵字種類
            search.startdatePost = vm.startdatePost;//起始日期
            search.enddatePost = vm.enddatePost;//結束日期
            search.optionsRadios = vm.optionsRadios;//課程班別
            if (search.種類Post == "1")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_lable1.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "2")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && c.c_name.Contains(search.keywordPost)
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else if (search.種類Post == "3")
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1 && (m.m_lastName.Contains(search.keywordPost) || m.m_lastName.Contains(search.keywordPost))
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            else
            {
                if (search.optionsRadios == "option1")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath,
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option2")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent > 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
                if (search.optionsRadios == "option3")
                {
                    if (search.areaPost == "1")
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                    else
                    {
                        var imgForClassListT = (from m in db.tMember
                                                join t in db.tTeacher on m.memberID equals t.memberID
                                                join c in db.tClass on t.teacherID equals c.teacherID
                                                where c.c_startTime > search.startdatePost && c.c_endTime < search.enddatePost && c.c_lable4 == search.areaPost && c.c_maxStudent == 1
                                                select new classMgtList
                                                {
                                                    classID = c.classID,
                                                    c_imgPath1 = c.c_imgPath1,
                                                    c_level = c.c_level,
                                                    c_name = c.c_name,
                                                    c_hourRate = c.c_hourRate,
                                                    c_price = c.c_price,
                                                    c_discount = c.c_discount,
                                                    c_rate = c.c_rate,
                                                    c_rateTotal = c.c_rateTotal,
                                                    teacherID = c.teacherID,
                                                    memberID = m.memberID,
                                                    m_firstName = m.m_firstName,
                                                    m_lastName = m.m_lastName,
                                                    m_imgPath = m.m_imgPath
                                                });
                        CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
                        cMgtVM.classData = imgForClassListT.ToList() ?? new List<classMgtList>();
                        return View(cMgtVM);
                    }
                }
            }
            return View();
        }
    }
}