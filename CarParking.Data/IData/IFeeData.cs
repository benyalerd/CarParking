using CarParking.Data.Data;
using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Data.IData
{
    public interface IFeeData
    {
        void CloseConnection();
        bool AddFee(InsertFeeRequest request);
        bool UpdateFee(InsertFeeRequest request);
        ListFee GetFee(int parkingId);
        InsertTransactionResponse InsertTransaction(InsertTransactionRequest request);
        bool UpdateTransaction(DateTime endDate, int hourDiscount, decimal fee, int hours, int transactionId);
        Transaction GetTransactionByTranId(int transId);
    }
}
