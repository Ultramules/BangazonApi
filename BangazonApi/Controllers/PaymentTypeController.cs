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

namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private object exercise;
        private object newPaymentTypesId;

        public PaymentTypeController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public object Id { get; private set; }
        public object TypeAccountNumber { get; private set; }
        public object Type { get; private set; }


        //Verbs to be supported

        //GET
        //POST
        //PUT
        //DELETE
        //User should be able to GET a list, and GET a single item.


        // GET api/PaymentTypes
        //Defines GET method for GET all from PaymentTypes table
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM PaymentTypes";
                var fullPaymentTypes = await conn.QueryAsync<PaymentTypes>(sql);
                return Ok(fullPaymentTypes);
            };
        }

        // GET: api/PaymentType/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            string sql = $"SELECT Id, TypeAccountNumber, Type, BillingAddress, Customer  FROM PaymentTypes WHERE Id = {id}";

            using (IDbConnection conn = Connection)
            {
                var fullPaymentTypes = await conn.QueryAsync<PaymentTypes>(sql);
                return Ok(fullPaymentTypes);
            }
        }

        // POST: api/PaymentType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentTypeController paymentTypes)
        {
            string sql = $@"INSERT INTO PaymentTypes
            (`TypeAccountNumber`, `Type`)
            VALUES
            ('{paymentTypes.TypeAccountNumber}', '{paymentTypes.Type}');
            select seq from sqlite_sequence where name='Exercise';";

            using (IDbConnection conn = Connection)
            {
                var newPaymentTypeControllerId = (await conn.QueryAsync<int>(sql)).Single();
                paymentTypes.Id = newPaymentTypesId;
                return CreatedAtRoute("GetPaymentTypes", new { id = newPaymentTypesId }, paymentTypes);
            }
        }

        private IActionResult CreatedAtRoute(string v, object p, object exercise)
        {
            throw new NotImplementedException();
        }

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
}


