using TaskSched.Services.Enums;
using TaskScheduler.Services.SortService.Interfaces;
using Task = TaskSched.Data.Models.Task;

namespace TaskSched.Services.SortService
{
    public class ProjectTaskSortService<T> : ISortService<T> where T : Task
    {
        public IEnumerable<T> Sort(IEnumerable<T> tasks, SortDirection direction, SortParams sortParams)
        {
            switch (sortParams)
            {
                case SortParams.Name:
                    if (direction == SortDirection.Ascending)
                        tasks = tasks.OrderBy(t => t.Name)
                            .ThenByDescending(t => t.Priority)
                            .ThenByDescending(t => t.DateCreate)
                            .ThenByDescending(t => t.DateEnd);
                    else
                        tasks = tasks.OrderByDescending(t => t.Name)
                            .ThenByDescending(t => t.Priority)
                            .ThenByDescending(t => t.DateCreate)
                            .ThenByDescending(t => t.DateEnd);
                    break;

                case SortParams.EndDate:
                    if (direction == SortDirection.Ascending)
                        tasks = tasks.OrderBy(t => t.DateEnd)
                            .ThenByDescending(t => t.Priority)
                            .ThenByDescending(t => t.DateCreate);
                    else
                        tasks = tasks.OrderByDescending(t => t.DateEnd)
                            .ThenByDescending(t => t.Priority)
                            .ThenByDescending(t => t.DateCreate);
                    break;

                case SortParams.Priority:
                    if (direction == SortDirection.Ascending)
                        tasks = tasks.OrderBy(t => t.Priority)
                            .ThenByDescending(t => t.DateCreate)
                            .ThenByDescending(t => t.DateEnd);
                    else
                        tasks = tasks.OrderByDescending(t => t.Priority)
                            .ThenByDescending(t => t.DateCreate)
                             .ThenByDescending(t => t.DateEnd);
                    break;

                default:
                    if (direction == SortDirection.Ascending)
                        tasks = tasks.OrderBy(t => t.DateCreate)
                            .ThenByDescending(t => t.Priority)
                            .ThenByDescending(t => t.DateEnd);
                    else
                        tasks = tasks.OrderByDescending(t => t.DateCreate)
                            .ThenByDescending(t => t.Priority)
                            .ThenByDescending(t => t.DateEnd);
                    break;
            }

            return tasks;
        }
    }
}
