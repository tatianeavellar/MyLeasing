using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : IEntity
    {
        public int Id { get; set ; }

        [Required]
        [Display(Name = "Document*")]
        public string Document { get; set; }

        [Required]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name*")]
        public string LastName { get; set; }

        [Display(Name = "Lessee Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }

        [Required]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        public string Address { get; set; }

        public User User { get; set; }

        [Display(Name = "Photo")]
        public string ImageUrl { get; set; }

        [Display(Name = "Full Name - Document")]
        public string FullNameWithDocument
        {
            get
            {
                return $"{FirstName} {LastName} - {Document}";
            }
        }

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
