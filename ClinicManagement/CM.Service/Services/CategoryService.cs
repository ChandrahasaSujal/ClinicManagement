using CM.Data.Infrastructure;
using CM.Data.ViewModels.Medicine;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork)
        {
           
        }

        public bool AddCategory(CategoryViewModel category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ManufacturerViewModel> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
