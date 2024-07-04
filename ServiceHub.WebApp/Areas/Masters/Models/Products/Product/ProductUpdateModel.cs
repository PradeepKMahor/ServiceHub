using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class ProductUpdateModel
    {
        [Required]
        [Display(Name = "Product Id")]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Service Date")]
        public DateTime? ServiceDate { get; set; }

        [Required]
        [Display(Name = "Warranty Date")]
        public DateTime? WarrantyDate { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Upload Photo")]
        public string UploadPhoto { get; set; } = string.Empty;

        [Required]
        [Display(Name = "IsActive")]
        public string IsActive { get; set; } = string.Empty;
    }
}