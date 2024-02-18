using System.ComponentModel.DataAnnotations;
using TaskSched.Data.Enums;

namespace TaskSched.Data.Models
{
    public class Task
    {
        public Task()
        {
            TaskId = Guid.NewGuid();
        }
        public Guid TaskId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Priority Priority { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime DateEnd { get; set; }
        public DateTime TimeEnd { get; set; }
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }
}
