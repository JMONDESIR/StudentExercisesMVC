﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Cohort
    {
        public int Id { get; set; } 
        [Required]
        [Display(Name = "Cohort Name")]
        public string CohortName { get; set; }

    }
}
