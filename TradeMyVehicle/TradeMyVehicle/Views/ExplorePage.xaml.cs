using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMyVehicle.Models;
using TradeMyVehicle.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradeMyVehicle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorePage : ContentPage
    {
        //ObservableCollection is the list of Dynamic Data where,
        //whenever an item is added/removed then the whole list is refreshed
        public ObservableCollection<HotAndNewAd> VehiclesData;
        public ExplorePage()
        {
            InitializeComponent();
            VehiclesData = new ObservableCollection<HotAndNewAd>();
            GetHotAndNewVehicles();
        }

        private async void GetHotAndNewVehicles()
        {
            var vehicles = await ApiService.GetHotAndNewAds();
            foreach (var vehicle in vehicles)
            {
                VehiclesData.Add(vehicle);
            }
            CollectionView_Vehicles.ItemsSource = VehiclesData;
        }

        private void CollectionView_Vehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as HotAndNewAd;
            Navigation.PushModalAsync(new ItemDetailPage(currentSelection.id));
        }

        private void TapBike_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ItemsListPage(1));
        }

        private void TapCar_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ItemsListPage(2));
        }

        private void TapTruck_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ItemsListPage(3));
        }
    }
}