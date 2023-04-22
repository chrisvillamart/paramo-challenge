using Sat.Recruitment.Configuration.Configuration;
using Sat.Recruitment.Configuration.Utilities;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Domain.Enum;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;

        public UserRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Task<Result> CreateUser(User user)
        {
             List<User> _users = new List<User>();
             Result result = new Result();
             result.IsSuccess = true;
             result.Errors = new List<Error>();
            if (user.UserType == _appSettings.Normal.ToString() )
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    var gif = user.Money * percentage;
                    user.Money = user.Money + gif;
                }
                if (user.Money < 100)
                {
                    if (user.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = user.Money * percentage;
                        user.Money = user.Money + gif;
                    }
                }
            }
            if (user.UserType == _appSettings.SuperUser.ToString())
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = user.Money * percentage;
                    user.Money = user.Money + gif;
                }
            }
            if (user.UserType == _appSettings.Premium.ToString())
            {
                if (user.Money > 100)
                {
                    var gif = user.Money * 2;
                    user.Money = user.Money + gif;
                }
            }


            var reader = FileReader.ReadUsersFromFile();

            user.Email = StringUtils.NormalizeEmail(user.Email);
            
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var User = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(User);
            }
            reader.Close();

            foreach (var userList in _users)
            {
                var duplicationMessage = _appSettings.UserDuplicated.ToString();
                if (user.Email == userList.Email || user.Phone == userList.Phone)
                {
                    Debug.WriteLine($"{duplicationMessage} {user.Email} {user.Phone} ");
                    result.IsSuccess = false;
                    result.Errors.Add(new Error(ErrorTypes.Duplication, _appSettings.UserDuplicated.ToString()));
                }
                else if (user.Name == userList.Name)
                {
                    if (user.Address == userList.Address)
                    {
                        Debug.WriteLine($"{duplicationMessage} {user.Name} {user.Address} ");
                        result.IsSuccess = false;
                        result.Errors.Add(new Error(ErrorTypes.Duplication, _appSettings.UserDuplicated.ToString()));
                    }

                }
            }

            if (result.IsSuccess)
            {
                Debug.WriteLine(_appSettings.UserCreated.ToString());
            }


            return Task.FromResult(result); 
        }
    }
}
