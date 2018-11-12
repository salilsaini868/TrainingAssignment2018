using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.Data
{
    public class WebApisContext : DbContext
    {
        public WebApisContext(DbContextOptions<WebApisContext> options)
            : base(options)
        {
        }
        public DbSet<Models.LoginModel> LoginTable { get; set; }

        public DbSet<Models.ProductModel> ProductTable { get; set; }

        public DbSet<Models.CategoryModel> CategoryTable { get; set; }
    }
}
