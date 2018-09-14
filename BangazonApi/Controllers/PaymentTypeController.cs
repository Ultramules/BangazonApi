using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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

        public PaymentTypeController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));


        // GET: api/PaymentType
        [HttpGet]
        public async Task<IActionResult> Get(string q, string _include, string language)
        {
            // Store URL parameters in a tuple
            (string q, string include, string language) filter = (q, _include, language);

            string sqlSelect = "SELECT e.Id, e.Name, e.Language";
            string sqlFrom = "FROM Exercise e";
            string sqlJoin = "";
            string sqlWhere = "WHERE 1=1";

            string isQ = $"AND (e.Language LIKE '%{q}%' OR e.Name LIKE '%{q}%')";
            string isLanguage = $"AND e.Language = '{filter.language}'";

            string sqlSelectStudents = @"
                       ,s.Id
                       ,s.FirstName
                       ,s.LastName
                       ,s.SlackHandle";
            string studentsIncluded = "JOIN Student s ON se.StudentId = s.Id";
            string studentsExerciseIncluded = "JOIN StudentExercise se ON e.Id = se.ExerciseId";


            string sqlSelectInstructors = @"
                       ,i.Id
                       ,i.FirstName
                       ,i.LastName
                       ,i.Specialty
                       ,i.SlackHandle";
            string instructorsIncluded = "JOIN Instructor i ON i.Id = se.InstructorId";

            if (filter.include != null && filter.include.Contains("students"))
            {
                sqlSelect = $@"{sqlSelect} {sqlSelectStudents}";
                sqlJoin = $"{sqlJoin} {studentsExerciseIncluded} {studentsIncluded}";
            }

            if (filter.include != null && filter.include.Contains("instructors"))
            {
                sqlSelect = $@"{sqlSelect} {sqlSelectInstructors}";
                sqlJoin = $"{sqlJoin} {studentsExerciseIncluded} {instructorsIncluded}";
            }

            if (filter.q != null)
            {
                sqlWhere = $"{sqlWhere} {isQ}";
            }

            if (filter.language != null)
            {
                sqlWhere = $"{sqlWhere} {isLanguage}";
            }

            string sql = $"{sqlSelect} {sqlFrom} {sqlJoin} {sqlWhere}";
            Console.WriteLine(sql);
            using (IDbConnection conn = Connection)
            {
                if (filter.include == "students")
                {
                    Dictionary<int, Exercise> studentExercises = new Dictionary<int, Exercise>();

                    var fullExercises = await conn.QueryAsync<Exercise, Student, Exercise>(
                        sql,
                        (exercise, student) =>
                        {
                            if (!studentExercises.ContainsKey(exercise.Id))
                            {
                                studentExercises[exercise.Id] = exercise;
                            }
                            studentExercises[exercise.Id].AssignedStudents.Add(student);
                            return exercise;
                        }
                    );
                    return Ok(studentExercises.Values);

                }
                IEnumerable<Exercise> exercises = await conn.QueryAsync<Exercise>(sql);
                return Ok(exercises);
            }
        }

        // GET: api/PaymentType/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PaymentType
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
