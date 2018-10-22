using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIsTrainingProject.Models
{
    public class localCategoryModel : CategoryModel
    {
        public string ModifiedUser
        { get; set; }
        public string CreatedUser
        { get; set; }
    }
}
