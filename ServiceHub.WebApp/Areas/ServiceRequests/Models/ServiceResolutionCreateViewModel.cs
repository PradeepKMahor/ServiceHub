using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class ServiceResolutionCreateViewModel
    {
        //[Required]
        //[Display(Name = "UserID")]
        //public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "ETR")]
        public string ETR { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Root Cause")]
        public string RootCause { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Technician Remarks")]
        public string TechnicianRemarks { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Service Request No.")]
        public string ServiceRequestNo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Upload photo")]
        public string UploadPhoto { get; set; } = string.Empty;
    }
}