using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Waito.Models;

namespace Waito.Controllers
{
    public class StockistController : Controller
    {
        //
        // GET: /Stockist/

        public ActionResult Index()
        {
            List<WaitoDistributor> distributors = new WaitoEntities().WaitoDistributors.ToList();
            return View(distributors);            
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Distributor distributor)
        {
            if (ModelState.IsValid)
            {
                WaitoDistributor distributor_db = new WaitoDistributor()
                {
                    Title = distributor.Title,
                    Description = distributor.Description,
                    Address = distributor.Address,
                    Phone = distributor.Phone
                };


                using (WaitoEntities entities = new WaitoEntities())
                {
                    entities.WaitoDistributors.Add(distributor_db);
                    entities.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            WaitoDistributor distributor_db = new WaitoEntities().WaitoDistributors.Where( d => d.DistributorId == id).FirstOrDefault();

            Distributor distributor = new Distributor()
            {
                DistributorId = distributor_db.DistributorId,
                Title = distributor_db.Title,
                Description = distributor_db.Description,
                Address = distributor_db.Address,
                Phone = distributor_db.Phone
            };

            return View(distributor);
        }

        [HttpPost]
        public ActionResult Edit(Distributor distributor)
        {
            if (ModelState.IsValid)
            {
                using (WaitoEntities entities = new WaitoEntities())
                {
                    WaitoDistributor distributor_db = entities.WaitoDistributors.Where(d => d.DistributorId == distributor.DistributorId).FirstOrDefault();

                    distributor_db.Title = distributor.Title;
                    distributor_db.Description = distributor.Description;
                    distributor_db.Address = distributor.Address;
                    distributor_db.Phone = distributor.Phone;

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
                WaitoDistributor distributor_db = entities.WaitoDistributors.Where(d => d.DistributorId == id).FirstOrDefault();

                entities.WaitoDistributors.Remove(distributor_db);
                entities.SaveChanges();
            }    

            return RedirectToAction("Index");
        }

    }
}
