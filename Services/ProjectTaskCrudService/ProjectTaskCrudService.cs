using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskSched.Data.Context;
using TaskSched.Data.Models;
using TaskSched.Services.Interfaces;
using Task = TaskSched.Data.Models.Task;

namespace TaskSched.Services.ProjectTaskCrudService
{
    public class ProjectTaskCrudService : IProjectTaskCrudService
    {
        private readonly TaskSchedulerContext _context;

        public ProjectTaskCrudService(TaskSchedulerContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetUserByUserName(string userName)
        {
            var user = _context.Users
            .Where(u => u.UserName == userName);

            return user;
        }
        public IQueryable<Project> GetProjectById(string userName, Guid projectId)
        {
            var project = GetUserByUserName(userName)
                .Include(u => u.Projects)
                .SelectMany(u => u.Projects)
                .Where(p => p.ProjectId == projectId);

            return project;
        }

        public IQueryable<Project> GetProjects(string userName)
        {
            var projects = GetUserByUserName(userName)
               .Include(u => u.Projects)
               .SelectMany(u => u.Projects).OrderBy(u => u.DateCreated);

            return projects;
        }

        public async Task<bool> AddProjectAsync(string userName, Project newProject)
        {
            var currentUser = await GetUserByUserName(userName)
            .Include(u => u.Projects)
            .FirstOrDefaultAsync();

            if (currentUser == null) return false;

            currentUser.Projects.Add(newProject);

            int result = _context.SaveChanges();

            return Convert.ToBoolean(result);
        }
        public async Task<bool> DeleteProjectAsync(string userName, Guid projectId)
        {
            var deleteResult = await GetProjectById(userName, projectId).ExecuteDeleteAsync();
            return Convert.ToBoolean(deleteResult);
        }
        public async Task<bool> EditProjectAsync(string userName, Guid projectId, Project updatedProject)
        {

            int result = await GetProjectById(userName, projectId)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.Name, p => updatedProject.Name));

            return Convert.ToBoolean(result);
        }

        public async Task<bool> AddTaskAsync(string userName, Guid projectId, Task newTask)
        {
            var project = await GetProjectById(userName, projectId)
            .Include(p => p.Tasks).
            FirstOrDefaultAsync();

            if (project == null) return false;

            project.Tasks.Add(newTask);

            int result = _context.SaveChanges();

            return Convert.ToBoolean(result);
        }


        public async Task<bool> DeleteTaskAsync(string userName, Guid projectId, Guid taskId)
        {
            var resultDelete = await GetTaskById(userName, projectId, taskId).ExecuteDeleteAsync();

            return Convert.ToBoolean(resultDelete);
        }


        public async Task<bool> EditTaskAsync(string userName, Guid projectId, Guid taskId, Task updatedTask)
        {
            var result = await GetTaskById(userName, projectId, taskId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Name, t => updatedTask.Name)
                .SetProperty(t => t.Description, t => updatedTask.Description)
                .SetProperty(t => t.Priority, t => updatedTask.Priority)
                .SetProperty(t => t.DateEnd, t => updatedTask.DateEnd)
                .SetProperty(t => t.TimeEnd, t => updatedTask.TimeEnd));

            return Convert.ToBoolean(result);
        }


        public IQueryable<Task> GetTaskById(string userName, Guid projectId, Guid taskId)
        {
            var task = GetProjectById(userName, projectId)
                .Include(p => p.Tasks)
                .SelectMany(p => p.Tasks)
                .Where(t => t.TaskId == taskId);

            return task;
        }

        public IQueryable<Task> GetTasks(string userName, Guid projectId)
        {
            var tasks = GetProjectById(userName, projectId)
                .Include(p => p.Tasks).
                SelectMany(p => p.Tasks);

            return tasks;
        }

        public async Task<bool> ProjectIsNullAsync(Guid projectId, string userName)
        {
            var project = await GetProjectById(userName, projectId).FirstOrDefaultAsync();
            return project == null;
        }

        public async Task<bool> TaskIsNullAsync(Guid projectId, Guid taskId, string userName)
        {
            var project = await GetTaskById(userName, projectId, taskId).FirstOrDefaultAsync();
            return project == null;
        }

        public async System.Threading.Tasks.Task InitialiseProjectDisplayStateForOldUsersAsync(Guid projectId)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectDisplayState)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (project != null && project.ProjectDisplayState == null)
            {
                project.ProjectDisplayState = new ProjectDisplayState();
                var result = await _context.SaveChangesAsync();
            }
        }

		public IQueryable<ProjectDisplayState> GetProjectDisplayStateAsync(string userName, Guid projectId)
		{
			var displayState = GetProjectById(userName, projectId)
				.Include(p => p.ProjectDisplayState)
				.Select(p => p.ProjectDisplayState);

			return displayState;
		}


		public async Task<bool> UpdateProjectDisplayStateAsync(string userName, Guid projectId, ProjectDisplayState updatedDisplaySettings)
		{
			int result = await GetProjectDisplayStateAsync(userName, projectId)
				.ExecuteUpdateAsync(s => s
						.SetProperty(ds => ds.SortParams, ds => updatedDisplaySettings.SortParams)
						.SetProperty(ds => ds.SortDirection, ds => updatedDisplaySettings.SortDirection));

			return result > 0;
		}
	}
}
