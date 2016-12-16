using System.ComponentModel.DataAnnotations;

namespace DataCatalogApi.Models
{
    public class PersonModel
    {
        public PersonModel(string first, string last, string gender, string favoriteColor, string birthDate)
        {
            FirstName = first;
            LastName = last;
            Gender = gender;
            FavoriteColor = favoriteColor;
            BirthDate = birthDate;
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string FavoriteColor { get; set; }
        [Required]
        public string BirthDate { get; set; }
    }
}