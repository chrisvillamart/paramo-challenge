using Sat.Recruitment.Api.DTO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.UserModule
{
    public interface IUserService
    { 
        Task<ResultDTO> InsertUser(UserDTO userDTO);
        
    }
}
