﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string InstructorName { get; set; }
        public string SlackHandle { get; set; }
        public string Specialty { get; set; }
        public int CohortId { get; set; }
    }
}
