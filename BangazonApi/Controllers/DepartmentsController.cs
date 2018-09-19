<<<<<<< HEAD
﻿using System;
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
                string sql = $"SELECT * FROM Departments WHERE DepartmentId = {id}";

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
            select MAX(DepartmentId) from Departments";

            using (IDbConnection conn = Connection)
            {
                var createDepartmentId = (await conn.QueryAsync<int>(sql)).Single();
                department.DepartmentId = createDepartmentId;
                return CreatedAtRoute("GetDepartment", new { id = createDepartmentId }, department);
            }
        }

        // checks to see if deparment is already in database 
        private bool InvalidDepartment(int id)
        {
            string sql = $"SELECT Id, Name, ExpenseBudget FROM Departments WHERE DepartmentId = {id}";
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
            WHERE DepartmentId = {id}";
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

        //   GET /Departments?_include=employees
        //   Method: If returns employees then returns a list of departments each with a list of their employees
        //   Method: Else returns a list of departments
        [HttpGet]
        public async Task<IActionResult> Get(string _include, string _budget, int _greaterThan)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "Select * FROM Departments";

                if (_include != null && _include.Contains("employee"))
                {

                    sql = $" Select * FROM Departments JOIN Employee ON Departments.DepartmentId = Employee.DepartmentId";
                    Dictionary<int, Departments> report = new Dictionary<int, Departments>();
                    var fullDep = await conn.QueryAsync<Departments, Employees, Departments>(
                    sql, (department, employee) =>
                    {
                        if (!report.ContainsKey(department.DepartmentId))
                        {
                            report[department.DepartmentId] = department;
                        }
                        report[department.DepartmentId].EmployeeList.Add(employee);
                        return department;
                    }, splitOn: "DepartmentId, EmployeeId"
                        );
                    return Ok(report.Values);
                }

                if (_greaterThan > 1)
                {
                    sql = $@"SELECT * FROM Department WHERE ExpenseBudget >= {_greaterThan}";
                    var budget = await conn.QueryAsync<Departments>(
                        sql);
                    return Ok(budget);
                }
                var departments = await conn.QueryAsync<Departments>(sql);
                return Ok(departments);
            }
        }
    }
=======
﻿using System;
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
                string sql = $"SELECT * FROM Departments WHERE DepartmentId = {id}";

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
            select MAX(DepartmentId) from Departments";

            using (IDbConnection conn = Connection)
            {
                var createDepartmentId = (await conn.QueryAsync<int>(sql)).Single();
                department.DepartmentId = createDepartmentId;
                return CreatedAtRoute("GetDepartment", new { id = createDepartmentId }, department);
            }
        }

        // checks to see if deparment is already in database 
        private bool InvalidDepartment(int id)
        {
            string sql = $"SELECT Id, Name, ExpenseBudget FROM Departments WHERE DepartmentId = {id}";
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
            WHERE DepartmentId = {id}";
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

        //   GET /Departments?_include=employees
        //   Method: If returns employees then returns a list of departments each with a list of their employees
        //   Method: Else returns a list of departments
        [HttpGet]
        public async Task<IActionResult> Get(string _include, string _budget, int _greaterThan)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "Select * FROM Departments";

                if (_include != null && _include.Contains("employee"))
                {

                    sql = $" Select * FROM Departments JOIN Employee ON Departments.DepartmentId = Employee.DepartmentId";
                    Dictionary<int, Departments> report = new Dictionary<int, Departments>();
                    var fullDep = await conn.QueryAsync<Departments, Employees, Departments>(
                    sql, (department, employee) =>
                    {
                        if (!report.ContainsKey(department.DepartmentId))
                        {
                            report[department.DepartmentId] = department;
                        }
                        report[department.DepartmentId].EmployeeList.Add(employee);
                        return department;
                    }, splitOn: "DepartmentId, EmployeeId"
                        );
                    return Ok(report.Values);
                }

                if (_greaterThan > 1)
                {
                    sql = $@"SELECT * FROM Department WHERE ExpenseBudget >= {_greaterThan}";
                    var budget = await conn.QueryAsync<Departments>(
                        sql);
                    return Ok(budget);
                }
                var departments = await conn.QueryAsync<Departments>(sql);
                return Ok(departments);
            }
        }
    }
>>>>>>> master
}