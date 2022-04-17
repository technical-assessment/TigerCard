using System;
using System.Collections.Generic;

namespace TigerCard.Models
{
    public interface IBusinessConfigurationProvider
    {
        List<PeakHours> GetPeakHours();

        List<FareDetail> GetFareDetails();

        FareCapLimit GetFareCapLimit(string fromZoneId, string toZoneId);
    }
}
