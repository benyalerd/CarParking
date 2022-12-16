using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Response
{
    public class Transaction
    {
        [DataNames("END_START")]
        public DateTime EndDate { get; set; }
        [DataNames("HOUR_DISCOUNT")]
        public int HourDiscount { get; set; }
        [DataNames("FEE")]
        public decimal Fee { get; set; }
        [DataNames("HOURS")]
        public int Hours { get; set; }
        [DataNames("TRANSACTION_ID")]
        public int TransactionId { get; set; }
        [DataNames("LICENSE_PLATE")]
        public string LicensePlate { get; set; }
        [DataNames("PARKING_ID")]
        public int ParkingId { get; set; }
        [DataNames("START_DATE")]
        public DateTime StartDate { get; set; }
    }
}
