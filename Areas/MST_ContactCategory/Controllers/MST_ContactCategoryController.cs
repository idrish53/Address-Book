using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook1.Models;
using AddressBook1.DAL;
using AddressBook1.Areas.MST_ContactCategory.Models;
using AddressBook1.BAL;

namespace AddressBook1.Areas.MST_ContactCategory.Controllers
{
    [CheckAccess]

    [Area("MST_ContactCategory")]
    [Route("MST_ContactCategory/[controller]/[action]")]
    public class MST_ContactCategoryController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalCON= new CON_DAL();
            DataTable dt = dalCON.PR_MST_ContactCategory_SelectAll(str);
            return View("MST_ContactCategorylist", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.PR_MST_ContactCategory_Delete(str , ContactCategoryID);
            return RedirectToAction("Index");
        }
        #endregion

        #region ADD
        public IActionResult ADD(int? ContactCategoryID)
        {
            #region Record Select by PK
            if (ContactCategoryID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

                //prepare a connection
                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();

                //prepare a command
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "MST_ContactCategory_SelectByPK";
                objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                    modelMST_ContactCategory.ContactCategoryRelation = dr["ContactCategoryRelation"].ToString();
                    modelMST_ContactCategory.PhotoPath = dr["PhotoPath"].ToString();
                    modelMST_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelMST_ContactCategory.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
            }
            #endregion

            return View("MST_ContactCategoryAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            //upload of PhotoPath Here
            if (modelMST_ContactCategory.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelMST_ContactCategory.File.FileName);
                modelMST_ContactCategory.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_ContactCategory.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMST_ContactCategory.File.CopyTo(stream);
                }
            }

            string str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalLOC = new CON_DAL();

            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                DataTable dt = dalLOC.MST_ContactCategory_Insert(str, modelMST_ContactCategory);
                TempData["MST_ContactCatogaryInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalLOC.MST_ContactCategory_Update(str, modelMST_ContactCategory);
                TempData["MST_ContactCatogaryInsertMsg"] = "Record Updated Succesfully";
            }
            return RedirectToAction("Index");


            ////prepare a connection
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionstr);
            //conn.Open();

            ////prepare a command
            //SqlCommand objCmd = conn.CreateCommand();
            //objCmd.CommandType = CommandType.StoredProcedure;

            //if (modelMST_ContactCategory.ContactCategoryID == null)
            //{
            //    objCmd.CommandText = "MST_ContactCategory_Insert";
            //    objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            //}
            //else
            //{
            //    objCmd.CommandText = "MST_ContactCategory_Update";
            //    objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelMST_ContactCategory.ContactCategoryID;
            //}

            ////prepare a parameters
            //objCmd.Parameters.Add("@ContactCategoryName", SqlDbType.VarChar).Value = modelMST_ContactCategory.ContactCategoryName;
            //objCmd.Parameters.Add("@ContactCategoryRelation", SqlDbType.NVarChar).Value = modelMST_ContactCategory.ContactCategoryRelation;
            //objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;
            //objCmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelMST_ContactCategory.PhotoPath;
            //if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            //{
            //    if (modelMST_ContactCategory.ContactCategoryID == null)
            //        TempData["ContactCategoryInsertMsg"] = "Record Inserted Successfully";
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
            command.CommandText = "MST_ContactCategory_SelectPage";

            command.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = HttpContext.Request.Form["ContactCategoryName"].ToString();
            command.Parameters.Add("@ContactCategoryRelation", SqlDbType.NVarChar).Value = HttpContext.Request.Form["ContactCategoryRelation"].ToString();

            DataTable dt = new DataTable();
            SqlDataReader objsdr = command.ExecuteReader();
            dt.Load(objsdr);

            conn.Close();

            return View("MST_ContactCategorylist", dt);
        }
        #endregion

    }
}
