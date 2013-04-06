using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Waito.Models;

namespace Waito.Controllers
{
    [Authorize]
    public class StandardPagesController : Controller
    {
        //
        // GET: /StandardPages/

        public ActionResult Index()
        {
            List<WaitoPage> pages_db = new WaitoEntities().WaitoPages.ToList();
            return View(pages_db);
        }

        public ActionResult Edit(int id)
        {
            WaitoPage pages_db = new WaitoEntities().WaitoPages.Where( p => p.PageId == id).FirstOrDefault();

            Page page = new Page()
            {
                PageId = pages_db.PageId,
                PageName = pages_db.PageName,
                PageContent = pages_db.PageContent
            };
            return View(page);
        }


        [HttpPost]
        public ActionResult Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                using (WaitoEntities entities = new WaitoEntities())
                {
                    WaitoPage pages_db = entities.WaitoPages.Where(p => p.PageId == page.PageId).FirstOrDefault();

                    pages_db.PageName = page.PageName;
                    pages_db.PageContent = page.PageContent;

                    entities.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View();
        }



        public ActionResult Create()
        {            
            return View();
        }


        [HttpPost]
        public ActionResult Create(Page page)
        {
            if (ModelState.IsValid)
            {
                using (WaitoEntities entities = new WaitoEntities())
                {
                    WaitoPage pages_db = new WaitoPage()
                    {
                        PageName = page.PageName,
                        PageContent = page.PageContent
                    };

                    entities.WaitoPages.Add(pages_db);
                    entities.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Delete(int id)
        {
            using (WaitoEntities entities = new WaitoEntities())
            {
                WaitoPage pages_db = entities.WaitoPages.Where(p => p.PageId == id).FirstOrDefault();

                entities.WaitoPages.Remove(pages_db);
                entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Page(int id)
        {            
            WaitoPage pages_db = new WaitoEntities().WaitoPages.Where(p => p.PageId == id).FirstOrDefault();

            return View(pages_db);
        }
    }
}
