using Mzk.Api.Models;

namespace Mzk.Api.Services
{
    public interface IListDownloader
    {
        Station GetStationInfo(string site);
    }
}