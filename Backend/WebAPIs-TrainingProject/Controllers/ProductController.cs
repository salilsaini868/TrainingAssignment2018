using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPIsTrainingProject.Data;
using WebAPIsTrainingProject.Models;

namespace WebAPIsTrainingProject.Controllers
{
    //[Produces("application/json")]
    [Authorize]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private APIsTrainingContext context;
        private readonly ClaimsPrincipal principal;
        public ProductController(APIsTrainingContext _context, IPrincipal _principal)
        {
            context = _context;
            principal = _principal as ClaimsPrincipal;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int? id)
        {
            if (id != 0)
            {
                ProductModel edit = new ProductModel();
                var product = await context.ProductTable.Where(x => x.ProductID == id).FirstOrDefaultAsync();
                if (product != null)
                {
                    return Ok(product);                    
                }
                else
                {
                    return BadRequest("Product does not exist.");
                }
            }
            return Ok();
        }

        private dynamic GetSpecificClaim(string type)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;
            var userName = claimsIdentity.FindFirst(ClaimTypes.GivenName).ToString();
            if (type == "ID")
            {
                return Convert.ToInt32(userId);
            }
            else
            {
                return Convert.ToString(userName);
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertProduct(ProductModel prop)
        {
            //var str = HttpContext.Session.GetString("User");
            //var userlogin = JsonConvert.DeserializeObject<LoginModel>(str);

            if (prop.ProductID == 0)
            {
                ProductModel products = new ProductModel()
                {
                    ProductName = prop.ProductName,
                    Price = prop.Price,
                    Quantity = prop.Quantity,
                    CategoryID = prop.CategoryID,
                    VisibleTill = prop.VisibleTill,
                    ProductDescription = prop.ProductDescription,
                    IsActive = prop.IsActive,
                    CreatedBy = GetSpecificClaim("ID"),
                    CreatedDate = DateTime.Now
                };
                context.ProductTable.Add(products);
                await context.SaveChangesAsync();
                localProductModel localmodel = new localProductModel()
                {
                    CreatedUser = GetSpecificClaim("Name")
                };                
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel prop)
        {
            //var str = HttpContext.Session.GetString("User");
            //var userlogin = JsonConvert.DeserializeObject<LoginModel>(str);
            var EditQuery = context.ProductTable.Where(x => x.ProductID == id).FirstOrDefault();
            EditQuery.ProductName = prop.ProductName;
            EditQuery.CategoryID = prop.CategoryID;
            EditQuery.Price = prop.Price;
            EditQuery.Quantity = prop.Quantity;
            EditQuery.VisibleTill = prop.VisibleTill;
            EditQuery.ProductDescription = prop.ProductDescription;
            EditQuery.IsActive = prop.IsActive;
            EditQuery.ModifiedBy = Convert.ToInt32(GetSpecificClaim("ID"));
            EditQuery.ModifiedDate = DateTime.Now;
            await context.SaveChangesAsync();
            localProductModel localmodel = new localProductModel()
            {
                ModifiedUser = Convert.ToString(GetSpecificClaim("Name"))
            };           
            return Ok(prop);
        }

        [HttpGet]
        public async Task<IActionResult> Listing(string search)
        {

            var listQuery = from c in context.ProductTable
                            join createdUser in context.LoginTable
                            on c.CreatedBy equals createdUser.UserID
                            into createname
                            from p in createname.DefaultIfEmpty()
                            join modifiedUser in context.LoginTable
                            on c.ModifiedBy equals modifiedUser.UserID
                            into modifyname
                            from p1 in modifyname.DefaultIfEmpty()
                            join categoryname in context.CategoryTable
                            on c.CategoryID equals categoryname.CategoryID
                            into namecategory
                            from cn in namecategory.DefaultIfEmpty()
                            select new { product = c, createdUser = p.Username, modifiedBy = p1.Username, category = cn.CategoryName };

            if (search != null)
            {
                listQuery = listQuery.Where(x => x.product.ProductName.Contains(search) || x.product.ProductDescription.Contains(search));
            }
            var Productlist = await listQuery.ToListAsync();

            List<ProductModel> viewList = new List<ProductModel>();
            foreach (var item in Productlist)
            {
                localProductModel model = new localProductModel();
                model.ProductID = item.product.ProductID;
                model.ProductName = item.product.ProductName;
                model.CategoryID = item.product.CategoryID;
                model.Category = item.category;
                model.Price = item.product.Price;
                model.Quantity = item.product.Quantity;
                model.VisibleTill = item.product.VisibleTill;
                model.ProductDescription = item.product.ProductDescription;
                model.IsActive = item.product.IsActive;
                model.CreatedBy = item.product.CreatedBy;
                model.CreatedUser = item.createdUser;
                model.CreatedDate = item.product.CreatedDate;
                model.ModifiedUser = item.modifiedBy;
                viewList.Add(model);
            }
            if (Productlist.Count == 0)
            {
                return NotFound("No records found.");
            }

            return Ok(viewList);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int ID)
        {
            var deleteQuery = await context.ProductTable.Where(x => x.ProductID == ID).ToListAsync();
            foreach (var item in deleteQuery)
            {
                context.ProductTable.Remove(item);
            }
            context.SaveChanges();
            return Ok("Deleted successfully.");
        }
    }
}