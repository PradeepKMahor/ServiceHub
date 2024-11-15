namespace ServiceHub.Domain.Models.Data
{
    public class TblCRM
    {
        public int Id { get; set; }

        public string SerialNo { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public string ServiceCause { get; set; } = string.Empty;
        public DateTime? RequestedDate { get; set; }
        public DateTime? AssignedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
        public string ServiceRequestNo { get; set; } = string.Empty;
        public string RequestedBy { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;

        public string CustomerAddress { get; set; } = string.Empty;

        public string OtherUserComments { get; set; } = string.Empty;
        public string TechnicianRemarks { get; set; } = string.Empty;
        public string ProductSOP { get; set; } = string.Empty;
        public string ETR { get; set; } = string.Empty;
        public string RCA { get; set; } = string.Empty;
        public string Parts { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string AssignTo { get; set; } = string.Empty;
        public string RequestedByContact { get; set; } = string.Empty;
        public string AlternateContact { get; set; } = string.Empty;
        public string SrUploadPhoto1 { get; set; } = string.Empty;
        public string SrUploadPhoto2 { get; set; } = string.Empty;
        public string SrUploadPhoto3 { get; set; } = string.Empty;
        public string SrUploadPhoto4 { get; set; } = string.Empty;
        public string SrResolutionUploadPhoto1 { get; set; } = string.Empty;
        public string SrResolutionUploadPhoto2 { get; set; } = string.Empty;
        public string SrResolutionUploadPhoto3 { get; set; } = string.Empty;
        public string SrResolutionUploadPhoto4 { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}