using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork db)
        {
            unitOfWork = db;
        }
        public IActionResult Index()
        {
            var productsList = unitOfWork.Product.GetAll();
            return View(productsList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Add(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var product = unitOfWork.Product.Get(item => item.Id == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.Product.Update(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var product = unitOfWork.Product.Get(item => item.Id == id);
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var product = unitOfWork.Product.Get(item => item.Id == id);
            unitOfWork.Product.Remove(product);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
