using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Areas.Admin.Controllers
{
   public class ProductController : Controller
   {
      private readonly IUnitOfWork context;
      private readonly IWebHostEnvironment webHostEnvironment;

      public ProductController(IUnitOfWork _context, IWebHostEnvironment _webHostEnvironment)
      {
         context = _context;
         webHostEnvironment = _webHostEnvironment;
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
            //create product
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CoverTypeList"] = CoverTypeList;

				return View(productVM);
         }
         else
         {
            //update product
            productVM.Product = context.Product.GetFirstOrDefault(p => p.Id == id);
            return View(productVM);
         }
      }
   }
}