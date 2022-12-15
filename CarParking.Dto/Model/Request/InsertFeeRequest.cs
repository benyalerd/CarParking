using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Request
{
    public class InsertFeeRequest
    {
       public List<FeeRequest> FeeLists { get; set; }

    }

    public class FeeRequest
    {
        public int Period { get; set; }
        public decimal Fee { get; set; }
        public int ParkingId { get; set; }
    }
}
