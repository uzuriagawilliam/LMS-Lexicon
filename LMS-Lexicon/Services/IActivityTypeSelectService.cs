using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS_Lexicon.Services
{
    public interface IActivityTypeSelectService
    {
        Task<IEnumerable<SelectListItem>> GetActivityTypeAsync();
    }
}