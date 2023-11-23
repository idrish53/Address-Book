using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBook1.Areas.MST_ContactCategory.Models
{
    public class MST_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }
        [Required]
        [DisplayName("Contact Category Name")]
        public string ContactCategoryName { get; set; }
        [Required]
        [DisplayName("Contact Category Relation")]
        public string? ContactCategoryRelation { get; set; }

        //public int CountryID { get; set; }
        //public int StateID { get; set; }
        //public int CityID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
    }
}
