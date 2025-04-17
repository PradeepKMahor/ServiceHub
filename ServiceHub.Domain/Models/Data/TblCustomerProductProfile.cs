using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Models.Data
{
    public class TblCustomerProductProfile
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string Email1 { get; set; } = string.Empty;
        public string Email2 { get; set; } = string.Empty;
        public string Contact1 { get; set; } = string.Empty;
        public string Contact2 { get; set; } = string.Empty;
        public string CustomerGST { get; set; } = string.Empty;

        public DateTime? ValidFromDate { get; set; }

        public DateTime? ValidToDate { get; set; }

        public string CountryCode { get; set; } = string.Empty;

        public string? UploadLogo { get; set; } = string.Empty;
        public string? CustomerAddress { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}