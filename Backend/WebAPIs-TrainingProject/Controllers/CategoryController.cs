using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIsTrainingProject.Data;
using WebAPIsTrainingProject.Models;

namespace WebAPIsTrainingProject.Controllers
{
    //[Produces("application/json")]
    [Authorize]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private APIsTrainingContext context;
        private readonly ClaimsPrincipal principal;
        public CategoryController(APIsTrainingContext _context, IPrincipal _principal)
        {
            context = _context;
            principal = _principal as ClaimsPrincipal;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id != 0)
            {
                var detail = await context.CategoryTable.Where(x => x.CategoryID == id).FirstOrDefaultAsync();
                if (detail != null)
                {
                    return Ok(detail);
                }
                else
                {
                    return NotFound("Category does not exist.");
                }
            }
            return Ok();
        }

        private dynamic GetSpecificClaim(string type)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;
            var userName = claimsIdentity.FindFirst(ClaimTypes.GivenName).Value;
            if(type == "ID")
            {
                return Convert.ToInt32(userId);
            }
            else
            {
                return Convert.ToString(userName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertCategory(CategoryModel categories)
        {
            //var str = HttpContext.Session.GetString("User");
            //var userlogin = JsonConvert.DeserializeObject<LoginModel>(str);
            
            if (categories.CategoryID == 0)
            {
                CategoryModel category = new CategoryModel();
                category.CategoryName = categories.CategoryName;
                category.CategoryDescription = categories.CategoryDescription;
                category.IsActive = categories.IsActive;
                category.CreatedBy = GetSpecificClaim("ID");
                category.CreatedDate = DateTime.Now;
                context.CategoryTable.Add(category);
                await context.SaveChangesAsync();
                localCategoryModel localmodel = new localCategoryModel()
                {
                    CreatedUser = GetSpecificClaim("Name")
                };
            }
            return Ok(categories);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel categories)
        {
            //var str = HttpContext.Session.GetString("User");
            //var userlogin = JsonConvert.DeserializeObject<LoginModel>(str);            

            var updateQuery = await context.CategoryTable.Where(x => x.CategoryID == id).FirstOrDefaultAsync();
            updateQuery.CategoryName = categories.CategoryName;
            updateQuery.CategoryDescription = categories.CategoryDescription;
            updateQuery.IsActive = categories.IsActive;
            updateQuery.ModifiedBy = Convert.ToInt32(GetSpecificClaim("ID").Value);
            updateQuery.ModifiedDate = DateTime.Now;
            await context.SaveChangesAsync();
            localCategoryModel localmodel = new localCategoryModel()
            {
                ModifiedUser = Convert.ToString(GetSpecificClaim("Name").Value)
        };
            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Listing(string search)
        {
            var listQuery = from c in context.CategoryTable
                            join createdUser in context.LoginTable
                            on c.CreatedBy equals createdUser.UserID
                            into createname
                            from p in createname.DefaultIfEmpty()
                            join modifiedUser in context.LoginTable
                            on c.ModifiedBy equals modifiedUser.UserID
                            into modifyname
                            from p1 in modifyname.DefaultIfEmpty()
                            select new { category = c, createdUser = p.Username, modifiedBy = p1.Username };
            if (search != null)
            {
                listQuery = listQuery.Where(x => x.category.CategoryName.Contains(search) || x.category.CategoryDescription.Contains(search));
            }
            var list = await listQuery.ToListAsync();
            List<CategoryModel> viewList = new List<CategoryModel>();
            foreach (var item in list)
            {
                localCategoryModel model = new localCategoryModel();
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
                return NotFound("No records present.");
            }
            return Ok(viewList);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int ID)
        {
            var deleteQuery = await context.CategoryTable.Where(x => x.CategoryID == ID).ToListAsync();
            foreach (var item in deleteQuery)
            {
                context.CategoryTable.Remove(item);
            }
            await context.SaveChangesAsync();
            return Ok("Deleted successfully.");
        }
    }
}