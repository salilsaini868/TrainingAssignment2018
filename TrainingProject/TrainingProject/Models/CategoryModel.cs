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
        public int CreatedBy
        { get; set; }
        public DateTime CreatedDate
        { get; set; }
        public int ModifiedBy
        { get; set; }
        public DateTime ModifiedDate
        { get; set; }
        public string ModifiedUser
        { get; set; }
        public string CreatedUser
        { get; set; }
    }
}