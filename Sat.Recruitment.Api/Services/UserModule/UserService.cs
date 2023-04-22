using Microsoft.Extensions.Options;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Enum;
using Sat.Recruitment.Configuration.Configuration;
using System.Net;
using System.Xml.Linq;
using System;
using AutoMapper;
using Sat.Recruitment.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services.UserModule
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserService(IOptions<AppSettings> appSettings, IMapper mapper, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ResultDTO> InsertUser(UserDTO userDTO)
        {
            var resultDto = new ResultDTO();
            resultDto.IsSuccess = true;
            resultDto.Errors = new List<ErrorDTO>();
            if (userDTO.Name == null)
            {
                resultDto.Errors.Add(new ErrorDTO(ErrorTypes.Name, _appSettings.NameError.ToString()));
                resultDto.IsSuccess = false;
            }
               
            if (userDTO.Email == null)
            {
                resultDto.Errors.Add(new ErrorDTO(ErrorTypes.Name, _appSettings.EmailError.ToString()));
                resultDto.IsSuccess = false;
            }
                
            if (userDTO.Address == null)
            {
                resultDto.Errors.Add(new ErrorDTO(ErrorTypes.Name, _appSettings.AddressError.ToString()));
                resultDto.IsSuccess = false;
            }
            if (userDTO.Phone == null)
            { 
                resultDto.Errors.Add(new ErrorDTO(ErrorTypes.Name, _appSettings.PhoneError.ToString()));
                resultDto.IsSuccess = false;
            }

             
            if(resultDto.IsSuccess)
            {
                var userEntity = _mapper.Map<User>(userDTO);
                Result entityResult = await _userRepository.CreateUser(userEntity);
                resultDto = _mapper.Map<ResultDTO>(entityResult);
            }


            return resultDto;
        }
    }
}
