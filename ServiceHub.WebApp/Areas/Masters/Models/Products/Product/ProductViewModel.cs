using ServiceHub.Domain.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public ProductCreateModel CreateModel { get; set; }

        public IEnumerable<TblProduct> Products { get; set; }

        public FilterDataModel FilterDataModel { get; set; }
    }
}