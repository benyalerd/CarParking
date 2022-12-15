using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Request
{
    public class UpdateTransactionRequest
    {
        public DateTime EndDate { get; set; }
        public int HourDiscount { get; set; }
        public decimal Fee { get; set; }
        public int Hours { get; set; }
        public int TransactionId { get; set; }
    }
}
