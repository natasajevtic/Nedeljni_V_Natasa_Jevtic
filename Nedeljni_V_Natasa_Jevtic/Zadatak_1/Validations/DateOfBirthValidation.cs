using System;
using System.Globalization;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class DateOfBirthValidation : ValidationRule
    {
        /// <summary>
        /// This method checks if user input for birth date valid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>ValidationResult.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string dateOfBirth = value as string;
            try
            {
                DateTime conversion = DateTime.ParseExact(dateOfBirth, "M/d/yyyy", CultureInfo.InvariantCulture);
                if (conversion > DateTime.Now)
                {
                    return new ValidationResult(false, "Cannot be in the future.");
                }
                else if (DateTime.Now.Subtract(conversion).TotalDays > 36525)
                {
                    return new ValidationResult(false, "Cannot be in the past.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            //if cannot convert to DateTime, because  doesn't contain a valid date
            catch (Exception)
            {
                return new ValidationResult(false, "Invalid date. Format: M/d/yyyy");
            }
        }
    }
}

