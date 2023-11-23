﻿using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Net.Mail;
using AddressBook1.BAL;

namespace AddressBook1.DAL
{
    public class SEC_DALBase
    {
        #region Method: dbo_PR_SEC_User_SelectByPK
        public DataTable dbo_PR_SEC_User_SelectByPK(string ConnStr, int? UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SEC_User_SelectByPK");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_SEC_User_SelectByPK
        public DataTable dbo_PR_SEC_User_SelectByUserNamePassword(string ConnStr, string UserName,string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SEC_User_SelectByUserNamePassword");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_SEC_User_Insert
        public decimal? dbo_PR_SEC_User_Insert(string ConnStr,string UserName, string Password, string FirstName, string LastName, string EmailAddress, string PhotoPath,DateTime? Created,DateTime? Modified)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SEC_User_Insert");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);
                sqlDB.AddInParameter(dbCMD, "FirstName", SqlDbType.VarChar, FirstName);
                sqlDB.AddInParameter(dbCMD, "LastName", SqlDbType.VarChar, LastName);
                sqlDB.AddInParameter(dbCMD, "EmailAddress", SqlDbType.VarChar, EmailAddress);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, Created);
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, Modified);

                var vResult = sqlDB.ExecuteScalar(dbCMD);
                if (vResult == null)
                    return null;

                return (decimal)Convert.ChangeType(vResult, vResult.GetType());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
