using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class PMRViewModel
    {
        public PMRViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "SRN")]
        public string SRN { get; set; }

        [Display(Name = "Serial No.")]
        public string SerialNo { get; set; } = string.Empty;

        [Display(Name = "Product")]
        public string Product { get; set; } = string.Empty;

        [Display(Name = "Frequency")]
        public string Frequency { get; set; } = string.Empty;

        [Display(Name = "Priority")]
        public string Priority { get; set; } = string.Empty;

        [Display(Name = "Request Date")]
        public DateTime? RequestDate { get; set; }

        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; } = string.Empty;

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; } = string.Empty;

        [Display(Name = "RCA")]
        public string RCA { get; set; } = string.Empty;

        [Display(Name = "Assigned Date")]
        public DateTime? AssignedDate { get; set; }

        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Display(Name = "Closure Date")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "Resolution Time Taken")]
        public string ResolutionTimeTaken { get; set; } = string.Empty;

        [Display(Name = "SLA Status")]
        public string SLAStatus { get; set; } = string.Empty;

        [Display(Name = "Pending Duration")]
        public string PendingDuration { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Upload photo")]
        public string SrUploadPhoto1 { get; set; } = string.Empty;
    }
}