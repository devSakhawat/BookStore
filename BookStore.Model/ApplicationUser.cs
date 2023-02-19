﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
   public class ApplicationUser : IdentityUser
   {
      [Required]
      public string Name { get; set; }
      public string? StreetAddress { get; set; }
      public string? City { get; set; }
      public string? State { get; set; }
      public string? PostalCode { get; set; }

      [ForeignKey("CompanyId")]
      public int? CompanyId { get; set; }
      [ValidateNever]
      public Company Company { get; set; }
   }
}
