using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TradeMyVehicle
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("Username", UserName.Text);
        }

        private void GetBtn_Clicked(object sender, EventArgs e)
        {
            name.Text = Preferences.Get("Username", "");
        }
    }
}
