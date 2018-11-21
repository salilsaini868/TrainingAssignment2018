using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIs.Data;

namespace WebAPIs.Models.localModel
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryModel, localCategoryModel>();
            CreateMap<localCategoryModel, CategoryModel>();
        }
    }
}
