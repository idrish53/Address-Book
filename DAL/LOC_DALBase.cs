using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using AddressBook1.Models;
using AddressBook1.Areas.LOC_Country.Models;
using AddressBook1.Areas.LOC_State.Models;
using AddressBook1.Areas.LOC_City.Models;
using AddressBook1.BAL;

namespace AddressBook1.DAL
{
    public class LOC_DALBase
    {
        #region SELECTALL

        #region PR_LOC_Country_SelectAll

        public DataTable PR_LOC_Country_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());


                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_LOC_State_SelectAll

        public DataTable PR_LOC_State_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_LOC_City_SelectAll

        public DataTable PR_LOC_City_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region Insert

        #region PR_LOC_Country_Insert

        public DataTable PR_LOC_Country_Insert(string conn, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, modelLOC_Country.CountryCode);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_Country.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_LOC_State_Insert

        public DataTable PR_LOC_State_Insert(string conn, LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Insert");

                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, modelLOC_State.StateCode);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_State.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_LOC_City_Insert
        public DataTable PR_LOC_City_Insert(string conn, LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Insert");

                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.NVarChar, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, modelLOC_City.CityCode);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_City.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region Delete

        #region PR_LOC_Country_Delete
        public DataTable PR_LOC_Country_Delete(string conn, int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Delete");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_LOC_State_Delete
        public DataTable PR_LOC_State_Delete(string conn, int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Delete");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_LOC_City_Delete
        public DataTable PR_LOC_City_Delete(string conn, int CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Delete");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #endregion

        #region UPDATE 

        #region PR_LOC_Country_Update

        public DataTable PR_LOC_Country_Update(string conn, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Update");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, modelLOC_Country.CountryCode);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_Country.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_LOC_State_Update
        public DataTable PR_LOC_State_Update(string conn, LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Update");

                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_State.StateID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, modelLOC_State.StateCode);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_State.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_LOC_City_Update
        public DataTable PR_LOC_City_Update(string conn, LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Update");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelLOC_City.CityID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, modelLOC_City.CityCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_City.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }


        #endregion

        #endregion

    }
}
