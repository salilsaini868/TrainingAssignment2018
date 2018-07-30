using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingProject.Models
{
    public class CategoryModel
    {
        public int CategoryID
        { get; set; }
        public string CategoryName
        { get; set; }
        public string CategoryDescription
        { get; set; }
        public bool IsActive
        { get; set; }
    }
}