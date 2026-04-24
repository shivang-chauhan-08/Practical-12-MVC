using ADO.NET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ADO.NET_MVC.Controllers
{
    public class Task_1Controller : Controller
    {
        string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select * from Employee";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Address = reader["Address"].ToString()
                    };
                    employees.Add(emp);
                }
            }
            return View(employees);
        }

        public ActionResult Insert()
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "insert into Employee (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) values ('Shivang', 'Manojbhai', 'Chauhan', '2003-10-08', '9313193998', 'Subhanpura, Vadodara')";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Inserted");
        }

        public ActionResult InsertMore()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "insert into Employee (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) values " +
                    "('Shivang', 'Manojbhai', 'Chauhan', '2003-10-08', '9313193998', 'Subhanpura, Vadodara')," +
                    "('Bhautik', 'S', 'Ranpara', '2004-10-18', '9313193998', 'Subhanpura, Rajkot')," +
                    "('Meet', 'S', 'Rajpal', '2005-10-28', '9313193998', 'Subhanpura, Rajkot')," +
                    "('Sujal', 'S', 'Myatra', '2006-10-11', '9313193998', 'Subhanpura, Katch')";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Inserted More Records");
        }

        public ActionResult UpdateFirstName()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "update Employee set FirstName = 'SQLPerson' where Id = 26";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Updated FirstName of 1st Record");
        }

        public ActionResult UpdateMiddleName()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "update Employee set MiddleName = 'I'";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Updated MiddleName of all Data");
        }

        public ActionResult DeleteId()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "delete from Employee where Id < 2";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Deleted All Data With Id < 2");
        }

        public ActionResult DeleteAll()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "delete from Employee";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Deleted All Data");
        }
    }
}