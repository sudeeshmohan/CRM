using System.ComponentModel.DataAnnotations;

namespace Usermanagement.Application.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "First Name Required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email Address Required")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string? Username { get; set; }
    }
}
