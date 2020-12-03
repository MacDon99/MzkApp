using System.Collections.Generic;

namespace Mzk.Api.Models
{
    public class Stations
    {
        public int Id { get; set; }
        public int BusNumber { get; set; }
        public List<string> StationsFirstDirection { get; set; } = new List<string>();
        public List<string> StationsSecondDirection { get; set; } = new List<string>();
    }
}