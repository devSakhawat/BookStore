using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BookStore.Web.Areas.Admin.Controllers
{
   public class CoverTypeController : Controller
   {
      private readonly IUnitOfWork context;

      public CoverTypeController(IUnitOfWork context)
      {
         this.context = context;
      }

      // Get
      public IActionResult Index()
      {
         IEnumerable<CoverType> coverTypes = context.CoverType.GetAll();
         return View(coverTypes);
      }

      // Get
      public IActionResult Create()
      {
         return View(new CoverType());
      }

      // Post
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Create(CoverType model)
      {
         if (ModelState.IsValid)
         {
            context.CoverType.Add(model);
            context.Save();
            return RedirectToAction("Index");
         }
         return View(model);
      }

      // Get
      public IActionResult Edit(int? id)
      {
         if (id == 0)
         {
            return NotFound();
         }
         CoverType coverType = context.CoverType.GetFirstOrDefault(ct => ct.Id == id);

         if (coverType == null)
            return NotFound();

         return View(coverType);
      }

      //Post
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Edit(CoverType model)
      {
         if (ModelState.IsValid)
         {
            context.CoverType.Update(model);
            context.Save();
            return RedirectToAction("Index");
         }
         return View(model);
      }

      // Get
      public IActionResult Delete(int? id)
      {
         if (id == 0)
         {
            return NotFound();
         }
         CoverType coverType = context.CoverType.GetFirstOrDefault(ct => ct.Id == id);

         if (coverType == null)
            return NotFound();

         return View(coverType);
      }

      //Post
      [HttpPost]
      [ActionName("Delete")]
      public IActionResult DeleteEntity(CoverType model)
      {
         if (model.Id == 0 || model == null)
            return NotFound();
         CoverType coverType = context.CoverType.GetFirstOrDefault(ct => ct.Id == model.Id);
         if (coverType == null)
            return NotFound();
         context.CoverType.Remove(coverType);
         context.Save();
         return RedirectToAction(nameof(Index));
      }
   }
}
