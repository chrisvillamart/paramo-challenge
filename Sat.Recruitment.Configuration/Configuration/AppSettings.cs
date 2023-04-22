using System;

namespace Sat.Recruitment.Configuration.Configuration
{
    public class AppSettings
    {
        public AppSettings() { }
        public AppSettings(string nameError, string emailError, string addressError, string phoneError, object normal, object superUser, object premium, object userCreated, object userDuplicated)
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
        }
        public string NameError { get; set; }
        public string EmailError { get; set; }
        public string AddressError { get; set; }
        public string PhoneError { get; set; }
        public object Normal { get; set; }
        public object SuperUser { get; set; }
        public object Premium { get; set; }
        public object UserCreated { get; set; }
        public object UserDuplicated { get; set; }
    }
}
