using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjJJM.Controllers.FAQ
{
    public class FAQController : Controller
    {
        // GET: FAQ
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Question()
        {
            return View();
        }

        public ActionResult ContactJJM()
        {
            return View();
        }

        public ActionResult 客服反映表單()
        {
            return View();
        }

        public ActionResult 客服滿意度調查()
        {
            return View();
        }

        public ActionResult 網站使用滿意度()
        {
            return View();
        }

        public ActionResult 課程滿意度調查()
        {
            return View();
        }

    }
}