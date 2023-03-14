using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMiniProject.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;




namespace MVCMiniProject.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Content"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index", "Admin");
        }
           
        [HttpGet]
        public ActionResult Create()
        {
            GetCountry();
            Grid();
            return View();
        }
        [HttpPost]
        public ActionResult Create(User obj)
        {
            BALUser objB = new BALUser();
            objB.Register(obj.Name,obj.EmailId,obj.CityId,obj.Password);
            return RedirectToAction("Create");
        }
 
        public JsonResult GetCountry()
        {
            BALUser objBal = new BALUser();
            DataSet ds = new DataSet();
            ds = objBal.GetCountry();
            List<SelectListItem> CountryList = new List<SelectListItem>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                CountryList.Add(new SelectListItem
                { Text = dr["CountryName"].ToString(), 
                  Value = dr["CountryId"].ToString() });
            }
            ViewBag.Country = CountryList;
            return Json(CountryList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetState(int CountryId)
        {
            BALUser objBal = new BALUser();
            DataSet ds = new DataSet();
            ds = objBal.GetState(CountryId);
            List<SelectListItem> StateList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                StateList.Add(new SelectListItem
                {
                    Text = dr["StateName"].ToString(),
                    Value = dr["StateId"].ToString()
                });
            }
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCity(int StateId)
        {
            BALUser objBal = new BALUser();
            DataSet ds = new DataSet();
            ds = objBal.GetCity(StateId);
            List<SelectListItem> CityList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CityList.Add(new SelectListItem
                {
                    Text = dr["CityName"].ToString(),
                    Value = dr["CityId"].ToString()
                });
            }
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Grid()
        {
            BALUser ObjBal = new BALUser();
            DataSet ds = new DataSet();
            ds = ObjBal.GetDetails();
            User objDetails = new User();
            List<User> lstUserDt1 = new List<User>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                User obju = new User();
                obju.RegisterId = Convert.ToInt32(ds.Tables[0].Rows[i]["RegistrationId"].ToString());
                obju.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                obju.EmailId = ds.Tables[0].Rows[i]["EmailId"].ToString();
                obju.CountryName = ds.Tables[0].Rows[i]["CityName"].ToString();
                obju.StateName = ds.Tables[0].Rows[i]["StateName"].ToString();
                obju.CityName = ds.Tables[0].Rows[i]["CountryName"].ToString();
                obju.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                lstUserDt1.Add(obju);
            }
            objDetails.LstUser = lstUserDt1;
            return View(objDetails);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            BALUser objB = new BALUser();
            objB.Delete(Id);
            Response.Write("<script>alert ('Your Details has been Delete');</script>");
            return RedirectToAction("Create");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            GetCountry();
            User obj = new User();
            obj.RegisterId = Id;
            BALUser objB = new BALUser();
            SqlDataReader dr;
            dr = objB.Fetch(obj.RegisterId);
            while (dr.Read())
            {
                obj.RegisterId = Convert.ToInt32(dr["RegistrationId"].ToString());
                obj.Name = (dr["Name"].ToString());
                obj.EmailId = (dr["EmailId"].ToString());
                obj.CountryId = Convert.ToInt32(dr["CountryId"].ToString());
                obj.CountryName = (dr["CountryName"].ToString());
                obj.StateId = Convert.ToInt32(dr["StateId"].ToString());
                obj.StateName = (dr["StateName"].ToString());
                obj.CityId = Convert.ToInt32(dr["CityId"].ToString());
                obj.CityName = (dr["CityName"].ToString());
                obj.Password = (dr["Password"].ToString());
            }
            ViewBag.StateId = obj.StateId;
            ViewBag.StateName = obj.StateName;
            ViewBag.CityId = obj.CityId;
            ViewBag.CityName = obj.CityName;
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(User obj)
        {
            GetCountry();
            BALUser objB = new BALUser();
            objB.Edit(obj.Name,obj.EmailId,obj.CityId,obj.Password,obj.RegisterId);
            Response.Write("<script>alert ('Your Details has been Updated');</script>");
            return RedirectToAction("Create");
        }
        public ActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            GetCountry();
            User obj = new User();
            obj.RegisterId = Id;
            BALUser objB = new BALUser();
            SqlDataReader dr;
            dr = objB.Fetch(obj.RegisterId);
            while (dr.Read())
            {
                obj.RegisterId = Convert.ToInt32(dr["RegistrationId"].ToString());
                obj.Name = (dr["Name"].ToString());
                obj.EmailId = (dr["EmailId"].ToString());
              //  obj.CountryId = Convert.ToInt32(dr["CountryId"].ToString());
                obj.CountryName = (dr["CountryName"].ToString());
             //   obj.StateId = Convert.ToInt32(dr["StateId"].ToString());
                obj.StateName = (dr["StateName"].ToString());
             //   obj.CityId = Convert.ToInt32(dr["CityId"].ToString());
                obj.CityName = (dr["CityName"].ToString());
                obj.Password = (dr["Password"].ToString());
            }
            //ViewBag.StateId = obj.StateId;
            //ViewBag.StateName = obj.StateName;
            //ViewBag.CityId = obj.CityId;
            //ViewBag.CityName = obj.CityName;
            return PartialView(obj);
        }
    }
}