using ADO.NET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ADO.NET_MVC.Controllers
{
    public class Task_2Controller : Controller
    {
        string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public ActionResult Index()
        {
            List<Employee2> employees = new List<Employee2>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select * from Employee2";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee2 emp = new Employee2
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
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
                string query = "insert into Employee2 (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary) values " +
                    "('Shivang', 'Manojbhai', 'Chauhan', '2003-10-08', '9313193998', 'Subhanpura, Vadodara', 45000)," +
                    "('Bhautik', 'S', 'Ranpara', '2004-10-18', '9313193998', 'Subhanpura, Rajkot', 50000)," +
                    "('Meet', null, 'Rajpal', '2005-10-28', '9313193998', 'Subhanpura, Rajkot', 45000)," +
                    "('Sujal', 'S', 'Myatra', '1997-10-11', '9313193998', 'Subhanpura, Katch', 45000)";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Inserted Some Records");
        }

        public ActionResult TotalSalaries()
        {
            decimal TotalSalary = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select sum(Salary) as Total_Salary from Employee2";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                TotalSalary = Convert.ToDecimal(result);
            }
            return Content("Total Salary : " + TotalSalary);
        }

        public ActionResult OlderEmployee()
        {
            List<Employee2> employees = new List<Employee2>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select * from Employee2 where DOB < '2000-01-01'";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee2 emp = new Employee2
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                    employees.Add(emp);
                }
            }
            return View(employees);
        }

        public ActionResult NullMiddlename()
        {
            int TotalEmployee = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select count(*) from Employee2 where MiddleName is null";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                TotalEmployee = Convert.ToInt32(result);
            }
            return Content("Total Employees : " + TotalEmployee);
        }
    }
}