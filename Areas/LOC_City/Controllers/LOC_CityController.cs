using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook1.Models;
using System.Collections.Generic;
using AddressBook1.DAL;
using AddressBook1.Areas.LOC_City.Models;
using AddressBook1.Areas.LOC_Country.Models;
using AddressBook1.Areas.LOC_State.Models;
using AddressBook1.BAL;
namespace AddressBook1.Areas.LOC_City.Controllers
{
    [CheckAccess]
   
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        #region Configuration

        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_City_SelectAll(str);
            return View("LOC_Citylist", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_City_Delete(str, CityID);
            return RedirectToAction("Index");
        }
        #endregion

        #region ADD
        public IActionResult ADD(int? CityID)   
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

            #region Record Select by PK
            if (CityID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

                //prepare a connection
                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();

                //prepare a command
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_City_SelectByPK";
                objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                LOC_CityModel modelLOC_City = new LOC_CityModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.CityCode = dr["CityCode"].ToString();
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.PhotoPath = dr["PhotoPath"].ToString();
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CityAddEdit", modelLOC_City);
            }
            #endregion

            return View("LOC_CityAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult save(LOC_CityModel modelLOC_City)
        {
            //upload of PhotoPath Here
            if (modelLOC_City.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelLOC_City.File.FileName);
                modelLOC_City.PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_City.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelLOC_City.File.CopyTo(stream);
                }
            }
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();

            if (modelLOC_City.CityID == null)
            {
                DataTable dt = dalLOC.PR_LOC_City_Insert(str, modelLOC_City);
                TempData["CityInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalLOC.PR_LOC_City_Update(str, modelLOC_City);
                TempData["CityInsertMsg"] = "Record Updated Succesfully";
            }
            return RedirectToAction("Index");

            ////prepare a connection
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionstr);
            //conn.Open();

            ////prepare a command
            //SqlCommand objCmd = conn.CreateCommand();
            //objCmd.CommandType = CommandType.StoredProcedure;

            //if (modelLOC_City.CityID == null)
            //{
            //    objCmd.CommandText = "PR_LOC_City_Insert";
            //    objCmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DBNull.Value;
            //}
            //else
            //{
            //    objCmd.CommandText = "PR_LOC_City_Update";
            //    objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;
            //}

            ////prepare a parameters
            //objCmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = modelLOC_City.CityName;
            //objCmd.Parameters.Add("@CityCode", SqlDbType.NVarChar).Value = modelLOC_City.CityCode;
            //objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
            //objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
            //objCmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = DBNull.Value;
            //objCmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_City.PhotoPath;

            //if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            //{
            //    if (modelLOC_City.CityID == null)
            //        TempData["CityInsertMsg"] = "Record Inserted Successfully";
            //    else
            //        return RedirectToAction("Index");
            //}
            //conn.Close();
        }
        #endregion

        #region DropDownByCountryid
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
            List<Loc_StateDropDownModle> list = new List<Loc_StateDropDownModle>();
            foreach (DataRow dr in dt2.Rows)
            {
                Loc_StateDropDownModle cddm1 = new Loc_StateDropDownModle();
                cddm1.StateID = Convert.ToInt32(dr["StateID"]);
                cddm1.StateName = dr["StateName"].ToString();
                list.Add(cddm1);
            }
            var vModel= list;
            return Json(vModel);
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
            command.CommandText = "[PR_LOC_City_SelectPage]";

            command.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CountryName"].ToString();
            command.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["StateName"].ToString();
            command.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CityName"].ToString();
            command.Parameters.Add("@CityCode", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CityCode"].ToString();

            DataTable dt = new DataTable();
            SqlDataReader objsdr = command.ExecuteReader();
            dt.Load(objsdr);

            conn.Close();

            return View("LOC_Citylist", dt);
        }
        #endregion

    }
}
