using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using AddressBook1.Controllers;
using AddressBook1.Models;
using AddressBook1.Areas.CON_Contact.Models;
using AddressBook1.Areas.MST_ContactCategory.Models;
using AddressBook1.BAL;

namespace AddressBook1.DAL
{
    public class CON_DALBase
    {
        #region Select All

        #region PR_MST_ContactCategory_SelectAll
        public DataTable PR_MST_ContactCategory_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectAll");
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

        #region PR_CON_Contact_SelectAll
        public DataTable PR_CON_Contact_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_SelectAll");
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

        #region MST_ContactCategory_Insert

        public DataTable MST_ContactCategory_Insert(string conn, MST_ContactCategoryModel modelMST_ContactCategory)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("MST_ContactCategory_Insert");

                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryRelation", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryRelation);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelMST_ContactCategory.PhotoPath);
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

        #region CON_Contact_Insert
        public DataTable CON_Contact_Insert(string conn, CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("CON_Contact_Insert");

                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.NVarChar, modelCON_Contact.ContactName);
                sqlDB.AddInParameter(dbCMD, "ContactAddress", SqlDbType.NVarChar, modelCON_Contact.ContactAddress);
                sqlDB.AddInParameter(dbCMD, "PinCode", SqlDbType.Int, modelCON_Contact.PinCode);
                sqlDB.AddInParameter(dbCMD, "Mobile", SqlDbType.NVarChar, modelCON_Contact.Mobile);
                sqlDB.AddInParameter(dbCMD, "AlternateContact", SqlDbType.NVarChar, modelCON_Contact.AlternateContact);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "Birthdate", SqlDbType.Date, modelCON_Contact.Birthdate);
                sqlDB.AddInParameter(dbCMD, "AnniversaryDate", SqlDbType.Date, modelCON_Contact.AnniversaryDate);
                sqlDB.AddInParameter(dbCMD, "LinkedIn", SqlDbType.NVarChar, modelCON_Contact.LinkedIn);
                sqlDB.AddInParameter(dbCMD, "Twitter", SqlDbType.NVarChar, modelCON_Contact.Twitter);
                sqlDB.AddInParameter(dbCMD, "Insta", SqlDbType.NVarChar, modelCON_Contact.Insta);
                sqlDB.AddInParameter(dbCMD, "TypeOfProfesison", SqlDbType.NVarChar, modelCON_Contact.TypeOfProfesison);
                sqlDB.AddInParameter(dbCMD, "CompanyName", SqlDbType.NVarChar, modelCON_Contact.CompanyName);
                sqlDB.AddInParameter(dbCMD, "Designation", SqlDbType.NVarChar, modelCON_Contact.Designation);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelCON_Contact.PhotoPath);
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

        #region DELETE

        #region PR_CON_Contact_Delete

        public DataTable PR_CON_Contact_Delete(string conn, int ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_Delete");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);
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

        #region PR_MST_ContactCategory_Delete

        public DataTable PR_MST_ContactCategory_Delete(string conn, int ContactCategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_Delete");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);
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

        #region CON_Contact_Update
        public DataTable CON_Contact_Update(string conn, CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("CON_Contact_Update");

                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, modelCON_Contact.ContactID);
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.NVarChar, modelCON_Contact.ContactName);
                sqlDB.AddInParameter(dbCMD, "ContactAddress", SqlDbType.NVarChar, modelCON_Contact.ContactAddress);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "PinCode", SqlDbType.Int, modelCON_Contact.PinCode);
                sqlDB.AddInParameter(dbCMD, "Mobile", SqlDbType.NVarChar, modelCON_Contact.Mobile);
                sqlDB.AddInParameter(dbCMD, "AlternateContact", SqlDbType.NVarChar, modelCON_Contact.AlternateContact);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "Birthdate", SqlDbType.Date, modelCON_Contact.Birthdate);
                sqlDB.AddInParameter(dbCMD, "AnniversaryDate", SqlDbType.Date, modelCON_Contact.AnniversaryDate);
                sqlDB.AddInParameter(dbCMD, "LinkedIn", SqlDbType.NVarChar, modelCON_Contact.LinkedIn);
                sqlDB.AddInParameter(dbCMD, "Twitter", SqlDbType.NVarChar, modelCON_Contact.Twitter);
                sqlDB.AddInParameter(dbCMD, "Insta", SqlDbType.NVarChar, modelCON_Contact.Insta);
                sqlDB.AddInParameter(dbCMD, "TypeOfProfesison", SqlDbType.NVarChar, modelCON_Contact.TypeOfProfesison);
                sqlDB.AddInParameter(dbCMD, "CompanyName", SqlDbType.NVarChar, modelCON_Contact.CompanyName);
                sqlDB.AddInParameter(dbCMD, "Designation", SqlDbType.NVarChar, modelCON_Contact.Designation);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelCON_Contact.PhotoPath);
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

        #region MST_ContactCategory_Update
        public DataTable MST_ContactCategory_Update(string conn, MST_ContactCategoryModel modelMST_ContactCatogary)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("MST_ContactCategory_Update");

                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.NVarChar, modelMST_ContactCatogary.ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCatogary.ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryRelation", SqlDbType.NVarChar, modelMST_ContactCatogary.ContactCategoryRelation);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelMST_ContactCatogary.PhotoPath);
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
