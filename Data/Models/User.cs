using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskSched.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
            

        }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public override string? UserName { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();

        public List<UserComplaints> UserComplaints { get; set; } = new List<UserComplaints>();
    }
}
