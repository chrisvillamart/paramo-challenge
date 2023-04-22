using System.Collections.Generic;

namespace Sat.Recruitment.Api.DTO
{
    public class ResultDTO
    {
        public bool IsSuccess { get; set; }
        public List<ErrorDTO> Errors { get; set; }
    }

}
