using Microsoft.EntityFrameworkCore;
using TaskSched.Data.Context;
using TaskSched.Data.Models;
using TaskSched.Services.Interfaces;

namespace TaskSched.Services.CompanyDetailsInfoService
{
	public class CompanyDetailsInfoService : ICompanyDetailsInfoService
	{
		private readonly TaskSchedulerContext _context;

		public CompanyDetailsInfoService(TaskSchedulerContext context)
		{
			_context = context;
		}

		public IQueryable<CompanyDetails> GetInfo(string companyName)
		{
			return _context.CompanyDetails.Where(cd => cd.Name == companyName);
		}
	}
}
