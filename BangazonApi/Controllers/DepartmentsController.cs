using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// Author: Evan Lusky
// Exposes Departments to users, allows user to add employees to department query.

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DepartmentsController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: api/Departments/5
        // This method will return a single Department object with the ID being specified in URL
        [HttpGet("{id}", Name = "GetDepartment")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = $"SELECT * FROM Departments WHERE Id = {id}";

                var SingleDepartment = (await conn.QueryAsync<Departments>(sql)).Single();
                return Ok(SingleDepartment);
            }
        }

        // POST: api/Departments
        // This method will return the departments object that was created
        public async Task<IActionResult> Post([FromBody] Departments department)
        {
            string sql = $@"INSERT INTO Departments
            (Name, ExpenseBudget)
            VALUES
            ('{department.Name}', '{department.ExpenseBudget}');
            select MAX(Id) from Departments";

            using (IDbConnection conn = Connection)
            {
                var createDepartmentId = (await conn.QueryAsync<int>(sql)).Single();
                department.Id = createDepartmentId;
                return CreatedAtRoute("GetDepartment", new { id = createDepartmentId }, department);
            }

        }

        // checks to see if deparment is already in database 
        private bool InvalidDepartment(int id)
        {
            string sql = $"SELECT Id, Name, ExpenseBudget FROM Departments WHERE Id = {id}";
            using (IDbConnection conn = Connection)
            {
                return conn.Query<Departments>(sql).Count() > 0;
            }
        }

        // PUT api/values/5
        // This method will append and replace department object using specified ID in api url 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Departments department)
        {
            string sql = $@"
            UPDATE Departments
            SET Name = '{department.Name}',
                ExpenseBudget = '{department.ExpenseBudget}'
            WHERE Id = {id}";

            try
            {
                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);
                    if (rowsAffected > 0)
                    {
                        return new StatusCodeResult(StatusCodes.Status204NoContent);
                    }
                    throw new Exception("Not Appended");
                }
            }
            catch (Exception)
            {
                if (!InvalidDepartment(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }




    }
}