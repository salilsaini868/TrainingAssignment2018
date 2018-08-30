using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using TrainingProject.Models;

namespace TrainingProject.Controllers
{
    [AuthorizationFilter]
    public class CategoryController : Controller
    {
        // GET: Category               

        RHPMEntities entities = new RHPMEntities();

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id != null)
            {
                var detail = entities.Training_ProductCategories.Where(x => x.CategoryID == id).FirstOrDefault();
                CategoryModel detailModel = new CategoryModel();
                if (detail != null)
                {
                    detailModel.CategoryID = detail.CategoryID;
                    detailModel.CategoryName = detail.CategoryName;
                    detailModel.CategoryDescription = detail.CategoryDescription;
                    detailModel.IsActive = detail.IsActive;
                    detailModel.CreatedBy = detail.CreatedBy;
                    detailModel.CreatedDate = detail.CreatedDate;
                }
                else
                {
                    TempData["falseID"] = "Category not found.";
                }
                TempData["categoryid"] = detailModel.CategoryID;
                return View("InsertCategory", detailModel);
            }
            return View("InsertCategory");
        }

        [HttpPost]
        public ActionResult InsertCategory(CategoryModel categories)
        {
            var userlogin = Session["user"] as LoginModel;
            if (categories.CategoryID == 0)
            {
                Training_ProductCategories category = new Training_ProductCategories()
                {
                    CategoryName = categories.CategoryName,
                    CategoryDescription = categories.CategoryDescription,
                    IsActive = categories.IsActive,
                    CreatedBy = userlogin.UserID,
                    CreatedDate = DateTime.Now
                };
                entities.Training_ProductCategories.Add(category);
                entities.SaveChanges();
                categories.CreatedUser = userlogin.Username;
                TempData["Message_CategoryInsert"] = "category added.";
                return RedirectToAction("Detail");
            }
            else
            {
                var updateQuery = entities.Training_ProductCategories.Where(x => x.CategoryID == categories.CategoryID).FirstOrDefault();
                updateQuery.CategoryName = categories.CategoryName;
                updateQuery.CategoryDescription = categories.CategoryDescription;
                updateQuery.IsActive = categories.IsActive;
                updateQuery.ModifiedBy = userlogin.UserID;
                updateQuery.ModifiedDate = DateTime.Now;
                entities.SaveChanges();
                categories.ModifiedUser = userlogin.Username;
                TempData["Message_CategoryUpdate"] = "category updated.";
                return RedirectToAction("Detail", new { id = categories.CategoryID });
            }
        }

        public ActionResult Listing(FormCollection coll)
        {
            string searchView = coll["txtSearch"];
            ViewBag.searchQuery = searchView;
            var listQuery = from c in entities.Training_ProductCategories
                            join createdUser in entities.Training_LoginTable
                            on c.CreatedBy equals createdUser.UserID
                            into createname
                            from p in createname.DefaultIfEmpty()
                            join modifiedUser in entities.Training_LoginTable
                            on c.ModifiedBy equals modifiedUser.UserID
                            into modifyname
                            from p1 in modifyname.DefaultIfEmpty()
                            select new { category = c, createdUser = p.Username, modifiedBy = p1.Username };
            if (searchView != null)
            {
                listQuery = listQuery.Where(x => x.category.CategoryName.Contains(searchView) || x.category.CategoryDescription.Contains(searchView));
            }
            var list = listQuery.ToList();
            List<CategoryModel> viewList = new List<CategoryModel>();
            foreach (var item in list)
            {
                CategoryModel model = new CategoryModel();
                model.CategoryID = item.category.CategoryID;
                model.CategoryName = item.category.CategoryName;
                model.CategoryDescription = item.category.CategoryDescription;
                model.IsActive = item.category.IsActive;
                model.CreatedBy = item.category.CreatedBy;
                model.CreatedUser = item.createdUser;
                model.CreatedDate = item.category.CreatedDate;
                model.ModifiedBy = item.category.ModifiedBy;
                model.ModifiedDate = item.category.ModifiedDate;
                model.ModifiedUser = item.modifiedBy;
                viewList.Add(model);
            }
            if (list.Count == 0)
            {
                TempData["nodata"] = "No records found.";
            }
            return View("ListCategory", viewList);
        }
        public ActionResult Delete(int ID)
        {
            var deleteQuery = entities.Training_ProductCategories.Where(x => x.CategoryID == ID).ToList();
            foreach (var item in deleteQuery)
            {
                entities.Training_ProductCategories.Remove(item);
            }
            entities.SaveChanges();
            return RedirectToAction("Listing");
        }
    }
}