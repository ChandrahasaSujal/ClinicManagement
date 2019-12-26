using AutoMapper;
using CM.Data.Infrastructure;
using CM.Data.ViewModels.Medicine;
using CM.Model.Models.Medicine;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CM.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IEnumerable<Category> Categories;
        IEnumerable<CategoryViewModel> CategoriesViewModel;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool AddCategory(CategoryViewModel category)
        {
            Category categoryDb;
            try
            {
                if (category != null)
                {
                    category.Id = Guid.NewGuid();
                    categoryDb = new Category();
                    categoryDb = _mapper.Map(category, categoryDb);
                    _unitOfWork.CategoryRepository.Add(categoryDb);
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            try
            {
                Categories = new List<Category>();
                CategoriesViewModel = new List<CategoryViewModel>();
                Categories = _unitOfWork.CategoryRepository.Fetch(c => c.IsDeleted == false);
                if (Categories != null)
                {
                    CategoriesViewModel = _mapper.Map(Categories, CategoriesViewModel);
                    return CategoriesViewModel;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public List<SelectListItem> GetCategoriesForDropDownList()
        {
            List<SelectListItem> categoriesList = new List<SelectListItem>();
            try
            {
                IEnumerable<Category> categories = new List<Category>();
                categories = _unitOfWork.CategoryRepository.Fetch();
                foreach (var category in categories)
                {
                    categoriesList.Add(new SelectListItem()
                    {
                        Text = category.CategoryName,
                        Value = category.Id.ToString()
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return categoriesList;
        }
    }
}
