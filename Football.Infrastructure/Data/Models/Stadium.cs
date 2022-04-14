namespace Football.Infrastructure.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.DefaultLengthForKeyGuid;
    using static Data.DataConstants.Stadium;

    public class Stadium
    {
        [Key]
        [StringLength(DefaultLength)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(StadiumNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }

        public int Capacity { get; set; }

        [Required]
        [StringLength(StadiumAddressMaxLength)]
        public string Address { get; set; }
    }
}
