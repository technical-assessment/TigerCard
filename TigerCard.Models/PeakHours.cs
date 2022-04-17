using System;
using System.Collections.Generic;

namespace TigerCard.Models
{
    public class PeakHours
    {
        public DayOfWeek Day { get; set; }
        public List<Window> Windows { get; set; }
    }
}
