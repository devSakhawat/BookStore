using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModel;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.Controllers
{
   [Area("Customer")]
   [Authorize]
   public class CartController : Controller
   {
      private readonly IUnitOfWork _context;
      public ShoppingCardVM ShoppingCardVM { get; set; }

      public CartController(IUnitOfWork context)
      {
         _context = context;
      }

      public IActionResult Index()
      {
         var claimIdentity = (ClaimsIdentity)User.Identity;
         var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

         ShoppingCardVM = new ShoppingCardVM()
         {
            CartList = _context.ShoppingCart.GetAll(s => s.ApplicationUserId == claim.Value, includeProperties: "Product"),
            OrderHeader = new()
         };

         foreach (var cart in ShoppingCardVM.CartList)
         {
            cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
            ShoppingCardVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
         }

         return View(ShoppingCardVM);
      }

      // Get
      public IActionResult Summary ()
      {
         var claimIdentity = (ClaimsIdentity)User.Identity;
         var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

         ShoppingCardVM = new ShoppingCardVM()
         {
            CartList = _context.ShoppingCart.GetAll(s => s.ApplicationUserId == claim.Value, includeProperties: "Product"),
            OrderHeader = new()
         };

			ShoppingCardVM.OrderHeader.Name = ShoppingCardVM.OrderHeader.ApplicationUser.Name;
			ShoppingCardVM.OrderHeader.PhoneNumber = ShoppingCardVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCardVM.OrderHeader.StreetAddress = ShoppingCardVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCardVM.OrderHeader.City = ShoppingCardVM.OrderHeader.ApplicationUser.City;
			ShoppingCardVM.OrderHeader.State = ShoppingCardVM.OrderHeader.ApplicationUser.State;
			ShoppingCardVM.OrderHeader.PostalCode = ShoppingCardVM.OrderHeader.ApplicationUser.PostalCode;

			foreach (var cart in ShoppingCardVM.CartList)
         {
            cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
            ShoppingCardVM.OrderHeader.OrderTotal += (cart.Price * cart.Count); 
         }
         return View(ShoppingCardVM);
      }

      [HttpPost]
      [ActionName("Summary")]
      [ValidateAntiForgeryToken]
      public IActionResult SummaryPOST()
      {
         var claimsIdentity = (ClaimsIdentity)User.Identity;
         var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

         ShoppingCardVM.OrderHeader.OrderDate = System.DateTime.Now;
         ShoppingCardVM.OrderHeader.ApplicationUserId = claim.Value;

         foreach (var cart in ShoppingCardVM.CartList)
         {
            cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
            ShoppingCardVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
         }

         ApplicationUser applicationUser = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

         if (applicationUser.CompanyId.GetValueOrDefault() == 0)
         {
            ShoppingCardVM.OrderHeader.PaymentStatus = SD.PyamentStatusPending;
            ShoppingCardVM.OrderHeader.OrderStatus = SD.StatusPending;
         }
         else
         {
            ShoppingCardVM.OrderHeader.PaymentStatus = SD.PyamentStatusDelayedPayment;
            ShoppingCardVM.OrderHeader.OrderStatus = SD.StatusPending;
         }

         _context.OrderHeader.Add(ShoppingCardVM.OrderHeader);
         _context.Save();

         foreach (var cart in ShoppingCardVM.CartList)
         {
            OrderDetail orderDetail = new OrderDetail()
            {
               ProductId = cart.ProductId,
               OrderId = ShoppingCardVM.OrderHeader.Id,
               Price = cart.Price,
               Count = cart.Count,
            };
            _context.OrderDetail.Add(orderDetail);
            _context.Save();
         }

         //if (applicationUser.CompanyId.GetValueOrDefault() == 0)
         //{
         //   //
         //}
      }

      // Edit Item Count
      public IActionResult Edit(int productId)
      {
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCart cartFrobDb = _context.ShoppingCart.GetFirstOrDefault(s => s.ApplicationUserId == claim.Value && s.ProductId == productId, includeProperties:"Product");
         Product product = _context.Product.GetFirstOrDefault(p => p.Id == productId, includeProperties: "Category,CoverType");

         cartFrobDb.Product = product;
			return View(cartFrobDb);
      }

		// Edit Item Count
		[HttpPost]
      public IActionResult Edit(ShoppingCart model, int id)
      {
			ShoppingCart cartFrobDb = _context.ShoppingCart.GetFirstOrDefault(s => s.Id == id );
         cartFrobDb.Count = model.Count;

         _context.Save();
			return RedirectToAction(nameof(Index));
      }

      // Add item from card
      public ActionResult Plus(int cartId)
      {
         var cart = _context.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
         _context.ShoppingCart.IncrimentCount(cart, 1);
         _context.Save();
         return RedirectToAction(nameof(Index));
      }

      // Minus Item from card
      public ActionResult Minus(int cartId)
      {
         var cart = _context.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
         if (cart.Count <= 1)
         {
            _context.ShoppingCart.Remove(cart);
         }
         else
         {
            _context.ShoppingCart.DecrimentCount(cart, 1);
         }
         _context.Save();
         return RedirectToAction(nameof(Index));
      }

      // Remove product item from cart
      public ActionResult Remove(int cartId)
      {
         var cart = _context.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
         _context.ShoppingCart.Remove(cart);
         _context.Save();
         return RedirectToAction(nameof(Index));
      }

      // Product Price functionality
      private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
      {
         if (quantity <= 50)
         {
            return price;
         }
         else
         {
            if (quantity <= 100)
            {
               return price50;
            }
            return price100;
         }
      }
   }
}
