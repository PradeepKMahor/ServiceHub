using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class CMRViewModel
    {
        public CMRViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "SRN")]
        public string SRN { get; set; }

        [Display(Name = "Serial No.")]
        public string SerialNo { get; set; }

        [Display(Name = "Product")]
        public string Product { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Requested Date")]
        public DateTime? RequestedDate { get; set; }

        [Display(Name = "Requested By Contact")]
        public string RequestedByContact { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }

        [Display(Name = "Closure Date")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "Resolution Time Taken")]
        public string ResolutionTimeTaken { get; set; }

        [Display(Name = "SLA Status")]
        public string SLAStatus { get; set; }

        [Display(Name = "From Date")]
        public string SearchByFromDate { get; set; }

        [Display(Name = "To Date")]
        public string SearchByToDate { get; set; }

        [Display(Name = "Product")]
        public string SearchByProduct { get; set; }

        [Display(Name = "Customer")]
        public string SearchByCustomer { get; set; }

        [Display(Name = "Status")]
        public string SearchByStatus { get; set; }

        [Display(Name = "Priority")]
        public string SearchByPriority { get; set; }

        [Display(Name = "Assigned To")]
        public string SearchByAssignedTo { get; set; }

        [Display(Name = "SLA Status")]
        public string SearchBySLAStatus { get; set; }
    }
}