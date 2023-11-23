using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook1.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }
        [Required]
        [DisplayName("Country Name")]
        [StringLength(20,MinimumLength =3)]  

        public string? CountryName { get; set; }
        public int CountryCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; } 
        public string PhotoPath { get; set; }

    }

    public class Loc_CountryDropDownModle
    {
        public int? CountryID { get; set;}

        public string? CountryName { get; set;}  
    }
}  
