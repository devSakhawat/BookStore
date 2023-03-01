using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.Controllers
{
   [Area("Customer")]
   public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _context;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			IEnumerable<Product> products = _context.Product.GetAll(includeProperties : "Category,CoverType");

			return View(products);
		}

		public IActionResult Details(int productId)
		{
			ShoppingCart shoppingCart = new ShoppingCart
			{
				Count = 1,
				ProductId = productId,
				Product = _context.Product.GetFirstOrDefault(p => p.Id == productId, includeProperties: "Category,CoverType")
			};

			return View(shoppingCart);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Details(ShoppingCart model, int productId)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			model.ApplicationUserId = claim.Value;

			ShoppingCart cartFrobDb = _context.ShoppingCart.GetFirstOrDefault(s => s.ApplicationUserId == claim.Value && s.ProductId == model.ProductId);

			if (cartFrobDb == null)
			{
            _context.ShoppingCart.Add(model);
         }
			else
			{
				_context.ShoppingCart.IncrimentCount(cartFrobDb, model.Count);
			}
			_context.Save();

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}