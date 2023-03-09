using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobTrackerAPI.Models;
using JobTrackerAPI.Data;
using MySqlConnector;
using Google.Protobuf.WellKnownTypes;
using System.Data;

namespace JobTrackerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobTrackerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public JobTrackerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Get all
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            string query = @"select * from job_applications where id = 2;";

            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("MySqlConnection");

            MySqlDataReader myReader; 
            await using var connection = new MySqlConnection(sqlConnection);
            { 
                await connection.OpenAsync().ContinueWith(
                    delegate { 
                        Console.WriteLine(connection.Ping()); 
                    });


                using var command = new MySqlCommand(query, connection);
                { 
                     myReader = await command.ExecuteReaderAsync();

                        // do something with 'value'
                       //var value = myReader.GetValue(2);
                        table.Load(myReader);

                }
                if (table.Rows.Count == 0)
                {
                    return new JsonResult(NoContent());
                }

            }
            return new JsonResult(table);
        }


        // Create/Edit
        [HttpPost]
        public async Task<JsonResult> Create(job_applications jobs)
        {
            string query = @"insert into job_applications (position_title) values (@position_title);";

            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("MySqlConnection");

            MySqlDataReader myReader;
            await using var connection = new MySqlConnection(sqlConnection);
            {
                await connection.OpenAsync().ContinueWith(
                    delegate {
                        Console.WriteLine(connection.Ping());
                    });


                using var command = new MySqlCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@position_title", jobs.position_title);
                    myReader = await command.ExecuteReaderAsync();
                    table.Load(myReader);

                }
            }
            return new JsonResult("Infomation Added!");
        }

        [HttpPut]
        public async Task<JsonResult> Update(job_applications jobs)
        {
            string query = @"update job_applications set
                            position_title = @position_title
                            where id = @id;";

            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("MySqlConnection");

            MySqlDataReader myReader;
            await using var connection = new MySqlConnection(sqlConnection);
            {
                await connection.OpenAsync().ContinueWith(
                    delegate {
                        Console.WriteLine(connection.Ping());
                    });


                using var command = new MySqlCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@id", jobs.id);
                    command.Parameters.AddWithValue("@position_title", jobs.position_title);
                    myReader = await command.ExecuteReaderAsync();
                    table.Load(myReader);

                }
            }
            return new JsonResult("Infomation updated!");
        }

        // Delete
       [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            string query = @"delete from job_applications
                            where id = @id;";

            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("MySqlConnection");

            MySqlDataReader myReader;
            await using var connection = new MySqlConnection(sqlConnection);
            {
                await connection.OpenAsync().ContinueWith(
                    delegate {
                        Console.WriteLine(connection.Ping());
                    });


                using var command = new MySqlCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@id", id);
                    myReader = await command.ExecuteReaderAsync();
                    table.Load(myReader);

                }
            }
            return new JsonResult("Infomation deleted!");
        }

        // Get
        //[HttpGet]
        // public JsonResult Get(int id)
        // {
        //     var result = _context.JobApplications.Find(id);

        //     if (result == null)
        //     {
        //         return new JsonResult(NotFound());
        //     }

        //     return new JsonResult(Ok(result));
        // }




    }
}
