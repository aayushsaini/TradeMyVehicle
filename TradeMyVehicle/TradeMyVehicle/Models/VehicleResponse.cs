﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TradeMyVehicle.Models
{
    public class VehicleResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int vehicleId { get; set; }
    }
}
