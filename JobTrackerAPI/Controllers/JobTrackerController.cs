using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobTrackerAPI.Models;
using JobTrackerAPI.Data;
using MySqlConnector;
using Google.Protobuf.WellKnownTypes;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobTrackerController : ControllerBase
    {

        private readonly ApiContext _context;
        public JobTrackerController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/JobApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplication>>> GetJobApplications()
        {
            var jobApplications = await _context.JobApplications
                .Include(JobApplication => JobApplication.ApplicationStatusNavigation)
                .ToListAsync();
            
            if (_context.JobApplications == null)
            {
                return NotFound();
            }

            return jobApplications;
        }

        // GET: api/JobApplications/5 //with id
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplication>> GetJobApplication(Guid id)
        {
            if (_context.JobApplications == null)
            {
                return NotFound();
            }
            var jobApplication = await _context.JobApplications.FindAsync(id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            return jobApplication;
        }

        // POST: api/JobApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobApplication>> PostJobApplication(JobApplication jobApplication)
        {
            if (_context.JobApplications == null)
            {
                return Problem("Entity set 'ApiContext.JobApplications'  is null.");
            }

            // Get the application status from the lookup table based on the provided status code
            var applicationStatus = await _context.ApplicationStatusLookup.FirstOrDefaultAsync(x => x.StatusValue == jobApplication.ApplicationStatus);

            // Set the ApplicationStatusNavigation property to the retrieved application status
            jobApplication.ApplicationStatusNavigation = applicationStatus;

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobApplication", new { id = jobApplication.ApplicationId }, jobApplication);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJobApplication(Guid id)
        {
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // [HttpPut]
        // public async Task<JsonResult> Update(JobApplication jobs)
        // {
        //     string query = @"update job_applications set
        //                     position_title = @position_title
        //                     where id = @id;";

        //     DataTable table = new DataTable();
        //     string sqlConnection = _configuration.GetConnectionString("MySqlConnection");

        //     MySqlDataReader myReader;
        //     await using var connection = new MySqlConnection(sqlConnection);
        //     {
        //         await connection.OpenAsync().ContinueWith(
        //             delegate {
        //                 Console.WriteLine(connection.Ping());
        //             });


        //         using var command = new MySqlCommand(query, connection);
        //         {
        //             command.Parameters.AddWithValue("@id", jobs.id);
        //             command.Parameters.AddWithValue("@position_title", jobs.position_title);
        //             command.Parameters.AddWithValue("@company", jobs.company);
        //             command.Parameters.AddWithValue("@website_link", jobs.website_link);
        //             command.Parameters.AddWithValue("@location", jobs.location);
        //             command.Parameters.AddWithValue("@application_status", jobs.application_status);
        //             myReader = await command.ExecuteReaderAsync();
        //             table.Load(myReader);

        //         }
        //     }
        //     return new JsonResult("Infomation updated!");
        // }

        // // Delete
        //[HttpDelete("{id}")]
        // public async Task<JsonResult> Delete(int id)
        // {
        //     string query = @"delete from job_applications
        //                     where id = @id;";

        //     DataTable table = new DataTable();
        //     string sqlConnection = _configuration.GetConnectionString("MySqlConnection");

        //     MySqlDataReader myReader;
        //     await using var connection = new MySqlConnection(sqlConnection);
        //     {
        //         await connection.OpenAsync().ContinueWith(
        //             delegate {
        //                 Console.WriteLine(connection.Ping());
        //             });


        //         using var command = new MySqlCommand(query, connection);
        //         {
        //             command.Parameters.AddWithValue("@id", id);
        //             myReader = await command.ExecuteReaderAsync();
        //             table.Load(myReader);

        //         }
        //     }
        //     return new JsonResult("Infomation deleted!");
        // }

    }
}
