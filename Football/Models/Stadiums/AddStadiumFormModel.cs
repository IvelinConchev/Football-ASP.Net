namespace Football.Models.Stadiums
{
    using System.ComponentModel.DataAnnotations;

    using static Football.Infrastructure.Data.DataConstants.Stadium;

    public class AddStadiumFormModel
    {

        [Display(Name = "City Name")]
        [Required]
        [StringLength(StadiumNameMaxLength, MinimumLength = StadiumNameMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        public int Capacity { get; set; }

        [Display(Name = "Описание")]
        [Required()]
        [StringLength(int.MaxValue,
            MinimumLength = StadiumDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "City Address")]
        [Required]
        [StringLength(StadiumAddressMaxLength, MinimumLength = StadiumAddressMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; init; }

        public IEnumerable<StadiumCitiesViewModel> Cities { get; set; }
    }
}
