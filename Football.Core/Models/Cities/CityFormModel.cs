namespace Football.Core.Models.Cities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Contracts;
    using Football.Core.Services.Cities.Models;
    using Microsoft.AspNetCore.Http;

    using static Football.Infrastructure.Data.DataConstants.City;

    public class CityFormModel : ICityModel
    {
        [Display(Name = "Име")]
        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Name { get; set; }

        [Display(Name = "Пощенски код")]
        [Required]
        [StringLength(CityPostCodeMaxLength, MinimumLength = CityPostCodeMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string PostCode { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }

        [Display(Name = "Описание")]
        [Required()]
        [StringLength(int.MaxValue,
            MinimumLength = CityDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Отбор")]
        public Guid TeamId { get; init; }

        public IEnumerable<CityTeamsServiceModel> Teams { get; set; }
    }
}
