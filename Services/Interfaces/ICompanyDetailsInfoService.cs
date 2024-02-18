using TaskSched.Data.Models;

namespace TaskSched.Services.Interfaces
{
	public interface ICompanyDetailsInfoService
	{
		public IQueryable<CompanyDetails> GetInfo(string companyName);
	}
}
