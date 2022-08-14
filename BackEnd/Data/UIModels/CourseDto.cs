using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UIModels
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseHours { get; set; }
        public string Department { get; set; }
        public int InstructorId { get; set; }
        public string? InstructorName { get; set; }
    }
}
