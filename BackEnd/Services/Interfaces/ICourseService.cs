using Data.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICourseService
    {
        ResponseDto AddNewCourse(CourseDto course);
        ResponseDto DeleteCourse(int id);
        ResponseDto EditCourse(CourseDto course);
        CourseDto GetCourse(int id);
        IEnumerable<CourseDto> GetCoursesList(int instructorId);
    }
}
