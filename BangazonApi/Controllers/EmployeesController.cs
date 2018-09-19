<<<<<<< HEAD
<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // GET: api/Employees
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "GetEmployees")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employees
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employees/5
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
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // GET: api/Employees
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "GetEmployees")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employees
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employees/5
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
>>>>>>> master
=======
﻿using System;
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

//purpose: 
// author: Adelaide
//methods: 

namespace firstSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EmployeesController(IConfiguration config)
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
        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            string sql = @"
        SELECT * FROM Employees

            
            ";

            Console.WriteLine(sql);

            using (IDbConnection conn = Connection)
            {
                IEnumerable<Employees> allEmployees = await conn.QueryAsync<Employees>(sql);
                return Ok(allEmployees);
            }
        }


        // GET api/Employees/5
        //Returns a single employee matching the Id passed in the URL
        [HttpGet("{id}", Name = "GetEmployees")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            string sql = $"SELECT * FROM Employees WHERE EmployeeId = {id}";

            using (IDbConnection conn = Connection)
            {
                try
                {
                    Employees employee = (await conn.QueryAsync<Employees>(sql)).Single();
                    return Ok(employee);
                }

                catch (InvalidOperationException)
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }
            }
        }

        //POST api/Employees
        //Post an Employee to DB.
        //Must match Employee model. FirstName, LastName, Supervisor, DepartmentsId are required params.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employees employee)
        {
            string sql = $@"INSERT INTO Employees
            (FirstName, LastName, Supervisor, DepartmentsId)
            VALUES
            ('{employee.FirstName}', '{employee.LastName}', '{employee.Supervisor}', '{employee.DepartmentsId}');
            select MAX(Id) from Employees;";

            Console.WriteLine(sql);
            using (IDbConnection conn = Connection)
            {
                var CreateEmployeeId = (await conn.QueryAsync<int>(sql)).Single();
                employee.EmployeeId = CreateEmployeeId;
                return CreatedAtRoute("GetEmployees", new { id = CreateEmployeeId }, employee);
            }
        }


        // PUT: api/Employees/5
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
>>>>>>> master
