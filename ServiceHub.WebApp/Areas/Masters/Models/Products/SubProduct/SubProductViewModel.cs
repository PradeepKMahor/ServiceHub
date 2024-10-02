using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class SubProductViewModel
    {
        public SubProductViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "Product Id")]
        public string ProductId { get; set; } = string.Empty;

        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Service Date")]
        public DateTime? ServiceDate { get; set; }

        [Display(Name = "Warranty Date")]
        public DateTime? WarrantyDate { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Upload Photo")]
        public string UploadPhoto { get; set; }

        [Display(Name = "Status")]
        public string IsActive { get; set; }
    }
}