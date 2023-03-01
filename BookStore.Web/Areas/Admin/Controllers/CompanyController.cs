using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace BookStore.Web.Areas.Admin.Controllers
{
   [Area("Admin")]
   public class CompanyController : Controller
   {
      private readonly IUnitOfWork context;

      public CompanyController(IUnitOfWork context)
      {
         this.context = context;
      }

      // Get
      public IActionResult Index()
      {
         return View();
      }

      // Get
      public IActionResult Upsert(int? id)
      {
         Company company = new();


         if (id == null || id == 0)
         {
            return View(company);
         }
         else
         {
            //update product
            company = context.Company.GetFirstOrDefault(p => p.Id == id);
            return View(company);
         }
      }

      // POST
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Upsert(Company model)
      {
         if (ModelState.IsValid)
         {
            if (model.Id == 0)
            {
               context.Company.Add(model);
               TempData["success"] = "Company created successfully";
            }
            else
            {
               context.Company.Update(model);
            }
            context.Save();
            TempData["success"] = "Company updated successfully";
            return RedirectToAction(nameof(Index));
         }
         return View(model);
      }

      #region API CALLS
      [HttpGet]
      public IActionResult GetAll()
      {
         var companies = context.Company.GetAll();
         return Json(new { data = companies });
      }

      // POST
      [HttpDelete]
      public IActionResult Delete(int? id)
      {
         var company = context.Company.GetFirstOrDefault(u => u.Id == id);

         if (company == null)
         {
            return Json(new { success = false, message = "Error while deleting" });
         }

         context.Company.Remove(company);
         context.Save();
         return Json(new { success = true, message = "Delete Successful" });
      }
      #endregion
   }
}
