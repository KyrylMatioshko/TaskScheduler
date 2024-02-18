using TaskSched.Services.Enums;

namespace TaskSched.ViewModels
{
    public class TaskSortViewModel
    {
        public Guid ProjectId { get; set; }
        public SortDirection SortDirection { get; set; }
        public SortParams SortParams { get; set; }
    }
}
