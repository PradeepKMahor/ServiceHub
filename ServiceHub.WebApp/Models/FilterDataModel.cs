using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class FilterDataModel
    {
        [Display(Name = "Search")]
        public string GenericSearch { get; set; } = string.Empty;

        [Display(Name = "Search")]
        public string ModalSearch { get; set; } = string.Empty;

        public int? Status { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Parent { get; set; } = string.Empty;

        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Employee")]
        public string Empno { get; set; } = string.Empty;

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        public string ActionName { get; set; } = string.Empty;

        [Display(Name = "Employee type")]
        public string[]? EmployeeTypeList { get; set; }

        [Display(Name = "Grade")]
        public string[]? GradeList { get; set; }

        [Display(Name = "Eligible for SWP")]
        public string EligibleForSWP { get; set; } = string.Empty;

        [Display(Name = "Laptop user")]
        public string LaptopUser { get; set; } = string.Empty;

        [Display(Name = "Primary workspace")]
        public string[]? PrimaryWorkspaceList { get; set; }

        [Display(Name = "Is active")]
        public int? IsActive { get; set; }

        [Display(Name = "Is active future")]
        public int? IsActiveFuture { get; set; }

        [Display(Name = "Desk assignment")]
        public string DeskAssigmentStatus { get; set; } = string.Empty;

        [Display(Name = "Currency")]
        public string Currency { get; set; } = string.Empty;

        [Display(Name = "Company ")]
        public string CompanyCode { get; set; } = string.Empty;

        [Display(Name = "Vendor")]
        public string Vendor { get; set; } = string.Empty;

        public bool IsFiltered
        {
            get
            {
                if (StartDate != null ||
                     EndDate != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}