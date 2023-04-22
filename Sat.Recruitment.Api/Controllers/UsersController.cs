using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Services.UserModule;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private IUserService _userService;
         
        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        { 
            var resultDTO = await _userService.InsertUser(userDTO);
            return Ok(resultDTO);
        } 
    }

}
