﻿namespace Football.Core.Models.Players
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Services.Players;
    using Football.Core.Services.Players.Models;
    using Microsoft.AspNetCore.Http;

    using static Football.Infrastructure.Data.DataConstants.Player;

    public class PlayerFormModel
    {
        [Display(Name = "Име")]
        [Required]
        [StringLength(PlayerFirstNameMaxLength, MinimumLength = PlayerFirstNameMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string FirstName { get; init; }

        [Display(Name = "Презиме")]
        [Required]
        [StringLength(PlayerMiddleNameMaxLength, MinimumLength = PlayerMiddleNameMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string MiddleName { get; init; }

        [Display(Name = "Фамилия")]
        [Required]
        [StringLength(PlayerLastNameMaxLength, MinimumLength = PlayerLastNameMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string LastName { get; init; }

        [Display(Name = "Отбор")]
        [Required]
        [StringLength(PlayerTeamMaxLength, MinimumLength = PlayerTeamMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Team { get; init; }

        [Display(Name = "Години")]
        [Range(PlayerAgeMinLength, PlayerAgeMaxLength, ErrorMessage = "Minimum: {1}, Maximum: {2}")]
        public int Age { get; init; }

        [Display(Name = "Килограми")]
        public double Weight { get; init; }

        [Display(Name = "Височина")]
        public double Height { get; init; }

        [Display(Name = "Image")]
        [Required]
        public IFormFile Image { get; init; }

        [Display(Name = "Голове")]
        public int Goal { get; init; }

        [Display(Name = "Номер на играча")]
        [Range(PlayerShirtNumberMinValue, PlayerShirtNumberMaxValue, ErrorMessage = "Minimum: {1}, Maximum: {2}")]
        public byte ShirtNumber { get; init; }

        [Display(Name = "Националност")]
        [Required]
        [StringLength(PlayerNationalityMaxLength, MinimumLength = PlayerNationalityMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Nationality { get; init; }
        
        [Display(Name = "Описание")]
        [Required()]
        [StringLength(int.MaxValue,
            MinimumLength = PlayerDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Позиция")]
        public Guid PositionId { get; init; }

        public IEnumerable<PlayerPositionServiceModel> Positions { get; set; }
    }
}
