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

    //--- authorize this controller for only users with instructor role
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

        //--- add new course action
        [HttpPost("AddCourse")]
        public IActionResult AddCourse(CourseDto course)
        {
            var result = courseService.AddNewCourse(course);
            return Ok(result);
        }

        //--- edit course action
        [HttpPut("EditCourseData")]
        public IActionResult EditCourseData(CourseDto course)
        {
            var result = courseService.EditCourse(course);
            return Ok(result);
        }

        //--- get courses related to specific instructor 
        [HttpGet("GetAllCourses")]
        public IActionResult GetCoursesList()
        {
            //--- get user id first from token
            var userId = security.GetUserIdFromToken();
            //--- get instructor id from it's user id
            var instructorId = instructorService.GetInstructorIdFromUserId(userId);
            //--- get all courses for this instructor
            var result = courseService.GetCoursesList(instructorId);
            return Ok(result);
        }

        //--- get specific course
        [HttpGet("GetCourse/{id}")]
        public IActionResult GetCourseById(int id)
        {
            var result = courseService.GetCourse(id);
            return Ok(result);
        }

        //--- delete specific course
        [HttpDelete("DeleteCourse/{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var result = courseService.DeleteCourse(id);
            return Ok(result);
        }
    }
}
