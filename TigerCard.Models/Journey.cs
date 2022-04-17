using System;
namespace TigerCard.Models
{
    public class Journey
    {
        public DateTime Date { get; set; }

        public Zone FromZone { get; set; }

        public Zone ToZone { get; set; }

        public double Fare { get; set; }
    }
}
