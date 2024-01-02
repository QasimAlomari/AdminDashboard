using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class CategoryModel : BaseEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryModelId
    {
        public int CategoryId { get; set; }
        //public int  Id { get; set; }
    }

    public class CategoryModelCreate
    {
        public string CategoryName { get; set; }
    }

   
}
