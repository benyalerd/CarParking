using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Service.IService
{
    public interface IFeeService
    {
        BaseResponse AddFee(InsertFeeRequest request);
        InsertTransactionResponse InsertTransaction(InsertTransactionRequest request);
        BaseResponse UpdateFee(InsertFeeRequest request);
        BaseResponse UpdateTransaction(UpdateTransactionRequest request);

    }
}
