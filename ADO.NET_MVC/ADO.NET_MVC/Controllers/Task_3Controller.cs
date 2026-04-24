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
    public class Task_3Controller : Controller
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

        public ActionResult Insert()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "insert into Designation (Designation) values ('Software Engineer'), ('Sr. Software Engineer'), ('Trainee'), ('Manager');" +
                    "insert into Employee3 (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId) values " +
                    "('Shivang', 'Manojbhai', 'Chauhan', '2003-10-08', '9313193998', 'Subhanpura, Vadodara', 45000, 1)," +
                    "('Bhautik', 'S', 'Ranpara', '2004-10-18', '9313193998', 'Subhanpura, Rajkot', 50000, 2)," +
                    "('Meet', null, 'Rajpal', '2005-10-28', '9313193998', 'Subhanpura, Rajkot', 45000, 3)," +
                    "('Sujal', 'S', 'Myatra', '1997-10-11', '9313193998', 'Subhanpura, Katch', 45000, 4);";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Inserted Some Records in Both Tables");
        }

        public ActionResult RecordByDesignation()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Inserted Some Records in Both Tables");
        }
    }
}