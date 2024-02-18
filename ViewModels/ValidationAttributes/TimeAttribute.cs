using System.ComponentModel.DataAnnotations;

namespace TaskSched.ViewModels.ValidationAttributes
{
	public class TimeAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is DateTime time)
			{
				if (validationContext.ObjectInstance is TaskViewModel model)
				{
					if (model.DateEnd == null)
						return new ValidationResult("Оберіть дату завершення.");
					if (model.DateEnd.Value.Date == DateTime.Now.Date && time.TimeOfDay < DateTime.Now.TimeOfDay)
						return new ValidationResult("Оберіть час який буде більшим за поточний.");
				}
			}
			return ValidationResult.Success;
		}
	}
}
