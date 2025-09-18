using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Model;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Runtime.CompilerServices;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _context.Students.Where(Student => Student.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost("AddStudent")]
        public async Task<ActionResult> AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.Where(Student => Student.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("UpdateStudent")]

        public async Task<ActionResult<Student>> UpdateStudent(Student updateStudent)
        {
            var dbStudent = await _context.Students.FindAsync(updateStudent.Id);
            if (dbStudent == null)
            {
                return NotFound("Student not found...");
            }
            dbStudent.Name = updateStudent.Name;
            dbStudent.Email = updateStudent.Email;

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());

        }
    }
}
