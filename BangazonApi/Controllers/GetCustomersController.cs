//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Dapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace firstSprint.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomersController : ControllerBase
//    {
//        // GET: api/Customers
//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            string sql = @"
//            SELECT
//            c.Id,
//            c.FirstName,
//            c.LastName,
//            c.AccountCreationDate,
//            c.LastLoginDate
//            FROM Customers c

//";
//        }

//        // GET: api/Customers/5
//        [HttpGet("{id}", Name = "Get")]
//        public string Get(int id)
//        public async Task<IActionResult> Get([FromRoute]int id)
//            {
//                string sql = $"SELECT Id, Name, Language FROM Exercise WHERE Id = {id}";

//                using (IDbConnection conn = Connection)
//                {
//                    IEnumerable<Exercise> exercises = await conn.QueryAsync<Exercise>(sql);
//                    return Ok(exercises);
//                }
//            }
//        }

//        // POST: api/Customers
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT: api/Customers/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE: api/ApiWithActions/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
