using System.ComponentModel.DataAnnotations.Schema;
using Usermanagement.Domain.Abstraction;

namespace Usermanagement.Domain.Entities.UserDetails
{
    [Table("Users", Schema = "Usermanagement")]
    public class User : Aggregate<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Username { get; set; }

    }
}
