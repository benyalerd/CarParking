using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Request
{
    public class InsertParkingRequest
    {
        public string ParkingName { get; set; }
        public string ParkingAddress { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
