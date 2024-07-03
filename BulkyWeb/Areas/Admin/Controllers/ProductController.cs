using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork db, IWebHostEnvironment webEnv)
        {
            unitOfWork = db;
            webHostEnvironment = webEnv;
        }
        public IActionResult Index()
        {
            var productsList = unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(productsList);
        } 
        // Update + insert 
        public IActionResult Upsert(int? id)
        {
            // This will retreive a list of Category Type 
            // Projection in EF Core
            // Getting all the category items from the database
            // Selecting each and coverting to SelectListItem type using .Select
            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category
                .GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                });
            ProductVM productVm = new()
            {
                CategoryListItems = CategoryList,
                Product = new Product()
            };
            // Create
            if(id == null || id == 0) return View(productVm);
            //update
            productVm.Product = unitOfWork.Product.Get(item => item.Id == id);
            return View(productVm);


        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    // The file might have some weird name
                    // Guid.NewGuid().ToString(); this will return a random name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    // checking if there is file inserted
                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        // delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    // save image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (obj.Product.Id == 0)
                {
                    unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    unitOfWork.Product.Update(obj.Product);
                }
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            obj.CategoryListItems = unitOfWork.Category.GetAll().Select(item => new SelectListItem
            {
                Text = item.CategoryName,
                Value = item.Id.ToString()
            });
            return View(obj);
        }

        //public IActionResult Edit(int? id, IFormFile? file)
        //{
        //    if (id == null || id == 0) return NotFound();
        //    var product = unitOfWork.Product.Get(item => item.Id == id);
        //    return View(product);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        unitOfWork.Product.Update(product);
        //        unitOfWork.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return NotFound();
        //}

        /*public IActionResult Delete(int? id)
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
        }*/

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });

        }

        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = unitOfWork.Product.Get(u => u.Id == id);
            if(productToBeDeleted == null)
            {
                return Json(
                    new { 
                        success = false, 
                        message = "Error while deleting" }
                    );
            }

            var oldImagePath =
                            Path.Combine(webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            unitOfWork.Product.Remove(productToBeDeleted);
            unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfullly" });
        }
        #endregion
    }
}
