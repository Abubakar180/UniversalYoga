using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.Models;

namespace UniversalYoga.Services.Interface
{
    public interface ICourses
    {
        Task<bool> BookCourse(YogaCourse model);
        Task<List<YogaCourse>> GetAllCoursesAsync();
        Task<List<YogaCourse>> GetBookedCoursesAsync();
    }
}
