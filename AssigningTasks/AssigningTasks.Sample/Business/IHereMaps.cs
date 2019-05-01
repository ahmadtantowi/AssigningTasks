using AssigningTasks.Sample.Models;
using System;
using System.Threading.Tasks;

namespace AssigningTasks.Sample.Business
{
    public interface IHereMaps
    {
        Task<DiscoverSearchModel> DiscoverSearch(string query, double lat, double lng);
        Task<DiscoverSearchModel> DiscoverSearch(string url);
    }
}