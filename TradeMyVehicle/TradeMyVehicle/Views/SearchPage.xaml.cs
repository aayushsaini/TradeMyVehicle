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
    public partial class SearchPage : ContentPage
    {
        public ObservableCollection<SearchVehicle> SearchVehiclesList;
        public SearchPage()
        {
            InitializeComponent();
            SearchVehiclesList = new ObservableCollection<SearchVehicle>();
        }

        private async void SearchBarVehicle_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vehiclesList = await ApiService.SearchVehicle(e.NewTextValue);
            //foreach (var vehicle in vehiclesList)
            //{
            //    SearchVehiclesList.Add(vehicle);
            //}
            ListView_Search.ItemsSource = vehiclesList;
        }

        private void ListView_Search_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedVehicle = e.SelectedItem as SearchVehicle;
            Navigation.PushModalAsync(new ItemDetailPage(selectedVehicle.id));
        }
    }
}