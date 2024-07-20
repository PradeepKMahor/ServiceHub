using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class ProductsController : BaseController
    {
        public IActionResult Index()
        {
            var productViewModel = new ProductViewModel();
            return View(productViewModel);
        }

        #region Product

        public IActionResult ProductIndex()
        {
            return View();
        }

        public IActionResult ProductCreate(ProductCreateModel productCreateModel)
        {
            return View();
        }

        public IActionResult ProductUpdate(ProductUpdateModel productUpdateModel)
        {
            return View();
        }

        #endregion Product

        #region Sub Product

        public IActionResult SubProductIndex()
        {
            return View();
        }

        #endregion Sub Product

        #region Root Cause Analysis

        public IActionResult RootCauseAnalysisIndex()
        {
            return View();
        }

        #endregion Root Cause Analysis
    }
}