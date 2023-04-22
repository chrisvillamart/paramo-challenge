using Sat.Recruitment.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain
{
    public class Error
    {
        public Error(ErrorTypes type, string message)
        {
            this.Type = type;
            this.Message = message;
        }

        public ErrorTypes Type { get; set; }
        public string Message { get; set; }
    }
}
