using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18849;Integrated Security=True";
        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();
            using (var con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;

                com.CommandText = "select FirstName, LastName, BirthDate, Studies.Name, Semester from Student, Studies, Enrollment where Enrollment.IdEnrollment=Student.IdEnrollment and Studies.IdStudy=Enrollment.IdStudy;";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName= dr["FirstName"].ToString();
                    st.LastName= dr["LastName"].ToString();
                    st.BirthDate= dr["BirthDate"].ToString();
                    st.Study= dr["Name"].ToString();
                    st.Semester= dr["Semester"].ToString();
                    list.Add(st);
                }

            }
            
            

            
            return Ok(list);
        }
    }
}