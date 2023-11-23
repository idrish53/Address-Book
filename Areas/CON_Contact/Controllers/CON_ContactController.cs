using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook1.Models;
using AddressBook1.DAL;
using AddressBook1.Areas.LOC_City.Models;
using AddressBook1.Areas.LOC_State.Models;
using AddressBook1.Areas.LOC_Country.Models;
using AddressBook1.Areas.CON_Contact.Models;
using AddressBook1.BAL;

namespace AddressBook1.Areas.CON_Contact.Controllers
{
    [CheckAccess]
    [Area("CON_Contact")]
    [Route("CON_Contact/[controller]/[action]")]
    public class CON_ContactController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public CON_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.PR_CON_Contact_SelectAll(str);
            return View("CON_Contactlist", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.PR_CON_Contact_Delete(str, ContactID);
            return RedirectToAction("Index");
        }
        #endregion

        #region ADD
        public IActionResult ADD(int? ContactID)
        {
            #region DropDownForCountry
            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            DataTable dt1 = new DataTable();
            SqlConnection conn1 = new SqlConnection(connectionstr1);
            conn1.Open();

            //prepare a command
            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_Country_SelectDropDown";
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);
            conn1.Close();
            List<Loc_CountryDropDownModle> list = new List<Loc_CountryDropDownModle>();
            foreach (DataRow dr in dt1.Rows)
            {
                Loc_CountryDropDownModle cddm = new Loc_CountryDropDownModle();
                cddm.CountryID = Convert.ToInt32(dr["CountryID"]);
                cddm.CountryName = dr["CountryName"].ToString();
                list.Add(cddm);
            }
            ViewBag.CountryList = list;
            #endregion

            #region DropDownForState
            string connectionstr2 = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            DataTable dt2 = new DataTable();
            SqlConnection conn2 = new SqlConnection(connectionstr2);
            conn2.Open();

            //prepare a command
            SqlCommand objCmd2 = conn2.CreateCommand();
            objCmd2.CommandType = CommandType.StoredProcedure;
            objCmd2.CommandText = "PR_LOC_State_SelectDropDown";
            SqlDataReader objSDR2 = objCmd2.ExecuteReader();
            dt2.Load(objSDR2);
            conn2.Close();
            List<Loc_StateDropDownModle> list1 = new List<Loc_StateDropDownModle>();
            foreach (DataRow dr in dt2.Rows)
            {
                Loc_StateDropDownModle cddm1 = new Loc_StateDropDownModle();
                cddm1.StateID = Convert.ToInt32(dr["StateID"]);
                cddm1.StateName = dr["StateName"].ToString();
                list1.Add(cddm1);
            }
            ViewBag.StateList = list1;
            #endregion

            #region DropDownForCity
            string connectionstr3 = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            DataTable dt3 = new DataTable();
            SqlConnection conn3 = new SqlConnection(connectionstr3);
            conn3.Open();

            //prepare a command
            SqlCommand objCmd3 = conn3.CreateCommand();
            objCmd3.CommandType = CommandType.StoredProcedure;
            objCmd3.CommandText = "PR_LOC_City_SelectDropDown";
            SqlDataReader objSDR3 = objCmd3.ExecuteReader();
            dt3.Load(objSDR3);
            conn3.Close();
            List<Loc_CityDropDownModle> list2 = new List<Loc_CityDropDownModle>();
            foreach (DataRow dr in dt3.Rows)
            {
                Loc_CityDropDownModle cddm2 = new Loc_CityDropDownModle();
                cddm2.CityID = Convert.ToInt32(dr["CityID"]);
                cddm2.CityName = dr["CityName"].ToString();
                list2.Add(cddm2);
            }
            ViewBag.CityList = list2;
            #endregion

