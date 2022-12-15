using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Request
{
    public class InsertTransactionRequest
    {
        public string LicensePlate { get; set; }
        public int ParkingId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
