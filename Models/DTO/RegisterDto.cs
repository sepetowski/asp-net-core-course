using System.ComponentModel.DataAnnotations;

namespace UdemyCourse.API.Models.DTO
{
    public class RegisterDto
    {
        public required string Username  { get; set; }

        [DataType(DataType.EmailAddress)]
        public required string Email {  get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public required string[] Roles { get; set; }
    }
}
