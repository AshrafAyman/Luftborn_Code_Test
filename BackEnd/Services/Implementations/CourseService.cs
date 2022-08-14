using Data.Context;
using Data.Models;
using Data.UIModels;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly SystemContext context;

        public CourseService(SystemContext _context)
        {
            context = _context;
        }

        public ResponseDto AddNewCourse(CourseDto course)
        {
            var newCourse = new Course()
            {
                CourseName = course.CourseName,
                CourseHours = course.CourseHours,
                CourseDescription = course.CourseDescription,
                Department = course.Department,
                InstructorId = course.InstructorId,
            };
            context.Courses.Add(newCourse);
            context.SaveChanges();
            var response = new ResponseDto()
            {
                Status = true,
                Message = $"Course: {course.CourseName} added successfully",
            };
            return response;
        }

        public ResponseDto EditCourse(CourseDto course)
        {
            var response = new ResponseDto();
            var courseToEdit = context.Courses.FirstOrDefault(e => e.Id == course.CourseId);
            if (courseToEdit != null)
            {
                courseToEdit.CourseName = course.CourseName;
                courseToEdit.CourseHours = course.CourseHours;
                courseToEdit.CourseDescription = course.CourseDescription;
                courseToEdit.Department = course.Department;
                courseToEdit.InstructorId = course.InstructorId;
                context.SaveChanges();

                // set response message in success
                response.Status = true;
                response.Message = $"Course: {course.CourseName} edited successfully";
                return response;
            }
            // set response message in failed
            response.Status = false;
            response.Message = "Can't edit this course";

            return response;
        }

        public IEnumerable<CourseDto> GetCoursesList(int instructorId)
        {
            var courses = context.Courses.Include(nameof(Instructor)).Where(e => e.InstructorId == instructorId).Select(a => new CourseDto
            {
                CourseId = a.Id,
                InstructorId = a.InstructorId,
                CourseName = a.CourseName,
                Department = a.Department,
                CourseHours = a.CourseHours,
                CourseDescription = a.CourseDescription,
                InstructorName = a.Instructor.Name
            }).AsEnumerable();

            return courses.Count() > 0 ? courses : Enumerable.Empty<CourseDto>();
        }

        public CourseDto GetCourse(int id)
        {
            var course = context.Courses.Where(e => e.Id == id).Select(a => new CourseDto
            {
                CourseId = a.Id,
                InstructorId = a.InstructorId,
                CourseName = a.CourseName,
                Department = a.Department,
                CourseHours = a.CourseHours,
                CourseDescription = a.CourseDescription,
                InstructorName = a.Instructor.Name
            }).FirstOrDefault();

            return course ?? new CourseDto();
        }

        public ResponseDto DeleteCourse(int id)
        {
            var response = new ResponseDto();
            var course = context.Courses.FirstOrDefault(e => e.Id == id);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();

                // set response message in success
                response.Status = true;
                response.Message = $"Course deleted successfully";
                return response;
            }

            // set response message in failed
            response.Status = false;
            response.Message = "Can't delete this course";

            return response;
        }
    }
}
