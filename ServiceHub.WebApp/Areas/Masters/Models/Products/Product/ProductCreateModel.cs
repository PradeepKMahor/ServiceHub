using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class ProductCreateModel
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

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

        [Display(Name = "Upload Photo")]
        public string UploadPhoto { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}