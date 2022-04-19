﻿namespace Football.Core.Services.Stadiums.Models
{
    using System;

    public class StadiumServiceModel
    {
        public Guid Id { get; set; } 

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }
    }
}
