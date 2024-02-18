using Microsoft.EntityFrameworkCore;
using TaskSched.Data.Context;
using TaskSched.Data.Models;
using TaskSched.Services.Interfaces;
using TaskSched.ViewModels;

namespace TaskSched.Services.ComplaintUserCrudService
{
	public class UserComplaintsCrudService : IUserComplaintsCrudService
	{
		private readonly TaskSchedulerContext _context;
		public UserComplaintsCrudService(TaskSchedulerContext schedulerContext)
		{
			_context = schedulerContext;
		}

		public IQueryable<User> GetUserByUserName(string userName)
		{
			var user = _context.Users
			.Where(u => u.UserName == userName);

			return user;
		}

		public async Task<bool> AddComplaint(UserComplaints userComplaint, string userName)
		{
			var user = await GetUserByUserName(userName)
				.FirstOrDefaultAsync();

			if (user == null) return false;

			var lastComplaint = await _context.UserComplaints
				.Where(x => x.UserId == user.Id)
				.OrderByDescending(x => x.CreatedAt)
				.FirstOrDefaultAsync();

			if (lastComplaint == null || lastComplaint.CreatedAt <= DateTime.Now.AddHours(-1))
			{
				userComplaint.UserId = user.Id;

				_context.UserComplaints.Add(userComplaint);

				int result = _context.SaveChanges();

				return result > 0;
			}

			return false;
		}
	}
}