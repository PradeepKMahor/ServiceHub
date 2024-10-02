using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class CMRCreateViewModel
    {
        //[Required]
        //[Display(Name = "UserID")]
        //public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Serial No.")]
        public string SerialNo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product")]
        public string Product { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Service Cause")]
        public string ServiceCause { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Requested On")]
        public DateTime? RequestedDate { get; set; }

        [Required]
        [Display(Name = "Assigned Date")]
        public DateTime? AssignedDate { get; set; }

        [Required]
        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Required]
        [Display(Name = "Service Request No.")]
        public string ServiceRequestNo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Customer/Organization name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Other/User Comments")]
        public string OtherUserComments { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Technician Remarks")]
        public string TechnicianRemarks { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product SOP")]
        public string ProductSOP { get; set; } = string.Empty;

        [Required]
        [Display(Name = "ETR")]
        public string ETR { get; set; } = string.Empty;

        [Required]
        [Display(Name = "RCA")]
        public string RCA { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Parts")]
        public string Parts { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Priority")]
        public string Priority { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Assign To")]
        public string AssignTo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Requested By Contact")]
        public string RequestedByContact { get; set; } = string.Empty;

        [Display(Name = "Alternate Contact")]
        public string AlternateContact { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrUploadPhoto1 { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrUploadPhoto2 { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrUploadPhoto3 { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Upload photo")]
        public string SrUploadPhoto4 { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrResolutionUploadPhoto1 { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrResolutionUploadPhoto2 { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrResolutionUploadPhoto3 { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrResolutionUploadPhoto4 { get; set; } = string.Empty;
    }
}