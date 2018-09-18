using System;
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
                string sql = $"SELECT * FROM Departments WHERE Id = {id}";

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
            select MAX(Id) from Departments";

            using (IDbConnection conn = Connection)
            {
                var createDepartmentId = (await conn.QueryAsync<int>(sql)).Single();
                department.Id = createDepartmentId;
                return CreatedAtRoute("GetDepartment", new { id = createDepartmentId }, department);
            }
        }

        // checks to see if deparment is already in database 
        private bool InvalidDepartment(int id)
        {
            string sql = $"SELECT Id, Name, ExpenseBudget FROM Departments WHERE Id = {id}";
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
        //   If returns employees then returns a list of departments each with a list of their employees
        //   Else returns a list of departments
        [HttpGet]
        public async Task<IActionResult> Get(string _include)
        {
            string sql = "Select * FROM Departments";
            if (_include != null && _include.Contains("employees"))
            {
                sql = "SELECT * FROM Departments AS A INNER JOIN Employees AS DE ON A.Id = B.DepartmentId;";
            }
            using (IDbConnection conn = Connection)
            {
                if (_include == "employees")
                {
                    Dictionary<int, Departments> employeesOfDepartment = new Dictionary<int, Departments>();
                    var queryDepartments = await conn.QueryAsync<Departments, Employees, Departments>(
                        sql,
                        (department, employee) =>
                        {
                            Departments departmentData;
                            if (!employeesOfDepartment.TryGetValue(department.Id, out departmentData))
                            {
                                departmentData = department;
                                departmentData.EmployeeList = new List<Employees>();
                                employeesOfDepartment.Add(departmentData.Id, departmentData);
                            }
                            departmentData.EmployeeList.Add(employee);
                            return departmentData;
                        });
                    return Ok(queryDepartments.Distinct());
                }
                var departments = await conn.QueryAsync<Departments>(sql);
                return Ok(departments);
            }
        }
    }
}