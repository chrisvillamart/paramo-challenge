using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain
{
    public interface IUserRepository
    {
        public Task<Result> CreateUser(User user);
    }
}
