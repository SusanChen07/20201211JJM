using prjJJM.ViewModels.ClassVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjJJM.Controllers
{
    public class OnlyClassController : Controller
    {
        // GET: OnlyClass
        public ActionResult List(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            CClassMgtImgViewModel cMgtVM = new CClassMgtImgViewModel();
            tClass clas = db.tClass.FirstOrDefault(c => c.classID == id);
            if (clas != null)
            {
                var imgForOnlyClass = (from m in db.tMember
                                       join t in db.tTeacher on m.memberID equals t.memberID
                                       join c in db.tClass on t.teacherID equals c.teacherID
                                       where c.classID == id
                                       select new classMgtList
                                       {
                                           classID = c.classID,
                                           c_imgPath1 = c.c_imgPath1,
                                           c_level = c.c_level,
                                           c_name = c.c_name,
                                           c_nameText = c.c_nameText,
                                           c_intro = c.c_intro,
                                           c_requirement = c.c_requirement,
                                           c_hourRate = c.c_hourRate,
                                           c_price = c.c_price,
                                           c_discount = c.c_discount,
                                           c_onsaleEnd = (DateTime)c.c_onsaleEnd,
                                           c_location = c.c_location,
                                           c_startTime = (DateTime)c.c_startTime,
                                           c_endTime = (DateTime)c.c_endTime,
                                           c_minStudent = c.c_minStudent,
                                           c_maxStudent = c.c_maxStudent,
                                           c_registerEnd = (DateTime)c.c_registerEnd,
                                           c_imgPath2 = c.c_imgPath2,
                                           c_imgPath3 = c.c_imgPath3,
                                           c_rate = c.c_rate,
                                           c_rateTotal = c.c_rateTotal,
                                           c_lable1 = c.c_lable1,
                                           c_lable2 = c.c_lable2,
                                           c_lable3 = c.c_lable3,
                                           c_lable4 = c.c_lable4,
                                           c_lable5 = c.c_lable5,
                                           teacherID = c.teacherID,
                                           t_certificateTxt = t.t_certificateTxt,
                                           t_title = t.t_title,
                                           memberID = m.memberID,
                                           m_firstName = m.m_firstName,
                                           m_lastName = m.m_lastName,
                                           m_imgPath = m.m_imgPath

                                       }).ToList();

                cMgtVM.classData = imgForOnlyClass;

                var ratingForOnlyClass = (from m in db.tMember
                                          join r in db.tRating on m.memberID equals r.memberID
                                          join c in db.tClass on r.classID equals c.classID
                                          
                                          where c.classID == id
                                          select new ratingList
                                          {
                                              m_firstName = m.m_firstName,
                                              m_lastName = m.m_lastName,
                                              m_imgPath = m.m_imgPath,
                                              r_star = r.r_star,
                                              r_sendText = r.r_sendText,
                                              r_sendTime = (DateTime)r.r_sendTime,

                                          }).ToList();

                cMgtVM.ratingData = ratingForOnlyClass;
            }

            return View(cMgtVM);

        }
    }
}