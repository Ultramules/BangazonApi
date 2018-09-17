using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using firstSprint.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

/*
 AUTHORED BY: Michael Roberts
 
 Purpose: Allow developers to access the Payment Type resource.
 User should be able to GET a list, and GET a single item.
Verbs to be supported:
GET
POST
PUT
DELETE
*/

namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IConfiguration _config;
        //private object exercise;
        //private object newPaymentTypesId;

        public PaymentsController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public object Id { get; private set; }
        public object TypeAccountNumber { get; private set; }
        public object Type { get; private set; }


      

        // GET api/PaymentTypes
        //Defines GET method for GET all from PaymentTypes table
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM Payments";
                var fullPaymentTypes = await conn.QueryAsync<Payment>(sql);
                return Ok(fullPaymentTypes);
            };
        }

        // GET: api/PaymentType/5
        [HttpGet("{id}", Name = "GetPaymentType")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            string sql = $"SELECT Id, TypeAccountNumber, Type, BillingAddress, Customer  FROM PaymentTypes WHERE Id = {id}";

            using (IDbConnection conn = Connection)
            {
                var fullPaymentTypes = await conn.QueryAsync<Payment>(sql);
                return Ok(fullPaymentTypes);
            }
        }

       // POST: api/PaymentType
       //[HttpPostAttribute]
       // public async Task<IActionResult> Post([FromBody] PaymentType paymentTypes)
       // {
       //     string sql = $@"INSERT INTO PaymentTypes
       //     (`TypeAccountNumber`, `Type`)
       //     VALUES
       //     ('{paymentTypes.TypeAccountNumber}', '{paymentTypes.Type}');
       //     select seq from sqlite_sequence where name='Exercise';";

       //     using (IDbConnection conn = Connection)
       //     {
       //         var newPaymentTypeControllerId = (await conn.QueryAsync<int>(sql)).Single();
       //         paymentTypes.Id = newPaymentTypesId;
       //         return CreatedAtRoute("GetPaymentTypes", new { id = newPaymentTypesId }, paymentTypes);
       //     }
       // }

        // PUT: api/PaymentType/5
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

    internal class POST
    {
    }

    public class PaymentType
    {
    }
}


