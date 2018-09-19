using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using firstSprint.Models;

namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductsController(IConfiguration config)
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
        //Get Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string sql = "SELECT * FROM Products";

            using (IDbConnection conn = Connection)
            {
                IEnumerable<Products> products = await conn.QueryAsync<Products>(sql);
                return Ok(products);
            }
        }
        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetProducts")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = $"SELECT * FROM Products WHERE ProductId = {id}";

                var theSingleExercise = (await conn.QueryAsync<Products>(sql)).Single();
                return Ok(theSingleExercise);
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Products product)
        {
            string sql = $@"INSERT INTO Exercise
            (Title, Price, Description, Quantity, ProductTypeId, CustomerId)
            VALUES
            ('{product.Title}', '{product.Price}','{product.Description}','{product.Quantity}','{product.ProductTypesId}','{product.CustomerId}');
            select MAX(Id) from Exercise";

            using (IDbConnection conn = Connection)
            {
                var ProductId = (await conn.QueryAsync<int>(sql)).Single();
                product.ProductId = ProductId;
                return CreatedAtRoute("GetProduct", new { id = ProductId }, product);
            }

        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Products product)
        {
            string sql = $@"
            UPDATE Product
            SET Name = '{product.Price}',
                Description = '{product.Description}',
                Quantity = '{product.Quantity}',
                ProductTypesId = '{product.ProductTypesId}',
                CustomerId = '{product.CustomerId}'
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
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string sql = $@"DELETE FROM Product WHERE Id = {id}";

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

        private bool ProductExists(int id)
        {
            string sql = $"SELECT Title, Price, Description, Quantity, ProductTypeId, CustomerId FROM Products WHERE Id = {id}";
            using (IDbConnection conn = Connection)
            {
                return conn.Query<Products>(sql).Count() > 0;
            }
        }
    }
}

