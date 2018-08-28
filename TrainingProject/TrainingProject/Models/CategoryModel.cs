using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingProject.Models;

namespace TrainingProject.Models
{
    public class CategoryModel : Training_ProductCategories
    {        
        public string ModifiedUser
        { get; set; }
        public string CreatedUser
        { get; set; }

    }
}