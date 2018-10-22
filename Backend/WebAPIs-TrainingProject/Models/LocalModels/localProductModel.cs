using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIsTrainingProject.Models
{
    public class localProductModel : ProductModel
    {
        public string Category { get; set; }
        public DateTime VisibleDate { get; set; }
        public string ModifiedUser { get; set; }
        public string CreatedUser { get; set; }
    }
}
