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
    public class EmployeeController : Controller
    {
        string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public ActionResult Index()
        {
            List<Employee3> employees = new List<Employee3>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select * from Employee3";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee3 emp = new Employee3
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        DesignationId = Convert.ToInt32(reader["DesignationId"])
                    };
                    employees.Add(emp);
                }
            }
            return View(employees);
        }
    }
}