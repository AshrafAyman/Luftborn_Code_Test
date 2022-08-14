using Data.UIModels;
using Infrastructure.Configuration;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SimpleCoursesCenterAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(ConstantHelper.InstructorRole)]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IInstructorService instructorService;
        private readonly ISecurity security;

        public CourseController(ICourseService _courseService, IInstructorService _instructorService, ISecurity _security)
        {
            courseService = _courseService;
            instructorService = _instructorService;
            security = _security;
        }
        [HttpPost("AddCourse")]
        public IActionResult AddCourse(CourseDto course)
        {
            var result = courseService.AddNewCourse(course);
            return Ok(result);
        }

        [HttpPut("EditCourseData")]
        public IActionResult EditCourseData(CourseDto course)
        {
            var result = courseService.EditCourse(course);
            return Ok(result);
        }

        [HttpGet("GetAllCourses")]
        public IActionResult GetCoursesList()
        {
            var userId = security.GetUserIdFromToken();
            var instructorId = instructorService.GetInstructorIdFromUserId(userId);
            var result = courseService.GetCoursesList(instructorId);
            return Ok(result);
        }

        [HttpGet("GetCourse/{id}")]
        public IActionResult GetCourseById(int id)
        {
            var result = courseService.GetCourse(id);
            return Ok(result);
        }

        [HttpDelete("DeleteCourse/{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var result = courseService.DeleteCourse(id);
            return Ok(result);
        }
    }
}
