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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            BtnLogin.IsEnabled = false;
            BtnLogin.IsVisible = false;
            Loader.IsVisible = true;
            Loader.IsRunning = true;
            //BtnLogin.BackgroundColor = Color.Silver;
            //BtnLogin.Text = "Loading...";
            var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
            if (response)
            {
                await DisplayAlert("Success", "You've successfully logged in", "Proceed");
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Try again");
            }
            Loader.IsVisible = false;
            Loader.IsRunning = false;
            BtnLogin.IsEnabled = true;
            BtnLogin.IsVisible = true;
            //BtnLogin.BackgroundColor = Color.FromHex("#303F9F");
            //BtnLogin.Text = "Sign into my account";
        }
    }
}