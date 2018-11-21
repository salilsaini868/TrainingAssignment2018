using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIs.Data;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Authorize]
    [Route("api/category/[action]")]
    public class CategoryController : Controller
    {
        private WebApisContext context;
        private readonly ClaimsPrincipal principal;
        Helper helper;
        private readonly IMapper mapper;

        public CategoryController(WebApisContext APIcontext, IPrincipal _principal, IMapper _mapper)
        {
            context = APIcontext;
            principal = _principal as ClaimsPrincipal;
            mapper = _mapper;
            helper = new Helper(_principal);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Data of the selected category.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id != 0)
            {
                var detail = await context.CategoryTable.FirstOrDefaultAsync(x => x.CategoryID == id);
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

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertCategory(CategoryModel categories)
        {
            if (categories.CategoryID == 0)
            {
                var category = mapper.Map<CategoryModel>(categories);
                category.CreatedBy = helper.GetSpecificClaim("ID");
                category.CreatedDate = DateTime.Now;
                context.CategoryTable.Add(category);
                await context.SaveChangesAsync();
                localCategoryModel localmodel = new localCategoryModel()
                {
                    CreatedUser = helper.GetSpecificClaim("Name")
                };
            }
            return Ok(categories);
        }

        /// <summary>
        /// Edits the selected category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel categories)
        {
            var updateQuery = await context.CategoryTable.FirstOrDefaultAsync(x => x.CategoryID == id);
            updateQuery.CategoryName = categories.CategoryName;
            updateQuery.CategoryDescription = categories.CategoryDescription;
            updateQuery.IsActive = categories.IsActive;            
            updateQuery.ModifiedBy = helper.GetSpecificClaim("ID");
            updateQuery.ModifiedDate = DateTime.Now;
            await context.SaveChangesAsync();
            localCategoryModel localmodel = new localCategoryModel()
            {
                ModifiedUser = helper.GetSpecificClaim("Name")
            };
            return Ok(categories);
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
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
                            select new localCategoryModel
                            {
                                CategoryID = c.CategoryID,
                                CategoryName = c.CategoryName,
                                CategoryDescription = c.CategoryDescription,
                                IsActive = c.IsActive,
                                CreatedBy = c.CreatedBy,
                                CreatedDate = c.CreatedDate,
                                CreatedUser = p.Username,
                                ModifiedBy = c.ModifiedBy,
                                ModifiedDate = c.ModifiedDate,
                                ModifiedUser = p1.Username
                            };
            if (search != null)
            {
                listQuery = listQuery.Where(x => x.CategoryName.Contains(search) || x.CategoryDescription.Contains(search));
            }
            var list = await listQuery.ToListAsync();
            List<CategoryModel> viewList = new List<CategoryModel>();
            viewList = mapper.Map<List<CategoryModel>>(list);

            if (list.Count == 0)
            {
                return NotFound("No records present.");
            }
            return Ok(viewList);
        }

        /// <summary>
        /// Deletes category.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int ID)
        {
            var deleteQuery = await context.CategoryTable.FirstOrDefaultAsync(x => x.CategoryID == ID);
            context.CategoryTable.Remove(deleteQuery);
            await context.SaveChangesAsync();
            return Ok("Deleted successfully.");
        }
    }
}