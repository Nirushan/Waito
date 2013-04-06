using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Waito.Models;

namespace Waito.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index(int lastId=0, int firstId = 0)
        {

            List<Product> products = new List<Product>();
            List<WaitoProduct> products_db;

            if (firstId > 0)
            {
                products_db = new WaitoEntities().WaitoProducts.Where(p => p.ProductID < firstId).OrderByDescending(p => p.ProductID).Take(4).ToList();
                if (products_db == null || products_db.Count == 0)
                    products_db = new WaitoEntities().WaitoProducts.OrderBy(p => p.ProductID).Take(4).ToList();
                else
                    products_db = products_db.OrderBy(p => p.ProductID).ToList();
            }

            else if (lastId > 0)
            {
                products_db = new WaitoEntities().WaitoProducts.Where(p => p.ProductID > lastId).OrderBy(p => p.ProductID).Take(4).ToList();
                if (products_db == null || products_db.Count == 0)
                    products_db = new WaitoEntities().WaitoProducts.OrderBy(p => p.ProductID).Take(4).ToList();
            }

            else
            {
                products_db = new WaitoEntities().WaitoProducts.OrderBy(p => p.ProductID).Take(4).ToList();
            }

            foreach (var item in products_db)
            {
                products.Add(
                    new Product()
                    {
                        ProductId = item.ProductID,
                        Title = item.Title,
                        ConsumerDetail = item.ConsumerDetails,
                        Description = item.Description,
                        IngredientList = item.IngredientList,
                        Cooking = item.Cooking,
                        MediumImage = item.MediumImage,
                        LargeImage = item.LargeImage
                    }
                );
            }

            return View(products);
        }


        public ActionResult Details(int id)
        {
            WaitoProduct product_db = new WaitoEntities().WaitoProducts.Where(p => p.ProductID == id).FirstOrDefault();

            Product product = new Product()
            {
                ProductId = product_db.ProductID,
                Title = product_db.Title,
                ConsumerDetail = product_db.ConsumerDetails,
                Description = product_db.Description,
                IngredientList = product_db.IngredientList,
                Cooking = product_db.Cooking,
                MediumImage = product_db.MediumImage,
                LargeImage = product_db.LargeImage
            };
            
            return View(product);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase MediumImage, HttpPostedFileBase LargeImage)
        {
            
            if (ModelState.IsValid)
            {
                string mediumName = "", largeName = "";
                if (MediumImage != null)
                {
                    mediumName = Path.GetFileName(MediumImage.FileName);
                    string fileSavePath = "~/Images/Products/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), mediumName);
                    MediumImage.SaveAs(path);
                }
                
                if (LargeImage != null)
                {
                    largeName = Path.GetFileName(LargeImage.FileName);
                    string fileSavePath = "~/Images/Products/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), largeName);
                    LargeImage.SaveAs(path);
                }

                WaitoProduct product_db = new WaitoProduct()
                {
                    
                    Title = product.Title,
                    ConsumerDetails = product.ConsumerDetail,
                    Description = product.Description,
                    IngredientList = product.IngredientList,
                    Cooking = product.Cooking,
                    

                    LargeImage = largeName,
                    MediumImage = mediumName,

                    CreatedOn = DateTime.Now.Date,
                    ModifiedOn = DateTime.Now.Date,
                    CreatedBy = "Admin"
                };

                using (WaitoEntities entities = new WaitoEntities())
                {
                    entities.WaitoProducts.Add(product_db);
                    entities.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            WaitoProduct product_db = new WaitoEntities().WaitoProducts.Where(p => p.ProductID == id).FirstOrDefault();

            Product product = new Product()
            {
                ProductId = product_db.ProductID,
                Title = product_db.Title,
                ConsumerDetail = product_db.ConsumerDetails,
                Description = product_db.Description,
                IngredientList = product_db.IngredientList,
                Cooking = product_db.Cooking,
                MediumImage = product_db.MediumImage,
                LargeImage = product_db.LargeImage
            };

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase MediumImage, HttpPostedFileBase LargeImage)
        {

            if (ModelState.IsValid)
            {
                string mediumName = "", largeName = "";
                if (MediumImage != null)
                {
                    mediumName = Path.GetFileName(MediumImage.FileName);
                    string fileSavePath = "~/Images/Products/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), mediumName);
                    MediumImage.SaveAs(path);
                }

                if (LargeImage != null)
                {
                    largeName = Path.GetFileName(LargeImage.FileName);
                    string fileSavePath = "~/Images/Products/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), largeName);
                    LargeImage.SaveAs(path);
                }

                
                using (WaitoEntities entities = new WaitoEntities())
                {
                    WaitoProduct product_db = entities.WaitoProducts.Where( p => p.ProductID == product.ProductId).FirstOrDefault();
                    
                    
                    product_db.Title = product.Title;
                    product_db.ConsumerDetails = product.ConsumerDetail;
                    product_db.Description = product.Description;
                    product_db.IngredientList = product.IngredientList;
                    product_db.Cooking = product.Cooking;

                    product_db.LargeImage = largeName;
                    product_db.MediumImage = mediumName;
                    
                    product_db.ModifiedOn = DateTime.Now.Date;
                    product_db.ModifiedBy = "Admin";

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
                WaitoProduct product_db = entities.WaitoProducts.Where(p => p.ProductID == id).FirstOrDefault();
                entities.WaitoProducts.Remove(product_db);
                entities.SaveChanges();
            }
            

            return RedirectToAction("Index");
        }

    }
}
