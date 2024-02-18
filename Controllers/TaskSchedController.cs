using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskSched.Data.Models;
using TaskSched.Services.Enums;
using TaskSched.Services.Interfaces;
using TaskSched.ViewModels;
using TaskScheduler.Services.SortService.Interfaces;
using Task = TaskSched.Data.Models.Task;

namespace TaskSched.Controllers
{
    [Authorize]
    public class TaskSchedController : Controller
    {
        private readonly IProjectTaskCrudService _projectTaskService;
        private readonly IMapper _mapper;
        public TaskSchedController(IProjectTaskCrudService projectTaskService, IMapper mapper)
        {
            _projectTaskService = projectTaskService;
            _mapper = mapper;
        }

        [HttpGet("{action=index/projectId}")]
        public async Task<IActionResult> Index(Guid? projectId)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            var userProject = await _projectTaskService.GetProjects(username).ToListAsync();

            var projectViewModel = _mapper.Map<IEnumerable<ProjectViewModel>>(userProject);

            if (projectId != null)
                ViewBag.projectId = projectId;

			return View(projectViewModel);
        }

        [HttpGet("tasks/{projectId}")]
        public async Task<IActionResult> Tasks(Guid projectId, [FromServices] ISortService<Task> taskService)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            var userProjects = await _projectTaskService.GetProjects(username).OrderBy(a => a.DateCreated).ToListAsync();

            var projectViewModel = _mapper.Map<IEnumerable<ProjectViewModel>>(userProjects);

            var currentProject = await _projectTaskService.
                GetProjectById(username, projectId)
                .Include(p => p.Tasks)
                .Include(p => p.ProjectDisplayState)
                .FirstOrDefaultAsync();

            if(currentProject == null) return NotFound();

			await _projectTaskService.InitialiseProjectDisplayStateForOldUsersAsync(projectId);

			var sortedTask = taskService.Sort(currentProject.Tasks, currentProject.ProjectDisplayState.SortDirection, currentProject.ProjectDisplayState.SortParams);

            ViewBag.userTasks = sortedTask;
            ViewBag.projectId = projectId;
            ViewBag.сulture = new CultureInfo("uk-UA");

