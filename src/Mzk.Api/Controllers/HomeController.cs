using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mzk.Api.Models;
using Mzk.Api.Services;

namespace MzkApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IListDownloader _listDownloader;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, IListDownloader listDownloader, AppDbContext context)
        {
            _logger = logger;
            _listDownloader = listDownloader;
            _appDbContext = context;
        }
        [HttpGet("getInfo/{busNumber}/{stationNumber}")]
        public IActionResult GetStationInfo(string busNumber, string stationNumber)
        {
            // var x =_appDbContext.Stations.FirstOrDefault(s => s.Id == 2);
            // List<TimesOfArrivals> itemListOfTimesOfArrivals = _appDbContext.TimesOfArrivalsWithPeriods.Where(t => t.StationId == 1).ToList();
            //  x.TimesOfArrivalsWithPeriods.AddRange(itemListOfTimesOfArrivals);
            // x.TimesOfArrivalsWithPeriods. = _appDbContext.TimesOfArrivalsWithPeriods
            var x = _listDownloader.GetStationInfo("https://mzkwejherowo.pl/rozklad-jazdy/1-263-01.html");
            return Ok(x);
        }
    }
}
