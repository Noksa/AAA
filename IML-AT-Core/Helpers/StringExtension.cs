﻿using System;
using System.Linq;

namespace IML_AT_Core.Helpers
{
    public static class StringExtension
    {
        public static string FirstCharToUpperAndOtherToLower(this string input)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            }
        }
    }
}
