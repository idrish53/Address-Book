using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook1.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }

        [Required]
        [DisplayName("City Name")]
        public string CityName { get; set; }

        [Required]
        [DisplayName("City Code")]
        public string? CityCode { get; set; }

        public int CountryID { get; set; }

        public int StateID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
    }
    public class Loc_CityDropDownModle
    {
        public int CityID { get; set; }

        public string CityName { get; set; }
    }

}
