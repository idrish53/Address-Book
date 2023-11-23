using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBook1.Areas.CON_Contact.Models
{
    public class CON_ContactModel
    {
        public int? ContactID { get; set; }

        [Required]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [DisplayName("Contact Address")]
        public string? ContactAddress { get; set; }
        //public string CountryName { get; set; }
        public int CountryID { get; set; }

        //public string StateName { get; set; }
        public int StateID { get; set; }

        //public string CityName { get; set; }
        public int CityID { get; set; }

        [Required]
        [DisplayName("PinCode")]
        public int PinCode { get; set; }

        [Required]
        [DisplayName("Mobile")]
        [StringLength(10,MinimumLength =10)]
        public string Mobile { get; set; }

        [Required]
        [DisplayName("Alternate Contact")]
        [StringLength(10, MinimumLength = 10)]
        public string AlternateContact { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime AnniversaryDate { get; set; }

        [Required]
        [DisplayName("LinkedIn")]
        public string? LinkedIn { get; set; }

        [Required]
        [DisplayName("Twitter")]
        public string? Twitter { get; set; }

        [Required]
        [DisplayName("Insta")]
        public string? Insta { get; set; }

        [Required]
        [DisplayName("Type Of Profesison")]
        public string TypeOfProfesison { get; set; }

        [Required]
        [DisplayName("CompanyName")]
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("Designation")]
        public string Designation { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }

    }
}
