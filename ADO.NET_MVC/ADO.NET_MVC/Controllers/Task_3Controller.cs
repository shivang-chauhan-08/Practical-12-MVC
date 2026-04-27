using ADO.NET_MVC.Models;
using ADO.NET_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            return View();
        }

        public ActionResult CountByDesignation()
        {
            List<DesignationCountVM> list = new List<DesignationCountVM>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select d.Designation, count(e.Id) as EmployeeCount from Designation d left join Employee3 e on d.Id = e.DesignationId group by d.Designation";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DesignationCountVM
                    {
                        Designation = reader["Designation"].ToString(),
                        EmployeeCount = Convert.ToInt32(reader["EmployeeCount"])
                    });
                }
            }
            return View(list);
        }

        public ActionResult EmployeeInfo()
        {
            List<EmployeeInfoVM> list = new List<EmployeeInfoVM>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select e.FirstName, e.MiddleName, e.LastName, d.Designation from Employee3 e left join Designation d on d.Id = e.DesignationId";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new EmployeeInfoVM
                    {
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Designation = reader["Designation"].ToString()
                    });
                }
            }
            return View(list);
        }

        public ActionResult EmployeeView()
        {
            List<EmployeeDetailVM> list = new List<EmployeeDetailVM>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select * from EmployeeDetails_View";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new EmployeeDetailVM
                    {
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Designation = reader["Designation"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    });
                }
            }
            return View(list);
        }

        public ActionResult InsertDesSP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertDesSP(Designation model)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertDesignation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Designation", SqlDbType.VarChar, 50).Value = model.DesignationName;

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult InsertEmpSP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertEmpSP(Employee3 emp)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = emp.FirstName;
                cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(emp.MiddleName) ? (object)DBNull.Value : emp.MiddleName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = emp.LastName;
                cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = emp.DOB;
                cmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 10).Value = emp.MobileNumber;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar, 100).Value = string.IsNullOrEmpty(emp.Address) ? (object)DBNull.Value : emp.Address;
                cmd.Parameters.Add("@Salary", SqlDbType.Decimal).Value = emp.Salary;
                cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = emp.DesignationId;

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DesignationWithMoreThan1Emp()
        {
            List<DesignationCountVM> list = new List<DesignationCountVM>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select d.Designation, count(e.Id) as EmployeeCount from Designation d left join Employee3 e on d.Id = e.DesignationId group by d.Designation having count(e.Id) > 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DesignationCountVM
                    {
                        Designation = reader["Designation"].ToString(),
                        EmployeeCount = Convert.ToInt32(reader["EmployeeCount"])
                    });
                }
            }
            return View(list);
        }

        public ActionResult GetEmployeeSP()
        {
            List<EmployeeDetailVM> list = new List<EmployeeDetailVM>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new EmployeeDetailVM
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"]?.ToString(),
                        LastName = reader["LastName"].ToString(),
                        Designation = reader["Designation"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    });
                }
            }

            return View(list);
        }

        public ActionResult EmployeesByDesignation(int designationId = 2)
        {
            List<EmployeeDetailVM> list = new List<EmployeeDetailVM>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployeesByDesignation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = designationId;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new EmployeeDetailVM
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"]?.ToString(),
                        LastName = reader["LastName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    });
                }
            }

            return View(list);
        }

        public ActionResult NonClusteredIndex()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "create nonclustered index NonIndex_DesignationId on Employee3(DesignationId)";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Content("Non Clustered Index Generated on DesignationId(Employee)");
        }

        public ActionResult MaxSalary()
        {
            Employee3 emp = new Employee3();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "select top 1 * from Employee3 order by Salary desc";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.Salary = Convert.ToDecimal(reader["Salary"]);
                }
            }
            return View(emp);
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