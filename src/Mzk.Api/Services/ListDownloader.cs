using System.Net;
using Mzk.Api.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace Mzk.Api.Services
{
    public class ListDownloader : IListDownloader
    {
        private WebClient WebClient { get; set; } = new WebClient();
        private Station ReceivedData { get; set; } = new Station();
        private HtmlDocument HtmlDoc { get; set; } = new HtmlDocument();
        private readonly AppDbContext _appDbContext;
        public ListDownloader(AppDbContext context)
        {
            _appDbContext = context;
        }
        public Station GetStationInfo(string site)
        {


            // ReceivedData.TimesOfArrivalsWithPeriods = GetTimesOfArrivalsWithPeriods();
            // ReceivedData.NextStations = GetListOfNextStationsWithTravelTimes();
            // ReceivedData.Legend = GetLegend();
            var pageContent = WebClient.DownloadString(site);
            HtmlDoc.LoadHtml(pageContent);
            GetTimesOfArrivalsWithPeriods();
            GetListOfNextStationsWithTravelTimes();
            GetLegend();
            return ReceivedData;
            // throw new System.NotImplementedException();
        }
        private void GetTimesOfArrivalsWithPeriods()
        {
             List<string> TotaLList = GetTotalListOfTimes();
             List<string> Titles = GetListsOfTitles();
             TimesOfArrivals TimesOfArrivals = new TimesOfArrivals();
            int position = 0;
            var count = TotaLList.Count;

            try
            {
                //list one
                TimesOfArrivals.Period = Titles[2];
                ReceivedData.TimesOfArrivalsWithPeriods.Add(TimesOfArrivals);
                AddListsToTitles(TotaLList, ref position, 0);
                //list two
                TimesOfArrivals = new TimesOfArrivals();
                TimesOfArrivals.Period = Titles[3];
                ReceivedData.TimesOfArrivalsWithPeriods.Add(TimesOfArrivals);
                AddListsToTitles(TotaLList, ref position, 1);
                //list three
                TimesOfArrivals = new TimesOfArrivals();
                TimesOfArrivals.Period = Titles[4];
                ReceivedData.TimesOfArrivalsWithPeriods.Add(TimesOfArrivals);
                AddListsToTitles(TotaLList, ref position, 2);
                //list four
                TimesOfArrivals = new TimesOfArrivals();
                TimesOfArrivals.Period = Titles[5];
                ReceivedData.TimesOfArrivalsWithPeriods.Add(TimesOfArrivals);
                AddListsToTitles(TotaLList, ref position, 3);
            }
            catch { }
        }
        private void GetListOfNextStationsWithTravelTimes()
        {
            string innerText;
            List<string> Times = new List<string>();
            List<string> Stations = new List<string>();
            List<string> ListOfStationsAndTimesToGet = new List<string>();

            foreach (var td in HtmlDoc.DocumentNode.SelectNodes("//td"))
            {
                HtmlAttribute classAttribute = td.Attributes["class"];

                if (classAttribute != null)
                {
                    if (classAttribute.Value == "czas")
                    {
                        innerText = td.InnerText.ToString().Trim();
                        Times.Add(innerText);
                    }
                    if (classAttribute.Value == "przyst")
                    {
                        innerText = td.InnerText.ToString().Trim().Replace("\u00A0", " ").Replace("&quot", "");
                        Stations.Add(innerText);
                    }
                }
            }

            Times.Add("");
            for (int i = 0; i < Times.Count; i++)
            {

                if (Times[i] != "")
                {
                    ListOfStationsAndTimesToGet.Add(Times[i] + " " + Stations[i]);
                }
                else
                {
                    ListOfStationsAndTimesToGet.Add("      " + Stations[i]);
                }
                if (Times[i] != "" && (Times[i].Length > Times[i + 1].Length || Convert.ToInt32("" + Times[i+1][0] + Times[i + 1][1] + Times[i + 1][3] + Times[i + 1][4]) < Convert.ToInt32("" + Times[i][0] + Times[i][1] + Times[i][3] + Times[i][4])) )
                {
                    break;
                }
            }
            ReceivedData.NextStations = ListOfStationsAndTimesToGet;
        }
        private void GetLegend() {
            var Legend = new List<string>();
            string innerText;
            var NodeDL = HtmlDoc.DocumentNode.SelectNodes("//dl");
            try
            {
                var inner = NodeDL[0].InnerHtml;
                var doc = new HtmlDocument();
                doc.LoadHtml(inner);

                foreach (HtmlNode dt in doc.DocumentNode.SelectNodes("//dt"))
                {
                    innerText = dt.InnerText.ToString().Trim();
                    Legend.Add(innerText);
                }

                for (int i = 0; i < Legend.Count; i++)
                {
                        var node = doc.DocumentNode.SelectNodes("//dd")[i];
                        Legend[i] += " " + node.InnerText.ToString().Trim();
                }
            }
            catch { }
            ReceivedData.Legend = Legend;
        }
        private List<string> GetTotalListOfTimes()
        {
            string innerText;
            List<string> TotalListOfTimes = new List<string>();
            foreach (var td in HtmlDoc.DocumentNode.SelectNodes("//td"))
            {
                HtmlAttribute classAttribute = td.Attributes["class"];

                if (classAttribute != null)
                {
                    if (classAttribute.Value == "godz")
                    {
                        innerText = td.InnerText.ToString();
                        var search = @"(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9](\s[A-Z])?";
                        Regex rgx = new Regex(search);

                        foreach (Match match in rgx.Matches(innerText))
                        {
                            TotalListOfTimes.Add(match.ToString());
                        }
                    }

                }
            }
            return TotalListOfTimes;
        }
        private List<string> GetListsOfTitles()
        {
            var allNodes = HtmlDoc.DocumentNode.SelectNodes("//h4");
            List<string> Titles = new List<string>();
            string innerText;
            try
            {
                for (int j = 0; j < allNodes.Count; j++)
                {

                    var currentNode = allNodes[j].InnerText.ToString().Trim();
                    if (currentNode != "Czas" || j == 0)
                    {
                        Titles.Add(innerText = allNodes[j].InnerText.ToString().Trim());
                    }
                    if (Titles[0] == currentNode && j != 0)
                    {
                        break;
                    }

                }
            }
            catch { }
            return Titles;

        }
        private void AddListsToTitles(List<string> TotaLList, ref int position, int CurrentList)
        {
            //var ReceivedData.TimesOfArrivalsWithPeriods = new List();
            var count = TotaLList.Count();
            //int position = 0;
            if (!(ReceivedData.TimesOfArrivalsWithPeriods[CurrentList].Period is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        //jeżeli liczba po konwersji z godziny np 14:10 -> 1410 jest mniejsza od poprzedniej, to działa dalej
                        if (Convert.ToInt32("" + TotaLList[i][0] + TotaLList[i][1] + TotaLList[i][3] + TotaLList[i][4]) < Convert.ToInt32("" + TotaLList[i + 1][0] + TotaLList[i + 1][1] + TotaLList[i + 1][3] + TotaLList[i + 1][4]))
                        {
                            ReceivedData.TimesOfArrivalsWithPeriods[CurrentList].Times.Add(TotaLList[i]);
                            // List[0].Add(TotaLList[i]);

                        }
                        else
                        {
                            ReceivedData.TimesOfArrivalsWithPeriods[CurrentList].Times.Add(TotaLList[i]);
                            position = i + 1;
                            break;

                        }

                    }
                }
        }
    }
}