using System.ComponentModel.DataAnnotations;

namespace Cactus.Infrastructure
{
    public class DateTimeRangeAttribute:ValidationAttribute
    {
        public DateTimeRangeAttribute(int minAge, int maxAge) {
            this.minAge = minAge;
            this.maxAge = maxAge;
        }

        public int minAge { get; }
        public int maxAge { get; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            DateTime time = Convert.ToDateTime(value);
            if (time < DateTime.Now && time >= DateTime.Now.AddYears(-minAge) && time <= DateTime.Now.AddYears(-maxAge)) {
                return ValidationResult.Success;
            }
            else return new ValidationResult(ErrorMessage = "Неверная дата рождения");
        }
    }
}
