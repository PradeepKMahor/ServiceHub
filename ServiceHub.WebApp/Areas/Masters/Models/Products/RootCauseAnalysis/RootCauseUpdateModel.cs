using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class RootCauseUpdateModel
    {
        [Required]
        [Display(Name = "Root Cause Id")]
        public string RootCauseId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Root Cause Code")]
        public string RootCauseCode { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Root Cause")]
        public string RootCause { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "IsActive")]
        public string IsActive { get; set; } = string.Empty;
    }
}