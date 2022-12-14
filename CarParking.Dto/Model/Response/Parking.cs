using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Response
{
    public class Parking : BaseResponse
    {
        [DataNames("PARKING_ID")]
        public int ParkingId { get; set; }
        [DataNames("PARKING_NAME")]
        public string ParkingName { get; set; }
        [DataNames("PARKING_ADDRESS")]
        public string ParkingAddress { get; set; }
        [DataNames("EMAIL")]
        public string Email { get; set; }
        [DataNames("PASSWORD")]
        public string Password { get; set; }
    }
}
