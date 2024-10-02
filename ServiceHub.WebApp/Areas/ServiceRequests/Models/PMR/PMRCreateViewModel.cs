using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class PMRCreateViewModel
    {
        [Required]
        [Display(Name = "Serial No.")]
        public string SerialNo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product")]
        public string Product { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Frequency")]
        public string Frequency { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Priority")]
        public string Priority { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Request Date")]
        public DateTime? RequestDate { get; set; }

        [Required]
        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "RCA")]
        public string RCA { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Assigned Date")]
        public DateTime? AssignedDate { get; set; }

        [Required]
        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Required]
        [Display(Name = "Closure Date")]
        public DateTime? ClosureDate { get; set; }

        [Required]
        [Display(Name = "Resolution Time Taken")]
        public string ResolutionTimeTaken { get; set; } = string.Empty;

        [Required]
        [Display(Name = "SLA Status")]
        public string SLAStatus { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Pending Duration")]
        public string PendingDuration { get; set; } = string.Empty;

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