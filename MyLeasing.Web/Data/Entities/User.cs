﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyLeasing.Web.Data.Entities
{
    public class User : IdentityUser
    {
        public int Id { get; set; }

        [DisplayName("Document*")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [DisplayName("First Name*")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name*")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string? Address { get; set; }

                

        [DisplayName("Owner Name")]
        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument =>$"{FirstName} {LastName} - {Document}";
    }
}
