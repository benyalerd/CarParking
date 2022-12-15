using CarParking.Data.IData;
using CarParking.Dto;
using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using CarParking.Dto.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CarParking.Data.Data
{
    public class FeeData : BaseData, IFeeData
    {
        private const string SP_ADD_FEE = "[SP_ADD_FEE]";
        private const string SP_DELETE_FEE_BY_PARKING_ID = "[SP_DELETE_FEE_BY_PARKING_ID]";
        private const string SP_GET_FEE_BY_PARKINGID = "[SP_GET_FEE_BY_PARKINGID]";
        private const string SP_UPDATE_TRANSACTION = "[SP_UPDATE_TRANSACTION]";
        private const string SP_ADD_TRANSACTION = "[SP_INSERT_TRANSACTION]";
        #region Constructor
        public FeeData(string connetionString) : base(connetionString)
        {
        }
        #endregion

        public bool AddFee(InsertFeeRequest request)
        {
            try
            {
                transaction = cnn.BeginTransaction();
                foreach (var item in request.FeeLists)
                {
                    ExecuteNonQuery(SP_ADD_FEE,
                    ("Period", ConvertDTA(item.Period)),
                    ("Fee", ConvertDTA(item.Fee)),
                    ("ParkingId", ConvertDTA(item.ParkingId)));
                }
                transaction.Commit();
                return true;
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public bool UpdateFee(InsertFeeRequest request)
        {
            try
            {
                transaction = cnn.BeginTransaction();
                int parkingId = request.FeeLists[0].ParkingId;
                ExecuteNonQuery(SP_DELETE_FEE_BY_PARKING_ID,
                ("ParkingId", ConvertDTA(parkingId)));
               
                foreach (var item in request.FeeLists)
                {
                    ExecuteNonQuery(SP_ADD_FEE,
                    ("Period", ConvertDTA(item.Period)),
                    ("Fee", ConvertDTA(item.Fee)),
                    ("ParkingId", ConvertDTA(item.ParkingId)));
                }
                transaction.Commit();

                return true;
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }


        public ListFee GetFee(GetFeeRequest request)
        {
            ListFee fee = new ListFee();
            DataNamesMapper<Fee> mapper = new DataNamesMapper<Fee>();
            try
            {
                DataSet dsResults = ExecuteDataSet(SP_GET_FEE_BY_PARKINGID,
                    ("ParkingId", ConvertDTA(request.ParkingId)));
                if ((dsResults != null) && (dsResults.Tables[0].Rows.Count > 0))
                {
                    if (dsResults.Tables[0].Rows[0][0] != System.DBNull.Value)
                    {
                        fee.ListFees.AddRange(mapper.Map(dsResults.Tables[0]));
                    }
                }
                return fee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public InsertTransactionResponse InsertTransaction(InsertTransactionRequest request)
        {
            InsertTransactionResponse response = new InsertTransactionResponse();
            try
            {
                DataSet dsResults = ExecuteDataSet(SP_ADD_TRANSACTION,
                    ("licensePlate", ConvertDTA(request.LicensePlate)),
                    ("startDate", ConvertDTA(request.StartDate)),
                    ("parkingId", ConvertDTA(request.ParkingId)));
                if ((dsResults != null) && (dsResults.Tables[0].Rows.Count > 0))
                {
                    if (dsResults.Tables[0].Rows[0][0] != System.DBNull.Value)
                    {                       
                        response.TransactionId = Convert.ToInt32(((dsResults.Tables[0].Rows[0][0] == Convert.DBNull) ? 0 : dsResults.Tables[0].Rows[0][0]));
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public bool UpdateTransaction(UpdateTransactionRequest request)
        {
            try
            {
                ExecuteNonQuery(SP_UPDATE_TRANSACTION,
                ("endDate", ConvertDTA(request.EndDate)),
                ("hourDiscount", ConvertDTA(request.HourDiscount)),
                ("fee", ConvertDTA(request.Fee)),
                ("hours", ConvertDTA(request.Hours)),
                 ("transactionId", ConvertDTA(request.TransactionId)));

                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
