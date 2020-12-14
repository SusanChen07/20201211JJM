using prjJJM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace prjJJM.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];


            var table = from p in new JJMdbEntities().tMember
                        select p;

            var keyw = from k in new JJMdbEntities().tMember
                       where k.m_firstName.Contains(keyword) || k.m_lastName.Contains(keyword)

                       select k;

            List<CMemberViewModel> list = new List<CMemberViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tMember m in table)
                {
                    list.Add(new CMemberViewModel(m));

                }
            }
            else
            {
                foreach (tMember m in keyw)
                {
                    list.Add(new CMemberViewModel(m));

                }
            }
            return View(list);

        }

        //新增
        public ActionResult Create()
        {
            return View();
        }

        //新增
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tMember m)
        {
            if (m.image != null)
            {
                int point1 = m.image.FileName.IndexOf(".");
                string extention1 = m.image.FileName.Substring(point1, m.image.FileName.Length - point1);
                string photoName1 = Guid.NewGuid().ToString() + extention1;
                m.image.SaveAs(Server.MapPath(@"~/Content/imgMember/" + photoName1));
                m.m_imgPath = @"/Content/imgMember/" + photoName1;
            }
            else
            {
                m.m_imgPath = "/Content/imgMember/defaultMember.png";
                
            }


            //if (string.IsNullOrEmpty(m.m_imgPath))
            //{
            //    return RedirectToAction("List");
            //}

            //if (string.IsNullOrEmpty(m.m_email)) /*後端驗證*/
            //{
            //    return RedirectToAction("Create");
            //}

            JJMdbEntities db = new JJMdbEntities();
            m.m_getNow = DateTime.Now;
            db.tMember.Add(m);
            db.SaveChanges();
            return RedirectToAction("List");

        }

        //修改
        public ActionResult Edit(int id)
        {
            tMember mem = new JJMdbEntities().tMember.FirstOrDefault(c => c.memberID == id);
            var memberList = new CMemberViewModel(mem);


            if (mem == null)
            {
                return RedirectToAction("List");
            }

            return View(memberList);
        }

        //修改
        [HttpPost]
        public ActionResult Edit(tMember modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tMember mem = db.tMember.FirstOrDefault(c => c.memberID == modify.memberID);

            if (modify.image != null)
            {
                int point = modify.image.FileName.IndexOf(".");
                string extention = modify.image.FileName.Substring(point, modify.image.FileName.Length - point);
                string photoName = Guid.NewGuid().ToString() + extention;
                string oldphoto = mem.m_imgPath;
                modify.image.SaveAs(Server.MapPath(@"~/Content/imgMember/" + photoName));
                try
                {
                    System.IO.File.Delete(Server.MapPath(@"~/Content/imgMember/" + oldphoto));
                }
                catch
                {
                }
                modify.m_imgPath = @"/Content/imgMember/" + photoName;
                mem.m_imgPath = modify.m_imgPath;
            }


            if (mem != null)
            {
                mem.m_firstName = modify.m_firstName;
                mem.m_lastName = modify.m_lastName;
                mem.m_birth = modify.m_birth;
                mem.m_gender = modify.m_gender;
                mem.m_email = modify.m_email;
                mem.m_emailConfirm = modify.m_emailConfirm;
                mem.m_password = modify.m_password;
                mem.m_phone = modify.m_phone;
                mem.m_district = modify.m_district;
                mem.m_address = modify.m_address;
                mem.m_role = modify.m_role;
                mem.m_hobby = modify.m_hobby;
                mem.m_imgPath = modify.m_imgPath;
                mem.m_Jpoint = modify.m_Jpoint;
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

        //刪除
        public ActionResult Delete(int id)
        {
            JJMdbEntities db = new JJMdbEntities();
            tMember Memb = db.tMember.FirstOrDefault(p => p.memberID == id);
            if (Memb != null)
            {
                db.tMember.Remove(Memb);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}