            #region Record Select by PK
            if (ContactID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

                //prepare a connection
                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();

                //prepare a command
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "CON_Contact_SelectByPK";
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                CON_ContactModel modelCON_Contact = new CON_ContactModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelCON_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelCON_Contact.ContactName = dr["ContactName"].ToString();
                    modelCON_Contact.ContactAddress = dr["ContactAddress"].ToString();
                    modelCON_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelCON_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                    modelCON_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                    modelCON_Contact.PinCode = Convert.ToInt32(dr["PinCode"]);
                    modelCON_Contact.Mobile = dr["Mobile"].ToString();
                    modelCON_Contact.AlternateContact = dr["AlternateContact"].ToString();
                    modelCON_Contact.Email = dr["Email"].ToString();
                    modelCON_Contact.Birthdate = Convert.ToDateTime(dr["Birthdate"]);
                    modelCON_Contact.AnniversaryDate = Convert.ToDateTime(dr["AnniversaryDate"]);
                    modelCON_Contact.LinkedIn = dr["LinkedIn"].ToString();
                    modelCON_Contact.Twitter = dr["Twitter"].ToString();
                    modelCON_Contact.Insta = dr["Insta"].ToString();
                    modelCON_Contact.TypeOfProfesison = dr["TypeOfProfesison"].ToString();
                    modelCON_Contact.CompanyName = dr["CompanyName"].ToString();
                    modelCON_Contact.Designation = dr["Designation"].ToString();
                    modelCON_Contact.PhotoPath = dr["PhotoPath"].ToString();
                    modelCON_Contact.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelCON_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("CON_ContactAddEdit", modelCON_Contact);
            }
            #endregion

