using System.ComponentModel.DataAnnotations;

namespace UdemyCourse.API.Models.DTO
{
    public class LoginDto
    {
      
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}

