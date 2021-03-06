namespace Football.Core.Models.Leagues
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Contracts;
    using Football.Core.Services.Leagues.Models;
    using Microsoft.AspNetCore.Http;

    using static Football.Infrastructure.Data.DataConstants.League;
    public class LeagueFormModel : ILeagueModel
    {
        [Display(Name = "Име")]
        [Required]
        [StringLength(LeagueNameMaxLength, MinimumLength = LeagueNameMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Name { get; set; }

        [Display(Name = "Снимка")]
        [Required]
        public IFormFile Image { get; set; }

        [Display(Name = "Описание")]
        [Required()]
        [StringLength(int.MaxValue,
            MinimumLength = LeagueDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }

        [Display(Name = "Отбор")]
        public Guid TeamId { get; set; }

        public IEnumerable<LeagueTeamServiceModel> Teams { get; set; }
    }
}
