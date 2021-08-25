using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Sheffield_App.Models;

namespace Sheffield_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
select * from dbo.Student";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Sheffield_AppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Student stu)
        {
            string query = @"
insert into dbo.Student values (@Student_NO, @Student_name)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Sheffield_AppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Student_NO", stu.Student_NO);
                    myCommand.Parameters.AddWithValue("@Student_Name", stu.Student_Name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Successfully Inserted");
        }
        [HttpPut]
        public JsonResult Put(Student stu)
        {
            string query = @"
update dbo.Student
set Student_Name = @Student_Name
where Student_NO=@Student_NO";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Sheffield_AppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Student_NO", stu.Student_NO);

                    myCommand.Parameters.AddWithValue("@Student_Name", stu.Student_Name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }
            return new JsonResult("Updated successfully");
        }

    }
}
