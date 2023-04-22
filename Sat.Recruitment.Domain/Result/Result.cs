using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public List<Error> Errors { get; set; }
    }
}
