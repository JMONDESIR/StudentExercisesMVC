using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int CohortId { get; set; }
        public string SlackHandle { get; set; }
    }
}
