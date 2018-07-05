using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLKnowledgeBase.Models;

namespace YLKnowledgeBase.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        ApplicationUser GetUser(Guid id);
        void ChangeRole();
        void ToDeleteUser();
        bool UserExists(Guid id);
        void Save();
    }
}
