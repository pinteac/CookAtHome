using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace cootathome.Utlity
{

    public class ValidationUtilities
    {
        // Validate spelling
        public static Regex regex = new Regex(
        "^(\\b[A-Za-z]*\\b)$",
            RegexOptions.IgnoreCase
    |       RegexOptions.Compiled
            );


        public static Regex regexPassword = new Regex(
                    @"^[A-Za-z0-9!@#$%^&*?_~-£,.]$",
            RegexOptions.IgnoreCase
    |       RegexOptions.Compiled
            );



        public static string ValidateInput(string input)
        {
            if (input.Length < 2 || !regex.IsMatch(input))
            {
                return "The name: " + input + " is invalid!";
            }
            return null;
        }

        public static string ValidatePassword(string password)
        {
            if (password.Length < 8 || regexPassword.IsMatch(password))
            {
                return "Your password is too weak! Please enter a stronger password!";
            }
            return null;
        }
    }
}
