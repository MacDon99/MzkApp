using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mzk.Api.Models;
using Mzk.Api.Services;

namespace Mzk.Api.Repositories
{
    public class Seeder
    {
        private readonly AppDbContext _appDbContext;
        private readonly IListDownloader _listDownloader;
        private List<StationToSave> StationsToSave { get; set; }
        private List<Station> StationsToPass { get; set; } = new List<Station>();
        private List<TimesOfArrivals> TimesOfArrivalsToPass { get; set; } = new List<TimesOfArrivals>();

        public Seeder(AppDbContext appDbContext, IListDownloader listDownloader)
        {
            _appDbContext = appDbContext;
            _listDownloader = listDownloader;
        }

        public void SaveListsOfBussesToDatabase()
        {
            if(!_appDbContext.Stations.Any())
            {
                createObjects();
                _appDbContext.AddRange(StationsToPass);
                _appDbContext.SaveChanges();
            }
        }
        private void CreateStations()
        {
            //NamePattern: Bus1Stacja1Kierunek1 => B1S1K1
            StationsToSave = new List<StationToSave>(){
                //B1K1
                new StationToSave(){ Link = "https://mzkwejherowo.pl/rozklad-jazdy/1-263-01.html", Name = "B1S1K1", BusNumber = 1},
                new StationToSave(){ Link = "https://mzkwejherowo.pl/rozklad-jazdy/1-261-01.html", Name = "B1S2K1", BusNumber = 1},
                new StationToSave(){ Link = "https://mzkwejherowo.pl/rozklad-jazdy/1-101-01.html", Name = "B1S3K1", BusNumber = 1},
                new StationToSave(){ Link = "https://mzkwejherowo.pl/rozklad-jazdy/1-102-01.html", Name = "B1S4K1", BusNumber = 1},
                new StationToSave(){ Link = "https://mzkwejherowo.pl/rozklad-jazdy/1-103-01.html", Name = "B1S5K1", BusNumber = 1},
                //B1K2
                //itp
            };
        }
        private void createObjects()
        {
            CreateStations();
            ListDownloader downloader;
            foreach(var station in StationsToSave)
            {
                downloader = new ListDownloader(_appDbContext);
                var ReceivedData = downloader.GetStationInfo(station.Link);
                ReceivedData.Name = station.Name;
                var rgx = new Regex(@"(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]?");
                var stringToCut = rgx.Match(ReceivedData.NextStations[ReceivedData.NextStations.Count-1]).ToString();
                ReceivedData.Direction = ReceivedData.NextStations[ReceivedData.NextStations.Count-1].Replace(stringToCut + " " , "");
                
                StationsToPass.Add(ReceivedData);
            }
        }
    }
}