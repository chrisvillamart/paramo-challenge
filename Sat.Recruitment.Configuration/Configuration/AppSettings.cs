using System;

namespace Sat.Recruitment.Configuration.Configuration
{
    public class AppSettings
    {
        public AppSettings() { }
        public AppSettings(string nameError, string emailError, string addressError, string phoneError, string normal, string superUser, string premium, string userCreated, string userDuplicated, string fileRoute, string emailFormatError)
        {
            NameError = nameError;
            EmailError = emailError;
            AddressError = addressError;
            PhoneError = phoneError;
            Normal = normal;
            SuperUser = superUser;
            Premium = premium;
            UserCreated = userCreated;
            UserDuplicated = userDuplicated;
            FileRoute = fileRoute;
            EmailFormatError = emailFormatError;
        }
        public string NameError { get; set; }
        public string EmailError { get; set; }
        public string AddressError { get; set; }
        public string PhoneError { get; set; }
        public string Normal { get; set; }
        public string SuperUser { get; set; }
        public string Premium { get; set; }
        public string UserCreated { get; set; }
        public string UserDuplicated { get; set; }
        public string FileRoute { get; set; }
        public string EmailFormatError { get; set; }
    }
}
