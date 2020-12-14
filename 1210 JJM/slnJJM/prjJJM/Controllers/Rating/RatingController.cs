using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjJJM.ViewModels;
using prjJJM.ViewModels.RatingVM;

namespace prjJJM.Controllers.Rating
{
    public class RatingController : Controller
    {
        // GET: Rating
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            var table = from p in new JJMdbEntities().tRating
                        select p;

            var keyw = from k in new JJMdbEntities().tRating
                       where k.r_send.Contains(keyword) || k.r_sendText.Contains(keyword)

                       select k;

            List<CRatingViewModel> list = new List<CRatingViewModel>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (tRating r in table)
                {
                    list.Add(new CRatingViewModel(r));
                }
            }
            else
            {
                foreach (tRating r in keyw)
                {
                    list.Add(new CRatingViewModel(r));
                }
            }

            return View(list);

        }

        public ActionResult Delete(int rid)
        {
            JJMdbEntities db = new JJMdbEntities();
            tRating rating = db.tRating.FirstOrDefault(r => r.ratingID == rid);

            tClass clas = db.tClass.FirstOrDefault(c => c.classID == rating.classID);

                if (rating != null)
                {
                    
                    db.tRating.Remove(rating);
                    clas.c_rateTotal -= 1;
                    db.SaveChanges();

                }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            tRating rating = (new JJMdbEntities()).tRating.FirstOrDefault(r => r.ratingID == id);
            var ratingList = new CRatingViewModel(rating);


            if (rating == null)
                return RedirectToAction("List");

            return View(ratingList);
        }
        [HttpPost]
        public ActionResult Edit(tRating modify)
        {
            JJMdbEntities db = new JJMdbEntities();
            tRating rating = db.tRating.FirstOrDefault(r => r.ratingID == modify.ratingID);

            if (rating != null)
            {
                rating.memberID = modify.memberID;
                rating.classID = modify.classID;
                rating.r_send = modify.r_send;
                rating.r_sendText = modify.r_sendText;
                rating.r_star = modify.r_star;
                //rating.r_sendTime = modify.r_sendTime;              

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tRating r)
        {
            var ratingList = new CRatingViewModel(r);

            JJMdbEntities db = new JJMdbEntities();
            r.r_sendTime = DateTime.Now;
            db.tRating.Add(r);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}