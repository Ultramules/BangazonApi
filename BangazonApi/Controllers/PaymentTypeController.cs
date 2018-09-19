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
                var fullPaymentTypes = await conn.QueryAsync<PaymentTypes>(sql);
                return Ok(fullPaymentTypes);
            };
        }

        // This GET method will allow the developers to query a payment time by ID. 
        // GET: api/PaymentType/5
        [HttpGet("{id}", Name = "GetPaymentType")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            string sql = $"SELECT Id, TypeAccountNumber, Type, BillingAddress, Customer  FROM PaymentTypes WHERE Id = {id}";

            using (IDbConnection conn = Connection)
            {
                var fullPaymentTypes = await conn.QueryAsync<PaymentTypes>(sql);
                return Ok(fullPaymentTypes);
            }
        }

        // This Post Method allows the developers to post a payment type to the database.
        //POST: api/PaymentType
        [HttpPostAttribute]
        public async Task<IActionResult> Post([FromBody] PaymentTypes payment)
        {
            string sql = $@"INSERT INTO Payments
             (TypeAccountNumber, Type, BillingAddress, CustomerId)
             VALUES
             ('{payment.TypeAccountNumber}', '{payment.Type}', '{payment.BillingAddress}','{payment.CustomerId}');
             select MAX(Id) from Payments";

            using (IDbConnection conn = Connection)
            {
                var newPaymentTypeId = (await conn.QueryAsync<int>(sql)).Single();
                payment.Id = newPaymentTypeId;
                return CreatedAtRoute("GetPaymentType", new { id = newPaymentTypeId }, payment);
            }
        }

        // PUT: api/PaymentType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Payment payment)
        {
            string sql = $@"
            UPDATE PaymentType
            SET TypeAccountNumber = '{payment.TypeAccountNumber}',
                Type = '{payment.Type}',
                BillingAddress = '{payment.BillingAddress}',
                CustomerId = '{payment.CustomerId}'
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
                    throw new Exception("No rows affected");
                }
            }
            catch (Exception)
            {
                if (PaymentsExists(id))
                {
                    throw;
                }
                else
                {
                    return NotFound();
                }
            }
        }

        private bool PaymentsExists(int id)
        {
            throw new NotImplementedException();
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
//// Queries all data in Payments table
//private bool PaymentsExists(int id)
//{
//    string sql = $"SELECT Id, TypeAccountNumber, Type, BillingAddress, CustomerId FROM Payment WHERE Id = {id}";
//    using (IDbConnection conn = Connection)
//    {
//        return conn.Query<Payment>(sql).Count() > 0;
//    }
//}