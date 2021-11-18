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
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            Loader.IsRunning = true;
            BtnSignUp.IsEnabled = false;
            BtnSignUp.IsVisible = false;
            //BtnSignUp.Text = "Loading...";
            //BtnLogin.BackgroundColor = Color.Silver;
            //BtnSignUp.TextColor = Color.White; 
            var response = await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
            if (response)
            {
                await DisplayAlert("Congrats! 🎉", "Your account has been created successfully!", "Continue");
                await Navigation.PushModalAsync(new LoginPage());
            } 
            else
            {
                await DisplayAlert("Oops!", "Something went wrong...", "Dismiss");
            }
            Loader.IsVisible = false;
            Loader.IsRunning = false;
            BtnSignUp.IsEnabled = true;
            BtnSignUp.IsVisible = true;
            //BtnLogin.BackgroundColor = Color.FromHex("#303F9F");
            //BtnSignUp.Text = "Continue";
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage() { BackgroundColor = Color.FromHex("#fcfdff") });
        }
    }
}