﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using firstSprint.Models;

namespace BangazonAPI.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MaxAttendees { get; set; }

        public List<Employees> AttendingEmployees = new List<Employees>();
    }
}

/* 
 AUTHOR: Aaron Miller
 PURPOSE: Model reflects TrainingProgram Table
*/
