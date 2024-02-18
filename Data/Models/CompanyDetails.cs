namespace TaskSched.Data.Models
{
	public class CompanyDetails
	{
		public CompanyDetails()
		{
			CompanyDetailsId = Guid.NewGuid();
		}

		public Guid CompanyDetailsId { get; set; }

		public string Name { get; set; } = null!;

		public string? Phone { get; set; }

		public string? Email { get; set; }

		public string? Country { get; set; }

		public string? City { get; set; }

		public string? Street { get; set; }
	}
}
