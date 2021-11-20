﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TradeMyVehicle.Models
{
    public class UserImageModel
    {
        public string imageUrl { get; set; }
        public string FullImagePath => $"https://trademycar.azurewebsites.net/{imageUrl}";
    }
}
