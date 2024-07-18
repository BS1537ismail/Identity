using Identity.Models;
using Identity.Service;
using Identity.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.Controllers
{
    [Authorize(Roles = "Admin, Manager, CEO")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var data = await unitOfWork.ProductService.GetAllAsync(inclideProperties: "Category");
            data = data.OrderBy(x => x.Price);

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await unitOfWork.CategoryService.GetAllAsync();
            IEnumerable<SelectListItem> CategoryList = data.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.ProductService.AddAsync(product);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var data = await unitOfWork.CategoryService.GetAllAsync();
            
            IEnumerable<SelectListItem> CategoryList = data.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            var item = await unitOfWork.ProductService.GetByIdAsync(x => x.Id == id, inclideProperties: "Category");
            if(item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ProductService.UpdateAsync(product);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
         
            var data = await unitOfWork.ProductService.GetByIdAsync(x => x.Id == id, inclideProperties: "Category");
            if(data == null)
            {
                return NotFound();
            }

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Details(Product product)
        {
            if (!ModelState.IsValid)
            {
               
                return View(product);
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.ProductService.GetByIdAsync(x => x.Id == id, inclideProperties: "Category");
            if(data == null)
            {
                return BadRequest();
            }

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
                unitOfWork.ProductService.DeleteAsync(product);

                return RedirectToAction("Index");
        }
    }
}
