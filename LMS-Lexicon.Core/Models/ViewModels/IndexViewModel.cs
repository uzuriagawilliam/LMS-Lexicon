using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class IndexViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string CourseName { get; set; }

        public IEnumerable<IndexUsersViewModel> UserList { get; set; }

    }
}
