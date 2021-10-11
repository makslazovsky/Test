using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/Department
        [HttpGet]
        public string Get()
        {
            string query = "Select * from department";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    
                    myReader.Close();
                    myCon.Close();
                }
            }

                return JsonConvert.SerializeObject(table);
        }

        // POST: api/Department
        [HttpPost]
        public string Post(Department department)
        {
            string query = "Insert into department (Name, ParentId) values" +
                "(@Name, @ParentId)";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", department.Name);
                    myCommand.Parameters.AddWithValue("@ParentId", department.ParentId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return "Added successfully";
        }

        // PUT: api/Department/5
        [HttpPut]
        public string Put(Department department)
        {
            string query = "Update department set " +
                "Name = @Name, ParentId = @ParentId " +
                "Where DepartmentId = @DepartmentId";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                    myCommand.Parameters.AddWithValue("@Name", department.Name);
                    myCommand.Parameters.AddWithValue("@ParentId", department.ParentId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return "Updated successfully";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string query = "Delete from department " +
                "Where DepartmentId = @DepartmentId";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return "Deleted successfully";
        }
    }
}
