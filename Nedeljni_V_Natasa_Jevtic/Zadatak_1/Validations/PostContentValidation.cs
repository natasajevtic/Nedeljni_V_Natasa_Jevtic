using System;
using System.Globalization;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class PostContentValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string content = value as string;
            if (String.IsNullOrEmpty(content))
            {
                return new ValidationResult(false, "Please fill a field.");
            }
            else if (content.Length > 100)
            {
                return new ValidationResult(false, "Maximum number of characters is 100.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}