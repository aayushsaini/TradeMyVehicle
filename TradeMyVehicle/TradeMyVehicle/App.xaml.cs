using System;
using TradeMyVehicle.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradeMyVehicle
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SignupPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
