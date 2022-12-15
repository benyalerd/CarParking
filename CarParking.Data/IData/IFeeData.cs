using CarParking.Data.Data;
using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using CarParking.Dto.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Data.IData
{
    public interface IFeeData
    {
        bool AddFee(InsertFeeRequest request);
        bool UpdateFee(InsertFeeRequest request);
        ListFee GetFee(GetFeeRequest request);
        InsertTransactionResponse InsertTransaction(InsertTransactionRequest request);
        bool UpdateTransaction(UpdateTransactionRequest request);
    }
}
