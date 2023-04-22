using Sat.Recruitment.Api.Enum;

namespace Sat.Recruitment.Api.DTO
{
    public class ErrorDTO
    { 
        public ErrorDTO(ErrorTypes type, string message)
        {
            this.Type = type;
            this.Message = message;
        }

        public ErrorTypes Type { get; set; }
        public string Message { get; set; }
    }
}
