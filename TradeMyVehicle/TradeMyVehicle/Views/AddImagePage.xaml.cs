using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AddImagePage : ContentPage
    {
        private MediaFile file;
        private int _vehicleId;
        public AddImagePage(int vehicleId)
        {
            InitializeComponent();
            _vehicleId = vehicleId;
        }

        private async void PickImageFromGallery(Image imageControl)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Oops", "Cannot complete this request, contact developers", "Okay");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { 
                CompressionQuality = 50,
                PhotoSize = PhotoSize.Large
            });

            if (file == null)
                return;

            //Added the source of Img 
            imageControl.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                AddImageToServer();
                return stream;
            });
        }

        private async void AddImageToServer()
        {
            var imageArray = FromFile.ToArray(file.GetStream());
            file.Dispose();

            var response = await ApiService.AddImage(_vehicleId, imageArray);

            if (response) return;
            await DisplayAlert("Something went wrong!", "Please try upload image again...", "Proceed");
        }

        private void TapImg1_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery(Img1);
        }

        private void TapImg2_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery(Img2);
        }

        private void TapImg3_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery(Img3);
        }

        private void TapImg4_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery(Img4);
        }

        private void TapImg5_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery(Img5);
        }

        private void TapImg6_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery(Img6);
        }

        private void BtnDone_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new MyAdsPage());
            //Navigation.RemovePage(this);
            DisplayAlert("Successfully Posted ✔", "Your vehicle's ad was successfully posted on the network", "Continue");
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}