namespace Football.Core.Models.Players
{
    using System;

    public class PlayerListingViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; init; }

        public string MiddleName { get; init; }

        public string LastName { get; init; }

        public int Age { get; init; }

        public double Weight { get; init; }

        public double Height { get; init; }

        public string Image { get; set; }

        public int Goal { get; init; }

        public byte ShirtNumber { get; init; }

        public string Nationality { get; init; }

        public string Team { get; init; }

        public string Description { get; init; }
        public string Position { get; init; }
    }
}
