using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Request
{
    public class GetParkingRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
