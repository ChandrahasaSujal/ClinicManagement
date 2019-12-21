using System.ComponentModel.DataAnnotations;

namespace CM.Data.ViewModels.Medicine
{
    public class ManufacturerViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
