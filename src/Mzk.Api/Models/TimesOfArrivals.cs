using System.Collections.Generic;

namespace Mzk.Api.Models
{
    public class TimesOfArrivals
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public List<string> Times { get; set; } = new List<string>();
        public int StationId { get; set; }
    }
}