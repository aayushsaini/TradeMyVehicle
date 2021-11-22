using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMyVehicle.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Image = TradeMyVehicle.Models.Image;

namespace TradeMyVehicle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        public ObservableCollection<Image> vehicleImages;
        private int totalImages = 0;

        //Contact details
        private string contactNumber;
        private string email;

        public ItemDetailPage(int id)
        {
            InitializeComponent();
            vehicleImages = new ObservableCollection<Image>();
            GetVehicleDetails(id);
        }

        private async void GetVehicleDetails(int vehicleId)
        {
            var vehicle = await ApiService.GetVehicleDetail(vehicleId);
            LblTitle.Text = vehicle.title;
            LblLocation.Text = vehicle.location;
            LblCondition.Text = vehicle.condition;
            LblPrice.Text = "₹" + vehicle.price.ToString();
            LblCompany.Text = vehicle.company;
            LblDescription.Text = vehicle.description;
            LblColor.Text = vehicle.color;
            LblModelNo.Text = vehicle.model;
            LblEngine.Text = vehicle.engine;

            ImgUser.Source = vehicle.fullImageUrl;
            var images = vehicle.images;
            totalImages = vehicle.images.Count;

            contactNumber = vehicle.phone;
            email = vehicle.email;

            foreach(var image in images)
            {
                vehicleImages.Add(image);
            }

            CarouselView_Images.ItemsSource = vehicleImages;
        }

        private void CarouselView_Images_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.FirstVisibleItemIndex <= totalImages)
            {
                var count = e.FirstVisibleItemIndex + 1;
                LblCount.Text = count + "/" + totalImages;
            }
        }

        private void BtnEmail_Clicked(object sender, EventArgs e)
        {
            var emailMessage = new EmailMessage("Interested buyer for your vehicle", "Hi! I want to know more about the vehicle you posted on TradeMyCar", email);
            Email.ComposeAsync(emailMessage);
        }

        private void BtnSms_Clicked(object sender, EventArgs e)
        {
            var sms = new SmsMessage("Hi! I want to know more about the vehicle you posted on TradeMyCar", contactNumber);
            Sms.ComposeAsync(sms);
        }

        private void BtnCall_Clicked(object sender, EventArgs e)
        {
            PhoneDialer.Open(contactNumber);
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}