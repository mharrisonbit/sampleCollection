using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CollectionViewSample.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CollectionViewSample.Interfaces
{
    public class GetDataAsync : IGetDataAsync
    {
        bool _validateStatus;

        HttpClient _client = new HttpClient();

        public bool Validate()
        {
            return _validateStatus;
        }

        public async Task<JObject> GetAllPOIAsync(string url)
        {
            JObject _json = new JObject();
            try
            {
                HttpResponseMessage response = null;
                var uri = new Uri(string.Format(url, string.Empty));
                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var _jsonTemp = JsonConvert.DeserializeObject<JArray>(data);
                    _json = new JObject(
                        new JProperty("data", _jsonTemp)
                    );
                    _validateStatus = true;
                }
                else
                {
                    _validateStatus = false;
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    _json = new JObject(
                        new JProperty("Reason", response.ReasonPhrase),
                        new JProperty("StatusCode", response.StatusCode)
                    );
                    _validateStatus = false;
                }
                response.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("                GetAllPOIModel:GetAllPOIModelRESTAsync {0}", ex.Message));
                _validateStatus = false;
            }
            return _json;
        }

        public async Task<ObservableCollection<CityPOI>> GetPOIByTypeAsync(string type)
        {
            ObservableCollection<CityPOI> _json = new ObservableCollection<CityPOI>();
            try
            {
                Uri uri = new Uri($"{Constants.RestUrl}{Constants.GetAllPOI}{Constants.ProfileId}/{type}{Constants.AppendUpdateFormat}");
                HttpResponseMessage response = null;

                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var _jsonTemp = JsonConvert.DeserializeObject<JArray>(data);
                    var tempJson = new JObject(
                        new JProperty("data", _jsonTemp)
                    );

                    _json = BuildCollection(tempJson);

                    _validateStatus = true;
                }
                else
                {
                    _validateStatus = false;
                }

                response.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!!!                   GetDataAsync:GetShopsPOIAsync " + ex.Message);

                _validateStatus = false;
            }

            return _json;
        }

        public async Task<ObservableCollection<CityPOI>> GetLimitedPOIByTypeAsync(string type)
        {
            ObservableCollection<CityPOI> _json = new ObservableCollection<CityPOI>();
            try
            {
                Uri uri = new Uri($"{Constants.RestUrl}{Constants.GetLimitedPoi}{Constants.ProfileId}/{type}{Constants.AppendUpdateFormat}");
                HttpResponseMessage response = null;

                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var _jsonTemp = JsonConvert.DeserializeObject<JArray>(data);
                    var tempJson = new JObject(
                        new JProperty("data", _jsonTemp)
                    );

                    _json = BuildCollection(tempJson);

                    _validateStatus = true;
                }
                else
                {
                    _validateStatus = false;
                }

                response.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!!!                   GetDataAsync:GetShopsPOIAsync " + ex.Message);

                _validateStatus = false;
            }

            return _json;
        }

        private ObservableCollection<CityPOI> BuildCollection(JObject items)
        {
            ObservableCollection<CityPOI> cityPOIs = new ObservableCollection<CityPOI>();

            foreach (var data in items["data"])
            {
                var poiTitle = data["title"].ToString();
                var poiTID = data["field_type_poi"].ToString();

                string poiDescription = data["field_description_poi"] != null ? data["field_description_poi"].ToString() : "";
                string latitude = data["field_latitude"] != null ? data["field_latitude"].ToString() : "";
                string longitude = data["field_longitude"] != null ? data["field_longitude"].ToString() : "";
                string phone = data["field_phone_poi"] != null ? data["field_phone_poi"].ToString() : "";
                string linkText = data["field_linkText"] != null ? data["field_linkText"].ToString() : "";
                string linkURL = data["field_linkURL"] != null ? data["field_linkURL"].ToString() : "";
                string logo = data["field_logo_poi"] != null ? data["field_logo_poi"].ToString() : "";
                string organization = data["field_venue_poi_organization"] != null ? data["field_venue_poi_organization"].ToString() : "";
                string addressLineOne = data["field_venue_poi_address_line1"] != null ? data["field_venue_poi_address_line1"].ToString() : "";
                string addressLineTwo = data["field_venue_poi_address_line2"] != null ? data["field_venue_poi_address_line2"].ToString() : "";
                string city = data["field_venue_poi_locality"] != null ? data["field_venue_poi_locality"].ToString() : "";
                string locality = data["field_venue_poi_locality"] != null ? data["field_venue_poi_locality"].ToString() : "";
                string administrative_area = data["field_venue_poi_administrative_area"] != null ? data["field_venue_poi_administrative_area"].ToString() : "";
                string postal_code = data["field_venue_poi_postal_code"] != null ? data["field_venue_poi_postal_code"].ToString() : "";
                string assetList = data["field_assets_poi"] != null ? data["field_assets_poi"].ToString() : "";
                string startDateTime = data["field_start_date"] != null ? data["field_start_date"].ToString() : "";
                string endDateTime = data["field_end_date"] != null ? data["field_end_date"].ToString() : "";

                var newPOI = new CityPOI
                {
                    Type = poiTID,
                    Title = poiTitle,
                    Description = poiDescription,
                    Image = Constants.ImageBase + logo,
                    Latitidue = Convert.ToDouble(latitude),
                    Longitude = Convert.ToDouble(longitude),
                    AddressLineOne = addressLineOne,
                    AddressLineTwo = addressLineTwo,
                    Locality = locality,
                    Organization = organization,
                    LinkText = linkText,
                    LinkURL = linkURL,
                    Phone = phone,
                    AdministrativeArea = administrative_area,
                    PostalCode = postal_code,
                    NID = data["nid"].ToString(),
                    StartDate = startDateTime,
                    EndDate = endDateTime
                };

                cityPOIs.Add(newPOI);
            }

            return cityPOIs;
        }
    }
}
