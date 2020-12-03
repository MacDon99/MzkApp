using System.Collections.Generic;

namespace Mzk.Api.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusNumber { get; set; }
        public List<TimesOfArrivals> TimesOfArrivalsWithPeriods { get; set; } = new List<TimesOfArrivals>();
        public List<string> NextStations { get; set; } = new List<string>();
        public List<string> Legend { get; set; } = new List<string>();
        public string Direction { get; set; }
    }
}