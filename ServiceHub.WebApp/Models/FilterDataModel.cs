using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class FilterDataModel
    {
        [Display(Name = "Search")]
        public string GenericSearch { get; set; }

        [Display(Name = "Search")]
        public string ModalSearch { get; set; }

        public int? Status { get; set; }

        public string Name { get; set; }

        public string Parent { get; set; }

        public string Assign { get; set; }

        public string Desgcode { get; set; }

        public string Desg { get; set; }

        [Display(Name = "On duty Type")]
        public string OndutyType { get; set; }

        [Display(Name = "Leave type")]
        public string LeaveType { get; set; }

        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Employee")]
        public string Empno { get; set; }

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        public string ActionName { get; set; }

        [Display(Name = "Employee type")]
        public string[] EmployeeTypeList { get; set; }

        [Display(Name = "Grade")]
        public string[] GradeList { get; set; }

        [Display(Name = "Eligible for SWP")]
        public string EligibleForSWP { get; set; }

        [Display(Name = "Laptop user")]
        public string LaptopUser { get; set; }

        [Display(Name = "Primary workspace")]
        public string[] PrimaryWorkspaceList { get; set; }

        [Display(Name = "Is active")]
        public int? IsActive { get; set; }

        [Display(Name = "Is active future")]
        public int? IsActiveFuture { get; set; }

        [Display(Name = "Desk assignment")]
        public string DeskAssigmentStatus { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Company ")]
        public string CompanyCode { get; set; }

        [Display(Name = "Vendor")]
        public string Vendor { get; set; }

        #region RapReporting

        [Display(Name = "Year month")]
        public string Yymm { get; set; }

        [Display(Name = "Cost code")]
        public string CostCode { get; set; }

        [Display(Name = "Project")]
        public string Projno { get; set; }

        [Display(Name = "Cost center")]
        public string CostCenter { get; set; }

        [Required]
        [Display(Name = "Year month")]
        public string Yyyymm { get; set; }

        [Display(Name = "Report for")]
        public string RepFor { get; set; }

        [Required]
        [Display(Name = "Year mode")]
        public string YearMode { get; set; }

        public string Keyid { get; set; }
        public string User { get; set; }

        [Required]
        [Display(Name = "Year")]
        public string Yyyy { get; set; }

        [Display(Name = "Report id")]
        public string Reportid { get; set; }

        public string Runmode { get; set; }
        public string Category { get; set; }

        [Display(Name = "Report type")]
        public string ReportType { get; set; }

        [Display(Name = "Simulation")]
        public string Simul { get; set; }

        public string ControllerName { get; set; }

        public string Dummy_CostCode { get; set; }
        public string Dummy_Projno { get; set; }

        [Display(Name = "Project")]
        public string ProjectNo { get; set; }

        [Display(Name = "Simulation")]
        public string Sim { get; set; }

        #endregion RapReporting

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
