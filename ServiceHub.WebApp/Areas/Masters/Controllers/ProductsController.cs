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

        public async Task<IActionResult> Index(int? id)
        {
            var productViewModel = new ProductViewModel();
            var data = (await _productRepository.GetAllAsync());

            productViewModel.Products = data;

            if (id is not null)
            {
                var result = await _productRepository.GetAsync(m => m.Id == id);

                productViewModel.CreateModel.ProductId = (int)id;
                productViewModel.CreateModel.ProductCode = result.ProductCode;
                productViewModel.CreateModel.ProductName = result.ProductName;
                productViewModel.CreateModel.ServiceDate = result.ServiceDate;
                productViewModel.CreateModel.WarrantyDate = result.WarrantyDate;
                productViewModel.CreateModel.ProductDescription = result.ProductDescription;
                productViewModel.CreateModel.UploadPhoto = result.UploadPhoto;

                if (result.Status == "Active")
                {
                    productViewModel.CreateModel.Status = true;
                }
                else
                {
                    productViewModel.CreateModel.Status = false;
                }
            }

            return View(productViewModel);
        }

        #region Product

        public IActionResult ProductIndex()
        {
            return View();
        }

        public async Task<IActionResult> ProductCreate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productRepository.GetAsync(m => m.Id == id);

            ProductCreateModel productCreateModel = new();

            if (result == null)
            {
                return View(productCreateModel);
            }

            productCreateModel.ProductId = result.Id;
            productCreateModel.ProductCode = result.ProductCode;
            productCreateModel.ProductName = result.ProductName;
            productCreateModel.ServiceDate = result.ServiceDate;
            productCreateModel.WarrantyDate = result.WarrantyDate;
            productCreateModel.ProductDescription = result.ProductDescription;
            productCreateModel.UploadPhoto = result.UploadPhoto;

            if (result.Status == "Active")
            {
                productCreateModel.Status = true;
            }
            else
            {
                productCreateModel.Status = false;
            }

            return View(productCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productRepository.GetAsync(m => m.Id == id);

            ProductCreateModel productCreateModel = new();

            return RedirectToAction("Index", new { id });
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

        public IActionResult ProductUpdate(int? id)
        {
            ProductCreateModel productCreateModel = new();
            return View(productCreateModel);
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