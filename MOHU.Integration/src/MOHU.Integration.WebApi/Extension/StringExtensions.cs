﻿namespace MOHU.Integration.WebApi.Extension;

public static class StringExtensions
{
    public static string ConvertPhoneNumberToInternationalFormat(this string input)
    {
        if (input.StartsWith("00"))
        {
            return "+" + input[2..];
        }
        
        return input;
    } 
}