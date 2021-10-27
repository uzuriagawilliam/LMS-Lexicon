using System.Threading.Tasks;

namespace LMS_Lexicon.Services
{
    public interface ICourseIdForUserService
    {
        Task<int?> GetCoursesIdForUserAsync();
    }
}