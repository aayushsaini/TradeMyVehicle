using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMyVehicle.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradeMyVehicle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePhonePage : ContentPage
    {
        public ChangePhonePage()
        {
            InitializeComponent();
        }

        private async void BtnAddPhone_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            Loader.IsRunning = true;
            BtnAddPhone.IsEnabled = false;
            BtnAddPhone.IsVisible = false;
            var response = await ApiService.EditPhoneNumber(EntPhone.Text);
            if (!response)
            {
                await DisplayAlert("Oops", "Something went wrong", "Retry");
            }
            else 
            {
                await DisplayAlert("Phone Number Added", "You've successfully added the phone number", "Okay");
                Loader.IsVisible = false;
                Loader.IsRunning = false;
                BtnAddPhone.IsEnabled = true;
                BtnAddPhone.IsVisible = true;
                await Navigation.PopAsync();
            }
            Loader.IsVisible = false;
            Loader.IsRunning = false;
            BtnAddPhone.IsEnabled = true;
            BtnAddPhone.IsVisible = true;
        }
    }
}