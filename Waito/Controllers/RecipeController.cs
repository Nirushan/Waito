using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Waito.Models;

namespace Waito.Controllers
{
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/

        public ActionResult Index(int lastId = 0, int firstId = 0)
        {
            List<Recipe> recipes = new List<Recipe>();
            List<WaitoRecipe> recipes_db;

            if (firstId > 0)
            {
                recipes_db = new WaitoEntities().WaitoRecipes.Where(r => r.RecipeID < firstId).OrderByDescending(r => r.RecipeID).Take(4).ToList();
                if (recipes_db == null || recipes_db.Count == 0)
                    recipes_db = new WaitoEntities().WaitoRecipes.OrderBy(r => r.RecipeID).Take(4).ToList();
                else
                    recipes_db = recipes_db.OrderBy(r => r.RecipeID).ToList();
            }

            else if (lastId > 0)
            {
                recipes_db = new WaitoEntities().WaitoRecipes.Where(r => r.RecipeID > lastId).OrderBy(r => r.RecipeID).Take(4).ToList();
                if (recipes_db == null || recipes_db.Count == 0)
                    recipes_db = new WaitoEntities().WaitoRecipes.OrderBy(r => r.RecipeID).Take(4).ToList();
            }

            else
            {
                recipes_db = new WaitoEntities().WaitoRecipes.OrderBy(r => r.RecipeID).Take(4).ToList();
            }

            foreach (var item in recipes_db)
            {
                recipes.Add(
                    new Recipe()
                    {
                        RecipeId = item.RecipeID,
                        Title = item.Title,
                        Serves = item.Serves,
                        Ingredient = item.Ingredients,
                        Description = item.Description,
                        MediumImage = item.MediumImage,
                        LargeImage = item.LargeImage
                    }
                );
            }

            return View(recipes);
            
        }


        public ActionResult Details(int id)
        {
            
            WaitoRecipe recipe_db = new WaitoEntities().WaitoRecipes.Where(r => r.RecipeID == id).FirstOrDefault();
            Recipe recipe = new Recipe()
            {
                RecipeId = recipe_db.RecipeID,
                Title = recipe_db.Title,
                Serves = recipe_db.Serves,
                Ingredient = recipe_db.Ingredients,
                Description = recipe_db.Description,
                MediumImage = recipe_db.MediumImage,
                LargeImage = recipe_db.LargeImage
            };
            return View(recipe);
        }

        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Recipe recipe, HttpPostedFileBase MediumImage, HttpPostedFileBase LargeImage)
        {
            if (ModelState.IsValid)
            {
                string mediumName = "", largeName = "";
                if (MediumImage != null)
                {
                    mediumName = Path.GetFileName(MediumImage.FileName);
                    string fileSavePath = "~/Images/Recipes/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), mediumName);
                    MediumImage.SaveAs(path);
                }

                if (LargeImage != null)
                {
                    largeName = Path.GetFileName(LargeImage.FileName);
                    string fileSavePath = "~/Images/Recipes/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), largeName);
                    LargeImage.SaveAs(path);
                }

                WaitoRecipe recipe_db = new WaitoRecipe()
                {
                    Title = recipe.Title,
                    Serves = recipe.Serves,
                    Ingredients = recipe.Ingredient,
                    Description = recipe.Description,

                    MediumImage = mediumName,
                    LargeImage = largeName,

                    CreatedOn = DateTime.Now.Date,
                    ModifiedOn = DateTime.Now.Date,
                    CreatedBy = "Admin"
                };

                using (WaitoEntities entities = new WaitoEntities())
                {            

                    entities.WaitoRecipes.Add(recipe_db);
                    entities.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            WaitoRecipe recipe_db = new WaitoEntities().WaitoRecipes.Where(r => r.RecipeID == id).FirstOrDefault();

            Recipe recipe = new Recipe()
            {
                RecipeId = recipe_db.RecipeID,
                Title = recipe_db.Title,
                Serves = recipe_db.Serves,
                Ingredient = recipe_db.Ingredients,
                Description = recipe_db.Description,

                MediumImage = recipe_db.MediumImage,
                LargeImage = recipe_db.LargeImage
                
            };

            return View(recipe);
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe, HttpPostedFileBase MediumImage, HttpPostedFileBase LargeImage)
        {
            if (ModelState.IsValid)
            {
                string mediumName = "", largeName = "";
                if (MediumImage != null)
                {
                    mediumName = Path.GetFileName(MediumImage.FileName);
                    string fileSavePath = "~/Images/Recipes/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), mediumName);
                    MediumImage.SaveAs(path);
                }

                if (LargeImage != null)
                {
                    largeName = Path.GetFileName(LargeImage.FileName);
                    string fileSavePath = "~/Images/Recipes/";
                    var path = Path.Combine(Server.MapPath(fileSavePath), largeName);
                    LargeImage.SaveAs(path);
                }

                using (WaitoEntities entities = new WaitoEntities())
                {
                    WaitoRecipe recipe_db = entities.WaitoRecipes.Where(r => r.RecipeID == recipe.RecipeId).FirstOrDefault();

                    recipe_db.Title = recipe.Title;
                    recipe_db.Serves = recipe.Serves;
                    recipe_db.Ingredients = recipe.Ingredient;
                    recipe_db.Description = recipe.Description;

                    recipe_db.MediumImage = mediumName;
                    recipe_db.LargeImage = largeName;
                    
                    recipe_db.ModifiedOn = DateTime.Now.Date;
                    recipe_db.ModifiedBy = "Admin";
                    
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
                WaitoRecipe recipe_db = entities.WaitoRecipes.Where(r => r.RecipeID == id).FirstOrDefault();
                entities.WaitoRecipes.Remove(recipe_db);
                entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
