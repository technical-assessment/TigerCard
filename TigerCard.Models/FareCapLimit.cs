using System;
namespace TigerCard.Models
{
    public class FareCapLimit
    {
        public double Weekly { get; set; }

        public double Daily { get; set; }

        public string FromZoneId { get; set; }

        public string ToZoneId { get; set; }
    }
}
