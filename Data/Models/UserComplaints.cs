namespace TaskSched.Data.Models
{
    public class UserComplaints
    {
        public UserComplaints()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
