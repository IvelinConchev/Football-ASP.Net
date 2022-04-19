namespace Football.Core.Models.Stadiums
{
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Services.Stadiums.Models;
    using Microsoft.AspNetCore.Http;

    using static Football.Infrastructure.Data.DataConstants.Stadium;
    public class StadiumFormModel
    {
        [Display(Name = "Име")]
        [Required]
        [StringLength(StadiumNameMaxLength, MinimumLength = StadiumNameMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Display(Name = "Описание")]
        [Required()]
        [StringLength(int.MaxValue,
           MinimumLength = StadiumDescriptionMinLength,
           ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }

        [Display(Name = "Capacity")]
        [Range(StadiumCapacityMinLength, StadiumCapacityMaxLength, ErrorMessage = "Minimum: {1}, Maximum: {2}")]
        public int Capacity { get; set; }

        [Display(Name = "Address")]
        [Required]
        [StringLength(StadiumAddressMaxLength, MinimumLength = StadiumAddressMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; set; }

        public IEnumerable<StadiumCitiesServiceModel> Cities { get; set; }
    }
}
