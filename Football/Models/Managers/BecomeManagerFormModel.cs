namespace Football.Models.Managers
{
    using System.ComponentModel.DataAnnotations;

    using static Football.Infrastructure.Data.DataConstants.Manager;

    public class BecomeManagerFormModel
    {
        [Required]
        [Display(Name = "Име")]
        [StringLength(ManagerNameMaxLength, MinimumLength = ManagerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Тел. номер")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }
    }
}
