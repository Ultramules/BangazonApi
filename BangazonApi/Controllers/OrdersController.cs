<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> master
﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using firstSprint.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
/*
* Created by Jacob Henderson
* Controller made to GET, POST, PUT, and DELETE items from the Orders and OrderProducts tables
* and query them as required by the client.
* */
namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IConfiguration _config;
        //Constructor setting value of a field to use within the Controller.
        public OrdersController(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                //Using field to connect to the SQL files
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: api/Orders
        [HttpGet]
        // Initial GET method with the Include and Completed query as requested by the client.
        public async Task<IActionResult> Get(string _include, bool completed)
        {
            //Created a tuple to hold the queries for organization and easy access
            (string _include, bool completed) queries = (_include, completed);
            //Each SQL request needed for getting proper information. Breaking up the variables is needed for logic
            //that requires different joins based on different queries requested by the user.
            string SqlSelect = @"Select * FROM Orders o";
            string SqlJoinProducts = "JOIN ProductOrders po ON o.OrderId = po.OrderId" +
                                     " JOIN Products p ON p.ProductId = po.ProductId";
            string SqlJoin = "";
            //If the user uses the include query with customers then the customer info should be joined onto the Orders table
            if(queries._include != null && queries._include.Contains("customers"))
            {
                SqlJoin = "JOIN Customers c on c.CustomerId = o.CustomerId";
            }
            // Main Sql variable that will be used below
            string Sql = $"{SqlSelect} {SqlJoinProducts} {SqlJoin}";
            Console.WriteLine(Sql);
            using (IDbConnection conn = Connection)
            {
                //Dictionary created to hold all the Order instances requested
                Dictionary<int, Orders> OrderDictionary = new Dictionary<int, Orders>();
                //Dapper is allowing us to connect our C# with the SQL and make the requests here
                try {
                    var OrdersQuery = await conn.QueryAsync<Orders, Products, Orders>(Sql,
                   (Order, Product) =>
                   {
                        //Creating an instance of an Order to populate the information in the model
                        Orders OrderInstance;
                       if (!OrderDictionary.TryGetValue(Order.OrderId, out OrderInstance))
                       {
                           OrderInstance = Order;
                           OrderInstance.Products = new List<Products>();
                            //Adding each Order instance into the dictionary. There is an iteration allowing
                            //Each item to go into the dictionary.
                            OrderDictionary.Add(OrderInstance.OrderId, OrderInstance);
                       }
                        //Adding each product related to the Orders into a list so it is accessed for every search
                        OrderInstance.Products.Add(Product);
                        //Returning the Order Instance fully populated with the necessary information
                        return OrderInstance;
                   },
                   splitOn: "ProductId"
                   );
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
                //Returning the status code 200 letting the user know their request was granted.
                return Ok(OrderDictionary.Values);
            }

        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "GetOrders")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            //Sql code that is getting the item from the list matching the id they put in the route.
            string SqlCode = $@"SELECT * FROM Orders o
                                WHERE o.OrderId = {id}";
            using (IDbConnection conn = Connection)
            {
                //Using Dapper to connect the SQL with the C#
                IEnumerable<Orders> RoutedOrders = await conn.QueryAsync<Orders>(SqlCode);
                //Returning the correct info from the users request
                return Ok(RoutedOrders);
            }
        }

        //// POST: api/Orders
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Orders order)
        //{
        //    //Due to having a list, a foreach is needed to allow all correct information to be posted into both tables
        //    string SqlCode = "";
        //    foreach(var item in order.Products)
        //    {
        //        SqlCode = $@"
        //                        INSERT INTO Orders
        //                        ,{order.Customer.Id}
        //                        ,{order.PaymentType.Id}
        //                        ,select seq from sqlite_sequence where name='Orders';
        //                        INSERT INTO OrderProducts
        //                        ,{order.Id}
        //                        ,{item.Id};
        //                        ";
        //    }
        //    using (IDbConnection conn = Connection)
        //    {
        //        var newOrderId = (await conn.QueryAsync<int>(SqlCode)).Single();
        //        order.Id = newOrderId;
        //        return CreatedAtRoute("GetExercise", new { id = newOrderId }, order);
        //    }
        //}

        //// PUT: api/Orders/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
