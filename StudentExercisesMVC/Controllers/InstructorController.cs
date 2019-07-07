using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Models.ViewModels;

namespace StudentExercisesMVC.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IConfiguration _config;

        public InstructorController(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Instructor
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.InstructorName,
                s.SlackHandle,
                s.Specialty,
                s.CohortId
            FROM Instructors s
        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InstructorName = reader.GetString(reader.GetOrdinal("InstructorName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            Specialty = reader.GetString(reader.GetOrdinal("Specialty")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };

                        instructors.Add(instructor);
                    }

                    reader.Close();

                    return View(instructors);
                }
            }
        }

        // GET: Instructor/Details/5
        public ActionResult Details(int Id)
        {
            try
            {
                Instructor Instructor = GetInstructorById(Id);
                return View(Instructor);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Instructor/Create
        public ActionResult Create()
        {
            InstructorCreateViewModel InstructorCreateViewModel = new InstructorCreateViewModel(_config.GetConnectionString("DefaultConnection"));

            return View(InstructorCreateViewModel);
        }

        // POST: Instructor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InstructorCreateViewModel model)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Instructors
                ( InstructorName, SlackHandle, Specialty, CohortId )
                VALUES
                ( @InstructorName, @SlackHandle, @Specialty, @CohortId )";
                    cmd.Parameters.Add(new SqlParameter("@InstructorName", model.Instructor.InstructorName));
                    cmd.Parameters.Add(new SqlParameter("@SlackHandle", model.Instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@Specialty", model.Instructor.Specialty));
                    cmd.Parameters.Add(new SqlParameter("@CohortId", model.Instructor.CohortId));
                    await cmd.ExecuteNonQueryAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: Instructor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Instructor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Instructor/Delete/5
        public ActionResult Delete(int Id)
        {
            try
            {
                Instructor Instructor = GetInstructorById(Id);
                return View(Instructor);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int Id)
        {
            try
            {
                // TODO: Add delete logic here

                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM Instructors WHERE Id = @Id";
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        cmd.ExecuteNonQuery();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                return View();
            }
        }
        private Instructor GetInstructorById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.InstructorName,
                s.SlackHandle,
                s.Specialty,
                s.CohortId
            FROM Instructors s
            WHERE Id = @Id";

                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Instructor Instructor = null;

                    if (reader.Read())
                    {
                        Instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InstructorName = reader.GetString(reader.GetOrdinal("InstructorName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            Specialty = reader.GetString(reader.GetOrdinal("Specialty")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };
                        reader.Close();
                        return Instructor;
                    }
                    else
                    {
                        reader.Close();
                        return Instructor;
                    }
                }
            }
        }
    }
}