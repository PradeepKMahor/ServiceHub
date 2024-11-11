using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            var productCreateModel = new ProductCreateModel();
            var data = (await _productRepository.GetAllAsync());

            productViewModel.Products = data;

            if (id is not null)
            {
                var result = await _productRepository.GetAsync(m => m.Id == id);

                productCreateModel.ProductId = (int)id;
                productCreateModel.Id = (int)id;
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
            }
            productViewModel.CreateModel = productCreateModel;
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
        public async Task<IActionResult> ProductCreate(ProductCreateModel viewModel)
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
                    if (viewModel.Id != 0)
                    {
                        tblProduct = await _productRepository.GetAsync(m => m.Id == viewModel.Id);
                    }
                    if (files.Count > 0)
                    {
                        var upload = Path.Combine(webRootPath, strFilePath);
                        var extention = Path.GetExtension(files[0].FileName);
                        if (viewModel.Id != 0)
                        {
                            var extention_new = Path.GetExtension(files[0].FileName);
                            if (tblProduct.UploadPhoto is not null)
                            {
                                var imagePath = Path.Combine(webRootPath, tblProduct.UploadPhoto.TrimStart('\\'));

                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }

                            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention_new), FileMode.Create))
                            {
                                files[0].CopyTo(fileStream);
                            }
                            viewModel.UploadPhoto = strFolderPath + fileName + extention_new;
                        }
                        else
                        {
                            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                            {
                                files[0].CopyTo(fileStream);

                                viewModel.UploadPhoto = strFolderPath + fileName + extention;
                            }
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
                    if (viewModel.Id != null)
                    {
                        _ = _productRepository.UpdateAsync(tblProduct);
                        Notify("Success", "Data updated successfully", "toaster", notificationType: Models.NotificationType.success);

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _ = _productRepository.InsertAsync(tblProduct);
                    }

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