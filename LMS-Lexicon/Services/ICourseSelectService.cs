using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS_Lexicon.Services
{
    public interface ICourseSelectService
    {
        Task<IEnumerable<SelectListItem>> GetCoursesAsync();
    }
}