            return View(projectViewModel);
        }

        [HttpGet("add-project")]
        public IActionResult AddProject()
        {
            return View(new ProjectViewModel());
        }

        [HttpPost("add-project")]
        public async Task<IActionResult> AddProject(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.User.Identity?.Name ?? "";
                var project = _mapper.Map<Project>(projectViewModel);

                var result = await _projectTaskService.AddProjectAsync(username, project);

                if (result)
                    return Json(new { Success = result, newProjectId = project.ProjectId });
                else
                    return BadRequest();
            }

            return View(projectViewModel);
        }
        [HttpGet("delete-project/{projectId}")]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            var projectIsNull = await _projectTaskService.ProjectIsNullAsync(projectId, username);

            if (projectIsNull)
                return BadRequest();

            ViewBag.projectId = projectId;
            return View(new ProjectViewModel());

        }

        [HttpDelete("delete-project/{projectId}/{isConfirmed}")]
        public async Task<IActionResult> DeleteProject(Guid projectId, bool isConfirmed)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            if (isConfirmed)
            {
                var result = await _projectTaskService.DeleteProjectAsync(username, projectId);

                if (result)
                    return Json(new { Success = result });
                else
                    return BadRequest();
            }

            ViewBag.projectId = projectId;
            return Json(new { ProjectId = projectId });
        }


        [HttpGet("update-project/{projectId}")]
        public async Task<IActionResult> UpdateProject(Guid projectId)
        {

            var username = HttpContext.User.Identity?.Name ?? "";

            var projectIsNull = await _projectTaskService.ProjectIsNullAsync(projectId, username);
            if (projectIsNull)
                return BadRequest();

            var projectToUpdate = await _projectTaskService
                .GetProjectById(username, projectId)
                .FirstOrDefaultAsync();

            var projectViewModel = _mapper.Map<ProjectViewModel>(projectToUpdate);

            ViewBag.projectId = projectId;
            
            return View(projectViewModel);
        }

        [HttpPut("update-project/{projectId}")]
        public async Task<IActionResult> UpdateProject(Guid projectId, ProjectViewModel updatedViewModelProject)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.User.Identity?.Name ?? "";

                var updatedProject = _mapper.Map<Project>(updatedViewModelProject);

                var result = await _projectTaskService.EditProjectAsync(username, projectId, updatedProject);

                return Json(new { Success = result });
            }

            ViewBag.projectId = projectId;

            return View(updatedViewModelProject);
        }


        [HttpGet("add-task/{projectId}")]
        public async Task<IActionResult> AddTask(Guid projectId)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            var projectIsNull = await _projectTaskService.ProjectIsNullAsync(projectId, username);

            if (projectIsNull)
                return BadRequest();

            ViewBag.projectId = projectId;
            return View(new TaskViewModel());

        }

        [ValidateAntiForgeryToken]
        [HttpPost("add-task/{projectId}")]
        public async Task<IActionResult> AddTask(Guid projectId, TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.User.Identity?.Name ?? "";
                var newTask = _mapper.Map<Task>(taskViewModel);
                bool result = await _projectTaskService.AddTaskAsync(username, projectId, newTask);

                return Json(new { Success = result });
            }

            ViewBag.projectId = projectId;
            return View(taskViewModel);
        }

        [HttpGet("update-task/{projectId}/{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid projectId, Guid taskId)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            var taskIsNull = await _projectTaskService.TaskIsNullAsync(projectId, taskId, username);

            if (taskIsNull)
                return BadRequest();

            var task = await _projectTaskService
                .GetTaskById(username, projectId, taskId)
                .FirstOrDefaultAsync();

            var taskViewModel = _mapper.Map<TaskViewModel>(task);

            ViewBag.projectId = projectId;
            ViewBag.taskId = taskId;

            return View(taskViewModel);
        }

        [HttpPut("update-task/{projectId}/{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid projectId, Guid taskId, TaskViewModel updatedTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.User.Identity?.Name ?? "";

                var updatedTask = _mapper.Map<Task>(updatedTaskViewModel);

                var result = await _projectTaskService.EditTaskAsync(username, projectId, taskId, updatedTask);

                return Json(new { Success = result });
            }

            ViewBag.projectId = projectId;
            ViewBag.taskId = taskId;
            return View(updatedTaskViewModel);
        }


        [HttpGet("delete-task/{projectId}/{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid projectId, Guid taskId)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            var taskIsNull = await _projectTaskService.TaskIsNullAsync(projectId, taskId, username);

            if (taskIsNull)
                return BadRequest();

            return View(new ProjectTaskViewModel() { ProjectId = projectId, TaskId = taskId });
        }

        [HttpDelete("delete-task")]
        public async Task<IActionResult> DeleteTask(ProjectTaskViewModel taskViewModel, [FromForm] bool isConfirmed)
        {
            if (ModelState.IsValid && isConfirmed)
            {
                var username = HttpContext.User.Identity?.Name ?? "";

                var result = await _projectTaskService
                    .DeleteTaskAsync(username, taskViewModel.ProjectId, taskViewModel.TaskId);

                return Json(new { Success = result });
            }

            return View(taskViewModel);
        }

        [HttpGet("sort-tasks/{projectId}")]
        public async Task<IActionResult> SortTasks(Guid projectId)
        {
            var username = HttpContext.User.Identity?.Name ?? "";

            if (await _projectTaskService.ProjectIsNullAsync(projectId, username))
                return BadRequest();

            return View(new TaskSortViewModel() { ProjectId = projectId });

        }

        [HttpPut("sort-tasks/{id}")]
        public async Task<IActionResult> SortTasks(TaskSortViewModel taskSortViewModel, [FromServices] ISortService<Task> taskService)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.User.Identity?.Name ?? "";

                var updatedDisplayState = _mapper.Map<ProjectDisplayState>(taskSortViewModel);

                var result = await _projectTaskService
                    .UpdateProjectDisplayStateAsync(username, taskSortViewModel.ProjectId, updatedDisplayState);

				return Json(new { Success = result, projectId = taskSortViewModel.ProjectId });
			}

			return View(taskSortViewModel);
        }
    }
}
