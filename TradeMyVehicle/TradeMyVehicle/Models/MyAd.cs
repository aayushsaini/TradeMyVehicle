using System;
using System.Collections.Generic;
using System.Text;

namespace TradeMyVehicle.Models
{
    public class MyAd
    {
        public int id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public string model { get; set; }
        public string location { get; set; }
        public DateTime datePosted { get; set; }
        public bool isFeatured { get; set; }
        public string imageUrl { get; set; }
        public string isFeaturedAd => isFeatured ? "Featured" : "Free";
        public string adPostedDate => datePosted.ToString("y");
        public string fullImageUrl => $"http://trademycar.azurewebsites.net/{imageUrl}";
    }
}
