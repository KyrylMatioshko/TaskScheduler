using System.ComponentModel.DataAnnotations;

namespace TaskSched.ViewModels.ValidationAttributes
{
	public class DateAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if (value is DateTime date)
			{
				return date.Date >= DateTime.Now.Date;
			}

			return false;
		}
	}
}
