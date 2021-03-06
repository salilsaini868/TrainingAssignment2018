﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TrainingProject.Models
{
    public class ProductModel
    {
        public int Product_ID { get; set; }

        public string Product_name { get; set; }

        public string CategoryID { get; set; }

        public string Category { get; set; }

        public int Price { get; set; }

        public int NoOfProducts { get; set; }

        public DateTime VisibleDate { get; set; }
        
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedUser { get; set; }

        public string CreatedUser { get; set; }
    }
}