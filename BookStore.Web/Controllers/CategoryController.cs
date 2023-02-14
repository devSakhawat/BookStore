using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
   public class CategoryController : Controller
   {
      private readonly IUnitOfWork context;

      public CategoryController(IUnitOfWork _context)
      {
         context = _context;
      }

      //Get
      public IActionResult Index()
      {
         IEnumerable<Category> categories = context.Category.GetAll();
         return View(categories);
      }

      //Get
      public IActionResult Create()
      {
         return View(new Category());
      }

      //Post
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Create(Category model)
      {
         if (ModelState.IsValid)
         {
            context.Category.Add(model);
            context.Save();
            return RedirectToAction(nameof(Index));
         }
         return View(model);
      }

      //Get
      public IActionResult Edit(int? id)
      {
         if (id == 0 || id == null)
         {
            return NotFound();
         }

         var categoryFromDb = context.Category.GetFirstOrDefault(c => c.Id == id);

         if (categoryFromDb == null)
         {
            return NotFound();
         }

         return View(categoryFromDb);
      }

      //Post
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Edit(Category model)
      {
         if (ModelState.IsValid)
         {
            context.Category.Update(model);
            context.Save();
            return RedirectToAction(nameof(Index));
         }
         return View(model);
      }

      //Get
      public IActionResult Detail(int? id)
      {
         if (id == null || id == 0)
            return NotFound();

         var category = context.Category.GetFirstOrDefault(c => c.Id == id);

         if (category == null || category.Id == 0)
            return NotFound();

         return View(category);
      }

      //Get
      public IActionResult Delete(int? id)
      {
         if (id == null || id == 0)
            return NotFound();

         var category = context.Category.GetFirstOrDefault(c => c.Id == id);

         if (category == null || category.Id == 0)
            return NotFound();

         return View(category);
      }

      //Post
      [HttpPost]
      [ActionName("Delete")]
      public IActionResult DeleteCategory(Category model)
      {
         if (model == null)
            return View(model);

         Category category = context.Category.GetFirstOrDefault(c => c.Id == model.Id);

         if (category == null || category.Id == 0)
            return View(category);

         context.Category.Remove(category);
         context.Save();

         return RedirectToAction(nameof(Index));
      }
   }
}
