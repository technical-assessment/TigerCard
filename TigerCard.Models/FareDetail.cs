using System;
namespace TigerCard.Models
{
    public class FareDetail
    {
        public string SourceZoneId { get; set; }

        public string ToZoneId { get; set; }

        public double BaseFare { get; set; }

        public double PeakFare { get; set; }
    }
}
