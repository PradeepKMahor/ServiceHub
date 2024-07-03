using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class ProductsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Product

        public IActionResult ProductIndex()
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