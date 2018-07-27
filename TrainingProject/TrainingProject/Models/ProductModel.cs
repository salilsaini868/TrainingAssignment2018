using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace TrainingProject.Models
{
    public class ProductModel
    {
        public int Product_ID { get; set; }

        public string Product_name { get; set; }

        public int Price { get; set; }

        public int NoOfProducts { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }


    }
}