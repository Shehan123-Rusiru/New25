using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Sheffield_App.Models;
using Microsoft.Extensions.Configuration;

namespace Sheffield_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public GradeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
select * from dbo.Grade";

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
        public JsonResult Post(Grade Gr)
        {
            string query = @"
insert into dbo.Grade values (@Student_NO, @Subject_ID,@Marks,@Grades)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Sheffield_AppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Student_NO", Gr.Student_NO);
                    myCommand.Parameters.AddWithValue("@Subject_ID", Gr.Subject_ID);
                    myCommand.Parameters.AddWithValue("@Marks", Gr.Marks);
                    myCommand.Parameters.AddWithValue("@Grades", Gr.Grades);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Successfully Inserted");
        }
        [HttpPut]
        public JsonResult Put(Grade Gr)
        {
            string query = @"
update dbo.Grade
set  Subject_ID = @Subject_ID, Marks = @Marks, Grades= @Grades 
where Student_NO=@Student_NO";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Sheffield_AppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Student_NO", Gr.Student_NO);
                    myCommand.Parameters.AddWithValue("@Subject_ID", Gr.Subject_ID);
                    

                    myCommand.Parameters.AddWithValue("@Marks", Gr.Marks);
                    myCommand.Parameters.AddWithValue("@Grades", Gr.Grades);
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
