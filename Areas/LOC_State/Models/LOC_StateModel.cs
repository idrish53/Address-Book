using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook1.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }
        public string StateName { get; set; }

        public string StateCode { get; set; }
        [Required]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        public int CountryID { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
        public int UserID { get; set;}
    }

    public class Loc_StateDropDownModle
    {
        public int StateID { get; set; }

        public string StateName { get; set; }
    }
}
