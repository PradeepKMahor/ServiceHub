using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Interfaces;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> CategoriesIndex()
        {
            CategoriesModelViewModel categoriesModelViewModel = new CategoriesModelViewModel();
            var catigories = await unitOfWork.Categories.Get();

            return View(categoriesModelViewModel);
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriesModel>> GetCatigories()
        {
            var catigories = await unitOfWork.Categories.Get();
            return catigories;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var category = await unitOfWork.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoriesModel model)
        {
            await unitOfWork.Categories.Add(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(CategoriesModel model)
        {
            if (model == null || model.f_uid == Guid.Empty)
            {
                return BadRequest();
            }

            var category = await unitOfWork.Categories.Find(model.f_uid);

            if (category == null)
            {
                return NotFound();
            }

            await unitOfWork.Categories.Update(category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var category = await unitOfWork.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            await unitOfWork.Categories.Remove(category);
            return Ok();
        }
    }
}