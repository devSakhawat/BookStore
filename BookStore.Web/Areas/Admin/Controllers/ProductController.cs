using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Areas.Admin.Controllers
{
   [Area("Admin")]
   public class ProductController : Controller
   {
      private readonly IUnitOfWork context;
      private readonly IWebHostEnvironment _webHostEnvironment;

      public ProductController(IUnitOfWork _context, IWebHostEnvironment webHostEnvironment)
      {
         context = _context;
         _webHostEnvironment = webHostEnvironment;
      }

      // Get
      public IActionResult Index()
      {
         return View();
      }

      // Get
      public IActionResult Upsert(int? id)
      {
         ProductVM productVM = new()
         {
            Product = new(),
            CategoryList = context.Category.GetAll().Select(c => new SelectListItem
            {
               Text = c.Name,
               Value = c.Id.ToString()
            }),

            CoverTypeList = context.CoverType.GetAll().Select(ct => new SelectListItem
            {
               Text = ct.Name,
               Value = ct.Id.ToString()
            })
         };


         if (id == null || id == 0)
         {
            return View(productVM);
         }
         else
         {
            //update product
            productVM.Product = context.Product.GetFirstOrDefault(p => p.Id == id);
            return View(productVM);
         }
      }

      // POST
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Upsert(ProductVM model, IFormFile? file)
      {
         if (ModelState.IsValid)
         {
            string wwwRotePath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
               string fileName = Guid.NewGuid().ToString();
               var uploadFolder = Path.Combine(wwwRotePath, @"images\products");
               if (!Directory.Exists(uploadFolder))
               {
                  Directory.CreateDirectory(uploadFolder);
               }

               string extension = Path.GetExtension(file.FileName);

               if (model.Product.ImageUrl != null)
               {
                  var oldImagePath = Path.Combine(wwwRotePath, model.Product.ImageUrl.Trim('\\'));
                  if (System.IO.File.Exists(oldImagePath))
                  {
                     System.IO.File.Delete(oldImagePath);
                  }
               }

               using (var filestreams = new FileStream(Path.Combine(uploadFolder, fileName + extension), FileMode.Create))
               {
                  file.CopyTo(filestreams);
               }

               model.Product.ImageUrl = @"\images\products\" + fileName + extension;
            }

            if (model.Product.Id == 0)
            {
               context.Product.Add(model.Product);
            }
            else
            {
               context.Product.Update(model.Product);
            }
            context.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction(nameof(Index));
         }
         return View(model);
      }

      #region API CALLS
      [HttpGet]
      public IActionResult GetAll()
      {
         var productList = context.Product.GetAll(includeProperties: "Category,CoverType");
         return Json(new { data = productList });
      }

      [HttpGet]
      public IActionResult GetProduct(int? id)
      {
         var product = context.Product.GetFirstOrDefault(x => x.Id == id);
         if (product == null)
         {
            return Json(new { success = false, message = "Error while deleting" });
         }
         return Json(new { data = product });
      }


      [HttpDelete]
      public IActionResult Delete(int? id)
      {
         var product = context.Product.GetFirstOrDefault(u => u.Id == id);

         if (product == null)
         {
            return Json(new { success = false, message = "Error while deleting" });
         }

         var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.Trim('\\'));
         if (System.IO.File.Exists(oldImagePath))
         {
            System.IO.File.Delete(oldImagePath);
         }

         context.Product.Remove(product);
         context.Save();
         return Json(new { success = true, message = "Delete Successful" });
      }
      #endregion
   }
}