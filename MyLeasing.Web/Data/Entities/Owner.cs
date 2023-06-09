﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner : IEntity
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Document*")]
        public string Document { get; set; }

        [Required]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name*")]
        public string LastName { get; set; }

        [Display(Name = "Fixed Phone")]
        public int? FixedPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public int? CellPhone { get; set; }

        public string? Address { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerName => $"{FirstName} {LastName}";

        public User User { get; set; }

        [Display(Name = "Photo")]
        public string ImageUrl { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }

                return $"https://localhost:44334{ImageUrl.Substring(1)}";
            }
        }
    }
}

        
  
