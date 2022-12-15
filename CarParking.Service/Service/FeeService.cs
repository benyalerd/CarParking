using CarParking.Data.IData;
using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using CarParking.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Service.Service
{
    public class FeeService : IFeeService
    {
        private readonly IFeeData _feeData;
        public FeeService(IFeeData feeData)
        {
            _feeData = feeData;
        }
        public BaseResponse AddFee(InsertFeeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (request == null || request.FeeLists == null || request.FeeLists.Count <= 0)
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }

                bool isAdd = _feeData.AddFee(request);
                if (!isAdd)
                {
                    response.ErrorCode = "005";
                    response.ErrorMessage = "Failed to add or update parking";
                    return response;
                }

                response.IsSuccess = true;
                response.ErrorCode = "000";
                response.ErrorMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InsertTransactionResponse InsertTransaction(InsertTransactionRequest request)
        {
            InsertTransactionResponse response = new InsertTransactionResponse();
            try
            {
                if (request == null || string.IsNullOrEmpty(request.LicensePlate) || request.ParkingId == 0 || request.StartDate == null)
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }
                request.StartDate = DateTime.UtcNow;
                response  = _feeData.InsertTransaction(request);
                if (response.TransactionId == 0)
                {
                    response.ErrorCode = "005";
                    response.ErrorMessage = "Failed to add or update parking";
                    return response;
                }

                response.IsSuccess = true;
                response.ErrorCode = "000";
                response.ErrorMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResponse UpdateFee(InsertFeeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (request == null || request.FeeLists == null || request.FeeLists.Count <= 0)
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }

                bool isAdd = _feeData.UpdateFee(request);
                if (!isAdd)
                {
                    response.ErrorCode = "005";
                    response.ErrorMessage = "Failed to add or update parking";
                    return response;
                }

                response.IsSuccess = true;
                response.ErrorCode = "000";
                response.ErrorMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
