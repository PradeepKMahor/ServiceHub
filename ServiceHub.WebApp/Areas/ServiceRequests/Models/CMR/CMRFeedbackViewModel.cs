using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class CMRFeedbackViewModel
    {
        [Required]
        [Display(Name = "Serial No.")]
        public string SerialNo { get; set; } = string.Empty;

        [Display(Name = "Product")]
        public string Product { get; set; } = string.Empty;

        [Display(Name = "Service Cause")]
        public string ServiceCause { get; set; } = string.Empty;

        [Display(Name = "Request Date")]
        public DateTime? RequestDate { get; set; }

        [Display(Name = "Assigned Date")]
        public DateTime? AssignedDate { get; set; }

        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Display(Name = "Service Request No.")]
        public string ServiceRequestNo { get; set; } = string.Empty;

        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; } = string.Empty;

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Feedback")]
        public string Feedback { get; set; } = string.Empty;
    }
}