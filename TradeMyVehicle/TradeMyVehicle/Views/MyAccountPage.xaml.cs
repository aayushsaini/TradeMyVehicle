using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMyVehicle.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImageToArray;
using Plugin.Media.Abstractions;

namespace TradeMyVehicle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        private MediaFile file;
        public MyAccountPage()
        {
            InitializeComponent();
        }

        private void TapUploadImage_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery();
        }

        private async void PickImageFromGallery()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Oops", "Cannot complete this request, contact developers", "Okay");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            //Added the source of Img 
            ImgProfile.Source = ImageSource.FromStream(() =>
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

            var response = await ApiService.EditUserProfile(imageArray);

            if (response) return;
            await DisplayAlert("Something went wrong!", "Please try upload image again...", "Proceed");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var profileImg = await ApiService.GetUserProfileImage();
            if (string.IsNullOrEmpty(profileImg.imageUrl))
            {
                ImgProfile.Source = "userPlaceholder.png";
            }
            else
            {
                ImgProfile.Source = profileImg.FullImagePath;
            }
        }
    }
}