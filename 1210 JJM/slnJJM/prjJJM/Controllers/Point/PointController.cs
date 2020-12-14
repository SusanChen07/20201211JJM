using prjJJM.ViewModels;
using prjJJM.ViewModels.PointVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjJJM.Controllers.Point
{
    public class PointController : Controller
    {
        JJMdbEntities db = new JJMdbEntities();
        // GET: Point
        public ActionResult List()
        {
            var id = Convert.ToInt32(Session["ID"]);
            var member = db.tMember.FirstOrDefault(x => x.memberID == id);

            var 點數異動紀錄 = db.tDeposit.Where(x => x.memberID == id).ToList();
            CPointViewModel PointVM = new CPointViewModel();
            PointVM.儲值 = 點數異動紀錄;
            PointVM.會員 = member;
            return View(PointVM);
        }
        [HttpPost]
        public ActionResult List(CPointViewModel vm)
        {
            var id = Convert.ToInt32(Session["ID"]);
            JJMdbEntities db = new JJMdbEntities();
            tMember member = db.tMember.FirstOrDefault(p => p.memberID == id);
            vm.d_getNow = DateTime.Now;
            vm.d_method = "信用卡";
            vm.d_point = vm.getPoint;
            db.tDeposit.Add(new tDeposit
            {
                d_getNow = vm.d_getNow,
                memberID =id,
                d_method=vm.d_method,
                d_point=vm.d_point,
            });
            member.m_Jpoint = (int)member.m_Jpoint + vm.getPoint;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult money()
        {
            var id = Convert.ToInt32(Session["ID"]);
            var member = db.tMember.FirstOrDefault(x => x.memberID == id);
            var tid= db.tTeacher.FirstOrDefault(x => x.memberID == id);
            var 點數異動紀錄 = db.tPay.Where(x => x.teacherID == tid.teacherID).ToList();
            CPointViewModel PointVM = new CPointViewModel();
            PointVM.教練 = tid;
            PointVM.請款 = 點數異動紀錄;
            PointVM.會員 = member;
            return View(PointVM);
        }
        [HttpPost]
        public ActionResult money(CPointViewModel vm)
        {
            var id = Convert.ToInt32(Session["ID"]);
            JJMdbEntities db = new JJMdbEntities();
            var tid = db.tTeacher.FirstOrDefault(x => x.memberID == id);
            tMember member = db.tMember.FirstOrDefault(p => p.memberID == id);
            tTeacher teacher = db.tTeacher.FirstOrDefault(p => p.memberID == tid.memberID);
            vm.p_getNow = DateTime.Now;
            vm.p_method = "教練請款";
            vm.p_money = vm.getPoint;
            db.tPay.Add(new tPay
            {
                p_getNow = vm.p_getNow,
                teacherID = tid.teacherID,
                p_method = vm.p_method,
                p_money = vm.p_money,
                p_status="未匯款",
                p_memo="",
                p_getMoneyTime=Convert.ToDateTime("1990-01-01"),
            });
            teacher.t_money = (int)teacher.t_money - vm.getPoint;
            member.m_Jpoint = (int)member.m_Jpoint - vm.getPoint;
            db.SaveChanges();
            return RedirectToAction("money");
        }
    }
}