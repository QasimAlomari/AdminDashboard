using Domain.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class ProductModel : BaseEntity
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }
    }

    public class ProductModelId
    {
        public int ProductId { get; set; }
    }

    public class ProductModelCreate
    {
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductModelUpdate
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int CategoryId { get; set; }
    }

}
