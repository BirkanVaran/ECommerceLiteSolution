using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Repository;
using ECommerceLiteEntity.Models;

namespace ECommerceLiteUI.Controllers
{
    public class CategoryController : Controller
    {
        // GLOBAL ALAN
        CategoryRepo myCategoryRepo = new CategoryRepo();
        #region CategoryList
        public ActionResult CategoryList()
        {
            var allCategories = myCategoryRepo.Queryable().Where(x => x.BaseCategoryId == null).ToList();
            ViewBag.CategoryCount = allCategories.Count;
            return View(allCategories);
        }
        #endregion

        public ActionResult SubCategoryList(int id)
        {
            var subCategories = myCategoryRepo.Queryable().Where(x => x.BaseCategoryId != null && x.BaseCategoryId == id).ToList();
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = myCategoryRepo.GetById(id).CategoryName;
            ViewBag.SubCategoryCount = subCategories.Count;
            return View(subCategories);
        }

        #region Create - HttpGet | HttpPost

        public ActionResult Create(int? id)
        {
            if (id!=null)
            {
                Category model = new Category()
                {
                    Id = id.Value
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Veri girişi hatalı.");
                }
                Category newCategory = new Category()
                {
                    CategoryName = model.CategoryName,
                    CategoryDescription = model.CategoryDescription,
                    RegisterDate = DateTime.Now,
                    BaseCategoryId=null
                };

                if (model.Id>0)
                {
                    newCategory.BaseCategoryId = model.Id;
                }
                int insertResult = myCategoryRepo.Insert(newCategory);
                if (insertResult > 0 && model.Id==0)
                {
                    return RedirectToAction("CategoryList", "Category");
                }
                else if (insertResult > 0 && model.Id > 0)
                {
                    return RedirectToAction("SubCategoryList", "Category");

                }
                else
                {
                    throw new Exception("Kategori eklerken hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu. Tekrar deneyiniz. HATA: " + ex.Message);
                // TODO: ez loglanacak
                return View(model);
            }
        }
        #endregion
    }
}