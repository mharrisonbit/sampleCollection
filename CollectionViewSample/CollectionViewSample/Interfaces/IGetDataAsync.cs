using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CollectionViewSample.Models;
using Newtonsoft.Json.Linq;

namespace CollectionViewSample.Interfaces
{
    public interface IGetDataAsync
    {
        Task<JObject> GetAllPOIAsync(string url);
        Task<ObservableCollection<CityPOI>> GetLimitedPOIByTypeAsync(string type);
        Task<ObservableCollection<CityPOI>> GetPOIByTypeAsync(string type);
        bool Validate();
    }
}