<<<<<<< HEAD
=======
﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using firstSprint.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
/*
* Created by Jacob Henderson
* Controller made to GET, POST, PUT, and DELETE items from the Orders and OrderProducts tables
* and query them as required by the client.
* */
namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IConfiguration _config;
        //Constructor setting value of a field to use within the Controller.
        public OrdersController(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                //Using field to connect to the SQL files
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: api/Orders
        [HttpGet]
        // Initial GET method with the Include and Completed query as requested by the client.
        public async Task<IActionResult> Get(string _include, bool completed)
        {
            //Created a tuple to hold the queries for organization and easy access
            (string _include, bool completed) queries = (_include, completed);
            //Each SQL request needed for getting proper information. Breaking up the variables is needed for logic
            //that requires different joins based on different queries requested by the user.
            string SqlSelect = @"Select * FROM Orders o";
            string SqlJoinProducts = "JOIN ProductOrders po ON o.OrderId = po.OrderId" +
                                     " JOIN Products p ON p.ProductId = po.ProductId";
            string SqlJoin = "";
            //If the user uses the include query with customers then the customer info should be joined onto the Orders table
            if(queries._include != null && queries._include.Contains("customers"))
            {
                SqlJoin = "JOIN Customers c on c.CustomerId = o.CustomerId";
            }
            // Main Sql variable that will be used below
            string Sql = $"{SqlSelect} {SqlJoinProducts} {SqlJoin}";
            Console.WriteLine(Sql);
            using (IDbConnection conn = Connection)
            {
                //Dictionary created to hold all the Order instances requested
                Dictionary<int, Orders> OrderDictionary = new Dictionary<int, Orders>();
                //Dapper is allowing us to connect our C# with the SQL and make the requests here
                try {
                    var OrdersQuery = await conn.QueryAsync<Orders, Products, Orders>(Sql,
                   (Order, Product) =>
                   {
                        //Creating an instance of an Order to populate the information in the model
                        Orders OrderInstance;
                       if (!OrderDictionary.TryGetValue(Order.OrderId, out OrderInstance))
                       {
                           OrderInstance = Order;
                           OrderInstance.Products = new List<Products>();
                            //Adding each Order instance into the dictionary. There is an iteration allowing
                            //Each item to go into the dictionary.
                            OrderDictionary.Add(OrderInstance.OrderId, OrderInstance);
                       }
                        //Adding each product related to the Orders into a list so it is accessed for every search
                        OrderInstance.Products.Add(Product);
                        //Returning the Order Instance fully populated with the necessary information
                        return OrderInstance;
                   },
                   splitOn: "ProductId"
                   );
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
                //Returning the status code 200 letting the user know their request was granted.
                return Ok(OrderDictionary.Values);
            }

        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "GetOrders")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            //Sql code that is getting the item from the list matching the id they put in the route.
            string SqlCode = $@"SELECT * FROM Orders o
                                WHERE o.OrderId = {id}";
            using (IDbConnection conn = Connection)
            {
                //Using Dapper to connect the SQL with the C#
                IEnumerable<Orders> RoutedOrders = await conn.QueryAsync<Orders>(SqlCode);
                //Returning the correct info from the users request
                return Ok(RoutedOrders);
            }
        }

        //// POST: api/Orders
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Orders order)
        //{
        //    //Due to having a list, a foreach is needed to allow all correct information to be posted into both tables
        //    string SqlCode = "";
        //    foreach(var item in order.Products)
        //    {
        //        SqlCode = $@"
        //                        INSERT INTO Orders
        //                        ,{order.Customer.Id}
        //                        ,{order.PaymentType.Id}
        //                        ,select seq from sqlite_sequence where name='Orders';
        //                        INSERT INTO OrderProducts
        //                        ,{order.Id}
        //                        ,{item.Id};
        //                        ";
        //    }
        //    using (IDbConnection conn = Connection)
        //    {
        //        var newOrderId = (await conn.QueryAsync<int>(SqlCode)).Single();
        //        order.Id = newOrderId;
        //        return CreatedAtRoute("GetExercise", new { id = newOrderId }, order);
        //    }
        //}

        //// PUT: api/Orders/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
>>>>>>> master
=======
>>>>>>> master
