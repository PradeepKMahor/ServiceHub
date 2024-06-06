using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class BaseModel
    {
        public Guid f_uid { get; set; }

        [Display(Name = "ID")]
        public int f_iid { get; set; }

        [Display(Name = "Create Date")]
        public DateTime f_create_date { get; set; }

        [Display(Name = "Update Date")]
        public DateTime f_update_date { get; set; }

        [Display(Name = "Delete Date")]
        public DateTime f_delete_date { get; set; }
    }
}