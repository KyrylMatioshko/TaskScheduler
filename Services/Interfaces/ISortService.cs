using TaskSched.Services.Enums;

namespace TaskScheduler.Services.SortService.Interfaces
{
    public interface ISortService<T> where T : class
    {
        public IEnumerable<T> Sort(IEnumerable<T> tasks, SortDirection direction, SortParams sortParams);
    }
}
