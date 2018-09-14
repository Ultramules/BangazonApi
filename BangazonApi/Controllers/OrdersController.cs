using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using firstSprint.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IConfiguration _config;
        public OrdersController(IConfiguration config)
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
        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> Get(string _include, bool completed)
        {
            (string _include, bool completed) queries = (_include, completed);
            string SqlSelect = "@ SELECT * " +
                               "FROM Orders o";
            string SqlJoinProducts = "@JOIN ProductOrders po ON po.OrderId = o.Id" +
                                     "JOIN Products p ON p.Id = po.ProductId;";
            string SqlJoin = "";
            if(queries._include != null && queries._include.Contains("customers"))
            {
                SqlJoin = "JOIN Customers c on c.Id = o.CustomerId";
            }
            string Sql = $"{SqlSelect} {SqlJoinProducts} {SqlJoin}";
            using (IDbConnection conn = Connection)
            {
                Dictionary<int, Orders> OrderDictionary = new Dictionary<int, Orders>();
                var OrdersQuery = await conn.QueryAsync<Orders, Products, Orders>(Sql,
                    (Order, Product) =>
                    {
                        Orders OrderInstance;
                        if (OrderDictionary.TryGetValue(Order.Id, out OrderInstance))
                        {
                            OrderInstance = Order;
                            OrderInstance.Products = new List<Products>();
                            OrderDictionary.Add(OrderInstance.Id, OrderInstance);
                        }
                        OrderInstance.Products.Add(Product);
                        return OrderInstance;
                    }
                    );
                return Ok(OrdersQuery.Distinct());
            }

        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Orders/5
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
