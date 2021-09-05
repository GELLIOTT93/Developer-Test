using System.ComponentModel.DataAnnotations;

namespace _288.TechTest.Api.Attributes
{
    public class NotEqualToAttribute : ValidationAttribute
    {
        private int Val { get; set; }

        public NotEqualToAttribute(int val)
        {
            Val = val;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // verify values
            if (value.Equals(Val))
                return new ValidationResult(string.Format("{0} should not be equal to {1}.", value, Val));
            else
                return ValidationResult.Success;
        }
    }
}
