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
    public partial class MyAdsPage : ContentPage
    {
        public ObservableCollection<MyAd> MyAdsList;
        ViewCell lastCell;
        public MyAdsPage()
        {
            InitializeComponent();
            MyAdsList = new ObservableCollection<MyAd>();
            GetVehicle();
        }

        private async void GetVehicle()
        {
            var vehicles = await ApiService.GetMyAds();
            foreach (var vehicle in vehicles)
            {
                MyAdsList.Add(vehicle);
            }
            ListView_Vehicles.ItemsSource = MyAdsList;
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            //UI Fix
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#f9f9f9");
                lastCell = viewCell;
            }
        }

        private void ListView_Vehicles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as MyAd;
            Navigation.PushModalAsync(new ItemDetailPage(selectedItem.id));
        }

        private void ViewCell_Tapped_1(object sender, EventArgs e)
        {
            //UI Fix
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#f9f9f9");
                lastCell = viewCell;
            }
        }
    }
}