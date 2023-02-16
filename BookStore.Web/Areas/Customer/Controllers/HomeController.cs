using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

		public IActionResult Details(int id)
		{
			ShoppingCart shoppingCart = new ShoppingCart
			{
				Count = 1,
				Product = _context.Product.GetFirstOrDefault(p => p.Id == id, includeProperties: "Category,CoverType")
      };

			return View(shoppingCart);
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