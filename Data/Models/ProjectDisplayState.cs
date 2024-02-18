using TaskSched.Services.Enums;

namespace TaskSched.Data.Models
{
	public class ProjectDisplayState
	{
		public ProjectDisplayState()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public Guid ProjectId { get; set; }
		public SortDirection SortDirection { get; set; }
		public SortParams SortParams { get; set; }
		public Project Project { get; set; } = null!;
	}
}
