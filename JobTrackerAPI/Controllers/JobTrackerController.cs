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
            string query = @"select id, position_title, company, website_link, location, application_status,
                            DATE_FORMAT(date_applied, '%Y-%m-%d') as date_applied, 
                            DATE_FORMAT(interview1_date, '%Y-%m-%d') as interview1_date,
                            DATE_FORMAT(interview2_date, '%Y-%m-%d') as interview2_date,
                            DATE_FORMAT(interview3_date, '%Y-%m-%d') as interview3_date,note,
                            days_since_applying, days_since_interview1, days_since_interview2, days_since_interview3
                            from job_applications;";

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
            string query = @"insert into job_applications ( position_title, company, website_link, location, application_status,
                            date_applied, interview1_date,interview2_date,interview3_date,note,
                            days_since_applying, days_since_interview1, days_since_interview2, days_since_interview3) 
                            values ( @position_title, @company, @website_link, @location, @application_status,
                            @date_applied, @interview1_date, @interview2_date, @interview3_date,@note,
                            @days_since_applying, @days_since_interview1, @days_since_interview2, @days_since_interview3);";

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
                    command.Parameters.AddWithValue("@company", jobs.company);
                    command.Parameters.AddWithValue("@website_link", jobs.website_link);
                    command.Parameters.AddWithValue("@location", jobs.location);
                    command.Parameters.AddWithValue("@application_status", jobs.application_status);
                    command.Parameters.AddWithValue("@date_applied", jobs.date_applied);
                    command.Parameters.AddWithValue("@interview1_date", jobs.interview1_date);
                    command.Parameters.AddWithValue("@interview2_date", jobs.interview2_date);
                    command.Parameters.AddWithValue("@interview3_date", jobs.interview3_date);
                    command.Parameters.AddWithValue("@note", jobs.note);
                    command.Parameters.AddWithValue("@days_since_applying", jobs.days_since_applying);
                    command.Parameters.AddWithValue("@days_since_interview1", jobs.days_since_interview1);
                    command.Parameters.AddWithValue("@days_since_interview2", jobs.days_since_interview2);
                    command.Parameters.AddWithValue("@days_since_interview3", jobs.days_since_interview3);

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
                    command.Parameters.AddWithValue("@company", jobs.company);
                    command.Parameters.AddWithValue("@website_link", jobs.website_link);
                    command.Parameters.AddWithValue("@location", jobs.location);
                    command.Parameters.AddWithValue("@application_status", jobs.application_status);
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

    }
}
