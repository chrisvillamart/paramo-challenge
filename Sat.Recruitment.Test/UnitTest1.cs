using System;
using System.Dynamic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Options;
using Moq;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Services.UserModule;
using Sat.Recruitment.Configuration.Configuration;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Infraestructure.Repositories;
using Xunit;
using static Sat.Recruitment.Api.Startup;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly UsersController _controller;
        private readonly IUserService _userService;
        public UnitTest1( )
        { 

            // Crea una instancia de UserService
            var appSettings = Options.Create(new AppSettings("The name is required", "The email is required",
                "The address is required", "The phone is required", "Normal", "SuperUser", "Premium", "User Created" , "User Duplicated", "/Resources/Users.txt", "The email format is incorrect"));
            var _userRepository = new UserRepository(appSettings);
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile()));
            var mapper = new Mapper(mapperConfig);
            _userService = new UserService(appSettings, mapper, _userRepository);

            // Crea una instancia de UserController
            _controller = new UsersController(_userService);
        }
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<UserDTO, User>();
                CreateMap<ResultDTO, Result>();
                CreateMap<ErrorDTO, Error>();

            } 
        }

        [Fact]
        public async Task Test1()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Mike";
            userDTO.Email = "mike@gmail.com";
            userDTO.Phone = "+349 1122354215";
            userDTO.Address = "Av. Juan G";
            userDTO.UserType = "Normal";
            userDTO.Money = 124;
              
            var result = await _controller.CreateUser(userDTO);


            var okResult = result as OkObjectResult;
            var resultDtoValue = okResult.Value as ResultDTO;

            Assert.True(resultDtoValue.IsSuccess);
            Assert.Empty(resultDtoValue.Errors);
        }

        [Fact]
        public async Task Test2()
        { 
            UserDTO userDTO = new UserDTO();
            userDTO.Name = "Agustina";
            userDTO.Email = "Agustina@gmail.com";
            userDTO.Phone = "+349 1122354215";
            userDTO.Address = "Av. Juan G";
            userDTO.UserType = "Normal";
            userDTO.Money = 124;

            var result = await _controller.CreateUser(userDTO);

            var okResult = result as OkObjectResult;
            var resultDtoValue = okResult.Value as ResultDTO;

            Assert.False(resultDtoValue.IsSuccess);
            Assert.Contains(resultDtoValue.Errors, error => error.Message == "User Duplicated");
        }
    }
}
