<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> master
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
    public class TrainingProgramsController : ControllerBase
    {
        // GET: api/TrainingPrograms
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TrainingPrograms/5
        [HttpGet("{id}", Name = "GetTrainingPrograms")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TrainingPrograms
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/TrainingPrograms/5
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
<<<<<<< HEAD
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
    public class TrainingProgramsController : ControllerBase
    {
        // GET: api/TrainingPrograms
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TrainingPrograms/5
        [HttpGet("{id}", Name = "GetTrainingPrograms")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TrainingPrograms
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/TrainingPrograms/5
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
>>>>>>> master
