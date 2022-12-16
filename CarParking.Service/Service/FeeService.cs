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
            finally
            {
                _feeData.CloseConnection();
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
            finally
            {
                _feeData.CloseConnection();
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
            finally
            {
                _feeData.CloseConnection();
            }
        }

        public BaseResponse UpdateTransaction(UpdateTransactionRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (request == null || request.TransactionId <= 0 || request.HourDiscount < 0)
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }
                Transaction transaction = _feeData.GetTransactionByTranId(request.TransactionId);
                if (transaction == null || transaction.TransactionId == 0)
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "not found transaction";
                    return response;
                }
                DateTime startDate = transaction.StartDate;
                DateTime endDate = DateTime.Now;
                decimal hourDiscount = request.HourDiscount * 60;
             
                decimal differentMinute = CalculateDifferenctMinute(startDate,endDate);
                if(differentMinute < 0)
                {
                    differentMinute = 0;
                }
                int hour = (int)Math.Ceiling(differentMinute / 60);

                decimal fee = 0;
                ListFee listFee = _feeData.GetFee(transaction.ParkingId);
                if (listFee == null || listFee.ListFees == null || listFee.ListFees.Count <=0)
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "not found fee";
                    return response;
                }

                int existingHour = (int)Math.Ceiling(differentMinute / 60);
                foreach (var feeItem in listFee.ListFees)
                {
                    if(feeItem.Period == 0)
                    {
                        if(existingHour > 0)
                        {
                            fee += (existingHour * feeItem.Charge);
                        }
                        break;
                    }
                    if(feeItem.Period < existingHour)
                    {
                        fee += (feeItem.Period * feeItem.Charge);
                        existingHour -= feeItem.Period;
                    }
                    else
                    {
                        fee += (existingHour * feeItem.Charge);
                        break;
                    }

                }
                bool isAdd = _feeData.UpdateTransaction(endDate,request.HourDiscount,fee,hour,request.TransactionId);
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
            finally
            {
                _feeData.CloseConnection();
            }
        }

        private decimal CalculateDifferenctMinute(DateTime startDate,DateTime endDate)
        {
            decimal startDateMinute = (startDate.DayOfYear * 24 * 60) + (startDate.Hour * 60) + (startDate.Minute);
            decimal endDateMinute = (endDate.DayOfYear * 24 * 60) + (endDate.Hour * 60) + (endDate.Minute);
            return endDateMinute - startDateMinute;
        }
    }
}
