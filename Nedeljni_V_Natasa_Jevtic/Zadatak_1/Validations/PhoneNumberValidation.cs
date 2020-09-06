using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Zadatak_1.Models;

namespace Zadatak_1.Validations
{
    class PhoneNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //validation for Serbian numbers
            string phoneNumber = value as string;
            if (Regex.Match(phoneNumber, @"^(\+3816[0-9]{7})$").Success || Regex.Match(phoneNumber, @"^(\+3816[0-9]{8})$").Success)
            {
                Users users = new Users();
                List<vwUser> userList = users.GetAllUsers();
                var user = userList.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault();
                //if exists user with forwarded phone number, return false
                if (user != null)
                {
                    return new ValidationResult(false, "This phone number already exists.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            else
            {
                return new ValidationResult(false, "Number must start with +3816, followed by 7 or 8 digits.");
            }
        }
    }
}
