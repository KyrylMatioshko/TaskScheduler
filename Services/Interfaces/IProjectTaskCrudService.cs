using TaskSched.Data.Models;

namespace TaskSched.Services.Interfaces
{
    public interface IProjectTaskCrudService
    {
        public IQueryable<User> GetUserByUserName(string userName);
        public IQueryable<Project> GetProjectById(string userName, Guid projectId);

        public Task<bool> AddProjectAsync(string userName, Project newProject);

        public IQueryable<Project> GetProjects(string userName);

        public Task<bool> EditProjectAsync(string userName, Guid projectId, Project updatedProject);

        public Task<bool> DeleteProjectAsync(string userName, Guid projectId);

        public IQueryable<Data.Models.Task> GetTaskById(string userName, Guid projectId, Guid taskId);

        public Task<bool> AddTaskAsync(string userName, Guid projectId, Data.Models.Task newTask);

        public IQueryable<Data.Models.Task> GetTasks(string userName, Guid projectId);

        public Task<bool> EditTaskAsync(string userName, Guid projectId, Guid taskId, Data.Models.Task updatedTask);

        public Task<bool> DeleteTaskAsync(string userName, Guid projectId, Guid taskId);

        public Task<bool> ProjectIsNullAsync(Guid projectId, string userName);

        public Task<bool> TaskIsNullAsync(Guid projectId, Guid taskId, string userName);

        public System.Threading.Tasks.Task InitialiseProjectDisplayStateForOldUsersAsync(Guid projectId);

        public IQueryable<ProjectDisplayState> GetProjectDisplayStateAsync(string userName, Guid projectId);

        public Task<bool> UpdateProjectDisplayStateAsync(string userName, Guid projectId, ProjectDisplayState updatedDisplaySettings);




	}
}
