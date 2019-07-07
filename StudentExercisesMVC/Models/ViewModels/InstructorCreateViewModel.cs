using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class InstructorCreateViewModel
    {
        private string _connectionString;

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
        public List<SelectListItem> Cohorts { get; set; }
        public Instructor Instructor { get; set; }

        public InstructorCreateViewModel() { }
        public InstructorCreateViewModel(string connectionString)
        {
            _connectionString = connectionString;

            Cohorts = GetAllCohorts()
                .Select(c => new SelectListItem
                {
                    Text = c.CohortName,
                    Value = c.Id.ToString()
                })
                .ToList();

            Cohorts
                .Insert(0, new SelectListItem
                {
                    Text = "Choose cohort...",
                    Value = "0"
                });

        }

        private List<Cohort> GetAllCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, CohortName FROM Cohort";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        cohorts.Add(new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CohortName = reader.GetString(reader.GetOrdinal("CohortName")),
                        });
                    }
                    reader.Close();
                    return cohorts;
                }
            }
        }
    }

}
