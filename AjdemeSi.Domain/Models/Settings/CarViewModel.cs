using System.ComponentModel.DataAnnotations;

namespace AjdemeSi.Domain.Models.Settings
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int YearOfManufacture { get; set; }
        [Required]
        public int NumberOfSits { get; set; }
        [Required]
        public bool IsDefault { get; set; }
    }
}
