using System.ComponentModel.DataAnnotations;

namespace TaskSched.Data.Models
{
    public class Project
    {
        public Project()
        {
            ProjectId = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();
        public User User { get; set; } = null!;
        public ProjectDisplayState ProjectDisplayState { get; set; } = null!;
	}
}
