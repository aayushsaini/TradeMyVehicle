using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMyVehicle.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradeMyVehicle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            Loader.IsRunning = true;
            BtnChangePassword.IsEnabled = false;
            BtnChangePassword.IsVisible = false;
            var response = await ApiService.ChangePassword(EntOldPassword.Text, EntNewPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Oops", "Something went wrong", "Try Again");
            }
            else
            {
                await DisplayAlert("Password Chaged", "You can now login with the new password", "Okay");
                Preferences.Set("AccessToken", string.Empty);
                Application.Current.MainPage = new NavigationPage(new SignupPage());
            }
            Loader.IsVisible = false;
            Loader.IsRunning = false;
            BtnChangePassword.IsEnabled = true;
            BtnChangePassword.IsVisible = true;
        }
    }
}