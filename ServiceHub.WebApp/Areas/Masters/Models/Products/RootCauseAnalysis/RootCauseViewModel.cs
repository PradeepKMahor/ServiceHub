using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class RootCauseViewModel
    {
        public RootCauseViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "Root Cause Id")]
        public string RootCauseId { get; set; } = string.Empty;

        [Display(Name = "Root Cause Code")]
        public string RootCauseCode { get; set; }

        [Display(Name = "Root Cause")]
        public string RootCause { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; }

        public string ProductId { get; set; }

        [Display(Name = "IsActive")]
        public string IsActive { get; set; }
    }
}