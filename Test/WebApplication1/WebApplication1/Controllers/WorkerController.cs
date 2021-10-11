using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public WorkerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Worker
        [HttpGet]
        public string Get()
        {
            string query = "Select * from Worker";
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

        [HttpGet("Department/{id}")]
        public string Get(int id)
        {
            string query = "Select * from Worker " +
                "where DepartmentId = @DepartmentId";
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

            return JsonConvert.SerializeObject(table);
        }

        [HttpGet("{searchText}")]
        public string Find(string searchText)
        {
            string query = "Select * from Worker " +
                "Where WorkerName Like @SearchText OR WorkerEmail Like @SearchText";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@SearchText", "%"+searchText+"%");

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return JsonConvert.SerializeObject(table);
        }

        // POST: api/Worker
        [HttpPost]
        public string Post(Worker worker)
        {
            string query = "Insert into worker (WorkerName, DepartmentId, WorkerEmail) values" +
                "(@WorkerName, @DepartmentId, @Email)";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@WorkerName", worker.WorkerName);
                    myCommand.Parameters.AddWithValue("@DepartmentId", worker.DepartmentId);
                    myCommand.Parameters.AddWithValue("@Email", worker.Email);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return "Added successfully";
        }

        // PUT: api/Worker/5
        [HttpPut]
        public string Put(Worker worker)
        {
            string query = "Update worker set " +
                "WorkerName = @Name, DepartmentId = @DepartmentId, WorkerEmail = @Email " +
                "Where WorkerId = @WorkerId";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@WorkerId", worker.WorkerId);
                    myCommand.Parameters.AddWithValue("@Name", worker.WorkerName);
                    myCommand.Parameters.AddWithValue("@DepartmentId", worker.DepartmentId);
                    myCommand.Parameters.AddWithValue("@Email", worker.Email);

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
            string query = "Delete from worker " +
                "Where WorkerId = @WorkerId";
            string sqlDataSource = _configuration.GetConnectionString("TestDbCon");
            MySqlDataReader myReader;
            DataTable table = new DataTable();
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@WorkerId", id);

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
