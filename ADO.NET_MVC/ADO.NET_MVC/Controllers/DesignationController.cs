using ADO.NET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADO.NET_MVC.Controllers
{
    public class DesignationController : Controller
    {
        string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public ActionResult Index()
        {
            List<Designation> designations = new List<Designation>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select * from Designation";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Designation des = new Designation
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DesignationName = reader["Designation"].ToString()
                    };
                    designations.Add(des);
                }
            }
            return View(designations);
        }
    }
}