using Sat.Recruitment.Configuration.Configuration;
using Sat.Recruitment.Configuration.Utilities;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Domain.Enum;
using System.Threading.Tasks;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

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
             Result result = new Result();
             result.IsSuccess = true;
             result.Errors = new List<Error>();


             StringUtils.NormalizeEmail(user.Email);

             var gift = CalculateGift(user);
             user.Money += gift;
             
            var rowList = FileReader.ReadFromFile(',', _appSettings.FileRoute);
            var _users = new List<User>();

            foreach (var row in rowList) {
                var userLine = new User();
                userLine.Name = row[0].ToString();
                userLine.Email = row[1].ToString();
                userLine.Phone = row[2].ToString();
                userLine.Address = row[3].ToString();
                userLine.UserType = row[4].ToString();
                userLine.Money = decimal.Parse(row[5].ToString());
                _users.Add(userLine);
            }
              
            foreach (var userList in _users)
            {   
                var duplicationMessage = _appSettings.UserDuplicated;
                if (user.Email == userList.Email || user.Phone == userList.Phone)
                {
                    Debug.WriteLine($"{duplicationMessage} {user.Email} {user.Phone} ");
                    result.IsSuccess = false;
                    result.Errors.Add(new Error(ErrorTypes.Duplication, _appSettings.UserDuplicated));
                }
                else if (user.Name == userList.Name)
                {
                    if (user.Address == userList.Address)
                    {
                        Debug.WriteLine($"{duplicationMessage} {user.Name} {user.Address} ");
                        result.IsSuccess = false;
                        result.Errors.Add(new Error(ErrorTypes.Duplication, _appSettings.UserDuplicated));
                    }

                }
            }

            if (result.IsSuccess)
            {
                Debug.WriteLine(_appSettings.UserCreated);
            }


            return Task.FromResult(result); 
        }

        private decimal CalculateGift(User user)
        {

            decimal gif = 0;
            var rules = new List<Func<User, decimal>>
            {
                (u) => u.UserType == _appSettings.Normal && u.Money > 100 ? u.Money * 0.12m :  0,

                (u) => u.UserType == _appSettings.Normal && (u.Money < 100 && u.Money > 10)  ? u.Money * 0.8m : 0,

                (u) => u.UserType == _appSettings.SuperUser && u.Money > 100 ? u.Money * 0.20m : 0,

                (u) => u.UserType == _appSettings.Premium && u.Money > 100 ? u.Money * 2 : 0
            };

            foreach (var rule in rules)
            {
                gif += rule(user);
            }
            return gif;
        }
    }
}
