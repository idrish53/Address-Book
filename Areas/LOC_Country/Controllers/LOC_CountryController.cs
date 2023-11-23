using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using AddressBook1.DAL;
using AddressBook1.Areas.LOC_Country.Models;
using AddressBook1.BAL;

namespace AddressBook1.Areas.LOC_Country.Controllers
{
    [CheckAccess]
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        #region Configuration

        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_Country_SelectAll(str);
            return View("LOC_Countrylist", dt);           
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_Country_Delete(str, CountryID);
            return RedirectToAction("Index");

        }
        #endregion

        #region ADD
        public IActionResult ADD(int? CountryID)
        {
            if (CountryID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

                //prepare a connection
                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();

                //prepare a command
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_Country_SelectByPK";
                objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;


                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);
                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_Country.CountryName = dr["CountryName"].ToString();
                        modelLOC_Country.CountryCode = Convert.ToInt32(dr["CountryCode"]);
                        modelLOC_Country.PhotoPath = dr["PhotoPath"].ToString();
                        modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelLOC_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }
                    return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult save(LOC_CountryModel modelLOC_Country)
        {
            //upload of PhotoPath Here
            if (modelLOC_Country.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelLOC_Country.File.FileName);
                modelLOC_Country.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_Country.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelLOC_Country.File.CopyTo(stream);
                }
            }
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();

            if (modelLOC_Country.CountryID == null)
            {
                DataTable dt = dalLOC.PR_LOC_Country_Insert(str, modelLOC_Country);
                TempData["CountryInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalLOC.PR_LOC_Country_Update(str, modelLOC_Country);
                TempData["CountryInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            ////prepare a connection
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionstr);
            //conn.Open();

            ////prepare a command
            //SqlCommand objCmd = conn.CreateCommand();
            //objCmd.CommandType = CommandType.StoredProcedure;
            //if (modelLOC_Country.CountryID == null)
            //{
            //    objCmd.CommandText = "PR_LOC_Country_Insert";
            //    objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            //}
            //else
            //{
            //    objCmd.CommandText = "PR_LOC_Country_Update";
            //    objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;
            //}

            ////prepare a parameters
            //objCmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
            //objCmd.Parameters.Add("@CountryCode", SqlDbType.Int).Value = modelLOC_Country.CountryCode;
            //objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;
            //objCmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_Country.PhotoPath;

            //if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            //{
            //    if (modelLOC_Country.CountryID == null)
            //        TempData["CountryInsertMsg"] = "Record Inserted Successfully";
            //    else
            //        return RedirectToAction("Index");
            //}
            //conn.Close();

            //return RedirectToAction("Index");
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
            command.CommandText = "PR_LOC_Country_SelectPage";

            command.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CountryName"].ToString();
            command.Parameters.Add("@CountryCode", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CountryCode"].ToString();

            DataTable dt = new DataTable();
            SqlDataReader objsdr = command.ExecuteReader();
            dt.Load(objsdr);

            conn.Close();

            return View("LOC_Countrylist", dt);
        }
        #endregion

    }
}
    

