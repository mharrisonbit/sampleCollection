using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CollectionViewSample.Interfaces;
using CollectionViewSample.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace CollectionViewSample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetDataAsync _getData) : base(navigationService)
        {
            this._getData = _getData;
            _dialogService = dialogService;
            _navigationService = navigationService;
            RefreshList = new DelegateCommand(RefreshHomeLists);
            GetData();
        }

        ObservableCollection<CityPOI> _FilterList;
        public ObservableCollection<CityPOI> FilterList
        {
            get { return _FilterList; }
            set { SetProperty(ref _FilterList, value); }
        }

        ObservableCollection<CityPOI> _myEvents;
        public ObservableCollection<CityPOI> MyEvents
        {
            get { return _myEvents; }
            set { SetProperty(ref _myEvents, value); }
        }

        ObservableCollection<CityPOI> _LimitedEvents;
        public ObservableCollection<CityPOI> LimitedEvents
        {
            get { return _LimitedEvents; }
            set { SetProperty(ref _LimitedEvents, value); }
        }

        ObservableCollection<CityPOI> _myRestaurants;
        public ObservableCollection<CityPOI> MyRestaurants
        {
            get { return _myRestaurants; }
            set { SetProperty(ref _myRestaurants, value); }
        }

        ObservableCollection<CityPOI> _LimitedRestaurants;
        public ObservableCollection<CityPOI> LimitedRestaurants
        {
            get { return _LimitedRestaurants; }
            set { SetProperty(ref _LimitedRestaurants, value); }
        }

        ObservableCollection<CityPOI> _myShops;
        public ObservableCollection<CityPOI> MyShops
        {
            get { return _myShops; }
            set { SetProperty(ref _myShops, value); }
        }

        ObservableCollection<CityPOI> _LimitedShops;
        public ObservableCollection<CityPOI> LimitedShops
        {
            get { return _LimitedShops; }
            set { SetProperty(ref _LimitedShops, value); }
        }

        ObservableCollection<CityPOI> _myTourism;
        public ObservableCollection<CityPOI> MyTourism
        {
            get { return _myTourism; }
            set { SetProperty(ref _myTourism, value); }
        }

        ObservableCollection<CityPOI> _LimitedTourism;
        public ObservableCollection<CityPOI> LimitedTourism
        {
            get { return _LimitedTourism; }
            set { SetProperty(ref _LimitedTourism, value); }
        }

        public IGetDataAsync _getData { get; private set; }
        public IPageDialogService _dialogService { get; private set; }
        public INavigationService _navigationService { get; }
        public DelegateCommand RefreshList { get; private set; }

        private async void RefreshHomeLists()
        {
            await GetData();
        }

        private async Task GetData()
        {
            MyShops = new ObservableCollection<CityPOI>();
            LimitedShops = new ObservableCollection<CityPOI>();
            MyEvents = new ObservableCollection<CityPOI>();
            LimitedEvents = new ObservableCollection<CityPOI>();
            MyRestaurants = new ObservableCollection<CityPOI>();
            LimitedRestaurants = new ObservableCollection<CityPOI>();
            MyTourism = new ObservableCollection<CityPOI>();
            LimitedTourism = new ObservableCollection<CityPOI>();

            try
            {
               
                IsBusy = true;

                var tempMyTourism = await _getData.GetPOIByTypeAsync("4");
                var tempMyShops = await _getData.GetPOIByTypeAsync("3");
                var tempMyRestaurants = await _getData.GetPOIByTypeAsync("2");
                var tempMyEvents = await _getData.GetPOIByTypeAsync("1");

                //HACK this is for testing only
                var tempLimitedTourism = await _getData.GetLimitedPOIByTypeAsync("4");
                var tempLimitedShops = await _getData.GetLimitedPOIByTypeAsync("3");
                var tempLimitedRestaurants = await _getData.GetLimitedPOIByTypeAsync("2");
                var tempLimitedEvents = await _getData.GetLimitedPOIByTypeAsync("1");

                if (_getData.Validate())
                {
                    MyShops = tempMyShops;
                    MyEvents = tempMyEvents;
                    MyRestaurants = tempMyRestaurants;
                    MyTourism = tempMyTourism;

                    LimitedTourism = tempLimitedTourism;
                    LimitedEvents = tempLimitedEvents;
                    LimitedShops = tempLimitedShops;
                    LimitedRestaurants = tempLimitedRestaurants;

                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Alert", "Opps!  There was an error retrieving your data.", "OK");
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!!!                   HomeViewModel:GetData " + ex.Message);
            }
            IsBusy = false;
        }
    }

}
