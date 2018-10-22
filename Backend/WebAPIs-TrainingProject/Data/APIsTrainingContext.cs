using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIsTrainingProject.Data
{
    public class APIsTrainingContext : DbContext
    {
        public APIsTrainingContext(DbContextOptions<APIsTrainingContext> options)
            : base(options)
        {
        }

        public DbSet<Models.LoginModel> LoginTable { get; set; }
        public DbSet<Models.CategoryModel> CategoryTable { get; set; }
        public DbSet<Models.ProductModel> ProductTable { get; set; }

    }
}
