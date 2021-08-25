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
    public class SubjectController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SubjectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
select * from dbo.Subject";

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
        public JsonResult Post(Subject sub)
        {
            string query = @"
insert into dbo.Subject values (@Subject_ID, @Subject_Name)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Sheffield_AppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Subject_ID", sub.Subject_ID);
                    myCommand.Parameters.AddWithValue("@Subject_Name", sub.Subject_Name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Successfully Inserted");
        }
        [HttpPut]
        public JsonResult Put(Subject sub)
        {
            string query = @"
update dbo.Subject
set Subject_Name = @Subject_Name
where Subject_ID=@Subject_ID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Sheffield_AppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Subject_ID", sub.Subject_ID);

                    myCommand.Parameters.AddWithValue("@Subject_Name", sub.Subject_Name);
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