            return View("CON_ContactAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            //upload of PhotoPath Here
            if (modelCON_Contact.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelCON_Contact.File.FileName);
                modelCON_Contact.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelCON_Contact.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCON_Contact.File.CopyTo(stream);
                }
            }

            string str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalLOC = new CON_DAL();

            if (modelCON_Contact.ContactID == null)
            {
                DataTable dt = dalLOC.CON_Contact_Insert(str, modelCON_Contact);
                TempData["ContactInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalLOC.CON_Contact_Update(str, modelCON_Contact);
                TempData["ContactInsertMsg"] = "Record Updated Succesfully";
            }
            return RedirectToAction("Index");

            ////prepare a connection
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionstr);
            //conn.Open();

            ////prepare a command
            //SqlCommand objCmd = conn.CreateCommand();
            //objCmd.CommandType = CommandType.StoredProcedure;

            //if (modelCON_Contact.ContactID == null)
            //{
            //    objCmd.CommandText = "CON_Contact_Insert";
            //    objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            //}
            //else
            //{
            //    objCmd.CommandText = "CON_Contact_Update";
            //    objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = modelCON_Contact.ContactID;
            //}



            ////prepare a parameters
            //objCmd.Parameters.Add("@ContactName", SqlDbType.VarChar).Value = modelCON_Contact.ContactName;
            //objCmd.Parameters.Add("@ContactAddress", SqlDbType.NVarChar).Value = modelCON_Contact.ContactAddress;
            //objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCON_Contact.CountryID;
            //objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCON_Contact.StateID;
            //objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCON_Contact.CityID;
            //objCmd.Parameters.Add("@PinCode", SqlDbType.Int).Value = modelCON_Contact.PinCode;
            //objCmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = modelCON_Contact.Mobile;
            //objCmd.Parameters.Add("@AlternateContact", SqlDbType.NVarChar).Value = modelCON_Contact.AlternateContact;
            //objCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = modelCON_Contact.Email;
            //objCmd.Parameters.Add("@Birthdate", SqlDbType.Date).Value = modelCON_Contact.Birthdate;
            //objCmd.Parameters.Add("@AnniversaryDate", SqlDbType.Date).Value = modelCON_Contact.AnniversaryDate;
            //objCmd.Parameters.Add("@LinkedIn", SqlDbType.NVarChar).Value = modelCON_Contact.LinkedIn;
            //objCmd.Parameters.Add("@Twitter", SqlDbType.NVarChar).Value = modelCON_Contact.Twitter;
            //objCmd.Parameters.Add("@Insta", SqlDbType.NVarChar).Value = modelCON_Contact.Insta;
            //objCmd.Parameters.Add("@TypeOfProfesison", SqlDbType.NVarChar).Value = modelCON_Contact.TypeOfProfesison;
            //objCmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = modelCON_Contact.CompanyName;
            //objCmd.Parameters.Add("@Designation", SqlDbType.NVarChar).Value = modelCON_Contact.Designation;
            //objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;
            //objCmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelCON_Contact.PhotoPath;

            //if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            //{
            //    if (modelCON_Contact.ContactID == null)
            //        TempData["ContactInsertMsg"] = "Record Inserted Successfully";
            //    else
            //        return RedirectToAction("Index");
            //}
            //conn.Close();
        }
        #endregion

        #region DropDownByCountryID
        public IActionResult DropDownByCountry(int CountryID)
        {
            string connectionstr2 = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            DataTable dt2 = new DataTable();
            SqlConnection conn2 = new SqlConnection(connectionstr2);
            conn2.Open();

            //prepare a command
            SqlCommand objCmd2 = conn2.CreateCommand();
            objCmd2.CommandType = CommandType.StoredProcedure;
            objCmd2.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
            objCmd2.Parameters.AddWithValue("CountryID", CountryID);
            SqlDataReader objSDR2 = objCmd2.ExecuteReader();
            dt2.Load(objSDR2);
            conn2.Close();
            List<Loc_StateDropDownModle> list1 = new List<Loc_StateDropDownModle>();
            foreach (DataRow dr in dt2.Rows)
            {
                Loc_StateDropDownModle cddm1 = new Loc_StateDropDownModle();
                cddm1.StateID = Convert.ToInt32(dr["StateID"]);
                cddm1.StateName = dr["StateName"].ToString();
                list1.Add(cddm1);
            }
            var vModel = list1;
            return Json(vModel);
        }
        #endregion

        #region DropDownByStateID
        public IActionResult DropDownByState(int StateID)
        {
            string connectionstr3 = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            DataTable dt3 = new DataTable();
            SqlConnection conn3 = new SqlConnection(connectionstr3);
            conn3.Open();

            //prepare a command
            SqlCommand objCmd3 = conn3.CreateCommand();
            objCmd3.CommandType = CommandType.StoredProcedure;
            objCmd3.CommandText = "PR_LOC_City_SelectDropDownByStateID";
            objCmd3.Parameters.AddWithValue("StateID", StateID);
            SqlDataReader objSDR3 = objCmd3.ExecuteReader();
            dt3.Load(objSDR3);
            conn3.Close();
            List<Loc_CityDropDownModle> list2 = new List<Loc_CityDropDownModle>();
            foreach (DataRow dr in dt3.Rows)
            {
                Loc_CityDropDownModle cddm2 = new Loc_CityDropDownModle();
                cddm2.CityID = Convert.ToInt32(dr["CityID"]);
                cddm2.CityName = dr["CityName"].ToString();
                list2.Add(cddm2);
            }
            var vModel1 = list2;
            return Json(vModel1);
        }
        #endregion

        #region Search
        public IActionResult Search()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[CON_Contact_SelectPage]";

            command.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CountryName"].ToString();
            command.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["StateName"].ToString();
            command.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CityName"].ToString();
            command.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["ContactName"].ToString();
            command.Parameters.Add("@ContactAddress", SqlDbType.NVarChar).Value = HttpContext.Request.Form["ContactAddress"].ToString();
            command.Parameters.Add("@PinCode", SqlDbType.NVarChar).Value = HttpContext.Request.Form["PinCode"].ToString();

            DataTable dt = new DataTable();
            SqlDataReader objsdr = command.ExecuteReader();
            dt.Load(objsdr);

            conn.Close();

            return View("CON_Contactlist", dt);
        }
        #endregion

    }
}


