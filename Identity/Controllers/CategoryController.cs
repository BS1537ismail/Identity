using Identity.Models;
using Identity.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize(Roles = "Admin, Manager, CEO")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var data = await unitOfWork.CategoryService.GetAllAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.CategoryService.AddAsync(category);
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var data = await unitOfWork.CategoryService.GetByIdAsync(x => x.Id == id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryService.UpdateAsync(category);
                return RedirectToAction("Index");
            }

            return View();
        }
      
        public async Task<IActionResult> Details(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var data = await unitOfWork.CategoryService.GetByIdAsync(x => x.Id == id);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var data = await unitOfWork.CategoryService.GetByIdAsync(x => x.Id == id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            
            unitOfWork.CategoryService.DeleteAsync(category);
            return RedirectToAction("Index");
        }


    }
}
