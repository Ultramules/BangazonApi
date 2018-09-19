using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using firstSprint.Models;

//purpose: 
// author: Adelaide
//methods: 

namespace firstSprint.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ComputersController : ControllerBase
        {
            private readonly IConfiguration _config;

            public ComputersController(IConfiguration config)
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
            // GET: api/Computers
            [HttpGet]
            public async Task<IActionResult> Get()
            {

                string sql = "SELECT * FROM Computers ";

                Console.WriteLine(sql);

                using (IDbConnection conn = Connection)
                {
                    IEnumerable<Computers> allComputers = await conn.QueryAsync<Computers>(sql);
                    return Ok(allComputers);
                }
            }


            // GET /Computers/5
            //Returns a single computer matching the Id passed in the URL
            [HttpGet("{id}", Name = "GetComputers")]
            public async Task<IActionResult> Get([FromRoute]int id)
            {
                string sql = $"SELECT * FROM Computers WHERE Id = {id}";

                using (IDbConnection conn = Connection)
                {
                    try
                    {
                        Computers computer = (await conn.QueryAsync<Computers>(sql)).Single();
                        return Ok(computer);
                    }

                    catch (InvalidOperationException)
                    {
                        return new StatusCodeResult(StatusCodes.Status404NotFound);
                    }
                }
            }
            // POST: api/Computers
            [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Computers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
