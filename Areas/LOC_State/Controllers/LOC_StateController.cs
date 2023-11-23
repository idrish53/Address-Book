using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using AddressBook1.DAL;
using AddressBook1.Areas.LOC_State.Models;
using AddressBook1.Areas.LOC_Country.Models;
using AddressBook1.BAL;
namespace AddressBook1.Areas.LOC_State.Controllers
{
    [CheckAccess]
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_State_SelectAll(str);
            return View("LOC_Statelist", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_State_Delete(str, StateID);
            return RedirectToAction("Index");
        }
        #endregion

        #region ADD
        public IActionResult ADD(int? StateID)
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

            #region Record Select by PK
            if (StateID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

                //prepare a connection
                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();

                //prepare a command
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_State_SelectByPK";
                objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                LOC_StateModel modelLOC_State = new LOC_StateModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = (dr["StateCode"]).ToString();
                    //modelLOC_State.CountryName = dr["CountryName"].ToString();
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.PhotoPath = dr["PhotoPath"].ToString();
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                }
                return View("LOC_StateAddEdit", modelLOC_State);
            }
            #endregion

            return View("LOC_StateAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult save(LOC_StateModel modelLOC_State)
        {
            //upload of PhotoPath Here
            if (modelLOC_State.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelLOC_State.File.FileName);
                modelLOC_State.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_State.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelLOC_State.File.CopyTo(stream);
                }
            }

            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();

            if (modelLOC_State.StateID == null)
            {
                DataTable dt = dalLOC.PR_LOC_State_Insert(str, modelLOC_State);
                TempData["StateInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalLOC.PR_LOC_State_Update(str, modelLOC_State);
                TempData["StateInsertMsg"] = "Record Updated Succesfully";
            }
            return RedirectToAction("Index");

            ////prepare a connection
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionstr);
            //conn.Open();

            ////prepare a command
            //SqlCommand objCmd = conn.CreateCommand();
            //objCmd.CommandType = CommandType.StoredProcedure;
            //if(modelLOC_State.StateID == null)
            //{
            //    objCmd.CommandText = "PR_LOC_State_Insert";
            //    objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            //}
            //else
            //{
            //    objCmd.CommandText = "PR_LOC_State_Update";
            //    objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_State.StateID;
            //}

            ////prepare a parameters
            //objCmd.Parameters.Add("@StateName", SqlDbType.VarChar).Value = modelLOC_State.StateName;
            //objCmd.Parameters.Add("@StateCode", SqlDbType.NVarChar).Value = modelLOC_State.StateCode;
            ////objCmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = modelLOC_State.CountryName;
            //objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_State.CountryID;
            //objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;    
            //objCmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_State.PhotoPath;

            //if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            //{
            //    if(modelLOC_State.StateID == null)
            //        TempData["StateInsertMsg"] = "Record Inserted Successfully";
            //    else
            //        return RedirectToAction("Index");
            //}
            //conn.Close();
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
            command.CommandText = "PR_LOC_State_SelectPage";

            command.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["CountryName"].ToString();
            command.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["StateName"].ToString();
            command.Parameters.Add("@StateCode", SqlDbType.NVarChar).Value = HttpContext.Request.Form["StateCode"].ToString();

            DataTable dt = new DataTable();
            SqlDataReader objsdr = command.ExecuteReader();
            dt.Load(objsdr);

            conn.Close();

            return View("LOC_Statelist", dt);
        }
        #endregion

    }
}

