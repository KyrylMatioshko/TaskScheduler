using TaskSched.Data.Context;
using TaskSched.Data.Models;

namespace TaskSched.Services.Interfaces
{
	public interface IUserComplaintsCrudService
	{
		public IQueryable<User> GetUserByUserName(string userName);
		public Task<bool> AddComplaint(UserComplaints userComplaint, string userName);
	}
}
