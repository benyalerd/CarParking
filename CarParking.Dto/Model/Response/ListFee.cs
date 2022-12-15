using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Response
{
    public class ListFee : BaseResponse
    {
        public List<Fee> ListFees { get; set; }
    }

    public class Fee
    {
        [DataNames("ID")]
        public int Id { get; set; }
        [DataNames("PERIOD")]
        public int Period { get; set; }
        [DataNames("FEE")]
        public decimal Charge { get; set; }
        [DataNames("PARKING_ID")]
        public int ParkingId { get; set; }
    }
}
