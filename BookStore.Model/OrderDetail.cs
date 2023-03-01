using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
	public class OrderDetail
	{
		public int Id { get; set; }

		[Required]
		[ForeignKey("OrderId")]
		public int OrderId { get; set; }
		[ValidateNever]
		public OrderHeader OrderHeader { get; set; }

		[Required]
		[ForeignKey("ProductId")]
		public int ProductId { get; set; }
		[ValidateNever]
		public Product Product { get; set; }


		public int Count { get; set; }

		public double Price { get; set; }
	}
}
