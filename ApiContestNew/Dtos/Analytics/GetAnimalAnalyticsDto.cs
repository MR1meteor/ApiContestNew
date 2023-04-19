﻿namespace ApiContestNew.Dtos.Analytics
{
    public record GetAnimalAnalyticsDto
    {
        public string AnimalType { get; set; } = string.Empty;
        public long AnimalTypeId { get; set; }
        public long QuantityAnimals { get; set; }
        public long AnimalsArrived { get; set; }
        public long AnimalsGone { get; set; }
    }
}
