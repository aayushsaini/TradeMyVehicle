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
    public partial class ItemsListPage : ContentPage
    {
        public ObservableCollection<VehicleByCategory> VehiclesList;
        ViewCell lastCell;
        string category;
        public ItemsListPage(int categoryId)
        {
            InitializeComponent();
            VehiclesList = new ObservableCollection<VehicleByCategory>();
            GetVehicle(categoryId);

            if (categoryId == 1) category = "Bikes";
            if (categoryId == 2) category = "Cars";
            if (categoryId == 3) category = "Trucks";
            CategoryTitle.Text = category;
        }

        private async void GetVehicle(int categoryId)
        {
            var vehicles = await ApiService.GetVehicleByCategory(categoryId);
            foreach (var vehicle in vehicles)
            {
                VehiclesList.Add(vehicle);
            }
            ListView_Vehicles.ItemsSource = VehiclesList;
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
            var selectedItem = e.SelectedItem as VehicleByCategory;
            Navigation.PushModalAsync(new ItemDetailPage(selectedItem.id));
        }
    }
}