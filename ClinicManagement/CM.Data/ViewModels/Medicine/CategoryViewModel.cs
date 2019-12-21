using System.ComponentModel.DataAnnotations;

namespace CM.Data.ViewModels.Medicine
{
    public class CategoryViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
