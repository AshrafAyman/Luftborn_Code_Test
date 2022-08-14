using Data.Context;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class InstructorService : IInstructorService
    {
        private readonly SystemContext context;

        public InstructorService(SystemContext _context)
        {
            context = _context;
        }

        public int GetInstructorIdFromUserId(int userId)
        {
            var user = context.Instructors.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                return user.Id;
            }
            return 0;
        }
    }
}
