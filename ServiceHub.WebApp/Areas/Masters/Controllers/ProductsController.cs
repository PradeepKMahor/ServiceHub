using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHub.DataAccess.Interface.Core;
using ServiceHub.DataAccess.Models;
using ServiceHub.DataAccess.Repositories.Core;
using ServiceHub.Domain.Models.Data;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;

        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductsController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _WebHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var productViewModel = new ProductViewModel();
            var data = (await _productRepository.GetAllAsync());

            productViewModel.Products = data;

            return View(productViewModel);
        }

        #region Product

        public IActionResult ProductIndex()
        {
            return View();
        }

        public IActionResult ProductCreate()
        {
            ProductCreateModel productCreateModel = new();
            return View(productCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductCreate(ProductCreateModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strFilePath = @"Imgs\Product";
                    string strFolderPath = @"\Imgs\Product\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    string fileName = Guid.NewGuid().ToString();
                    TblProduct tblProduct = new();

                    if (files.Count > 0)
                    {
                        var upload = Path.Combine(webRootPath, strFilePath);
                        var extention = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);

                            viewModel.UploadPhoto = strFolderPath + fileName + extention;
                        }
                    }

                    if (viewModel.Status)
                    {
                        tblProduct.Status = "Active";
                    }
                    else
                    {
                        tblProduct.Status = "DeActive";
                    }
                    tblProduct.ProductCode = viewModel.ProductCode;
                    tblProduct.ProductName = viewModel.ProductName;
                    tblProduct.ServiceDate = viewModel.ServiceDate;
                    tblProduct.WarrantyDate = viewModel.WarrantyDate;
                    tblProduct.ProductDescription = viewModel.ProductDescription;
                    tblProduct.UploadPhoto = viewModel.UploadPhoto;

                    var t = _productRepository.InsertAsync(tblProduct);
                    Notify("Success", "Data saved successfully", "toaster", notificationType: Models.NotificationType.success);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: Models.NotificationType.error);
            }

            return RedirectToAction(nameof(Index));
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