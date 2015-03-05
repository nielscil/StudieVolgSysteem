using System;
using System.Text.RegularExpressions;
using System.Web; 
using System.Web.WebPages;
/// <summary>
/// Summary description for Validator
/// </summary>
public class PasswordValidator : RequestFieldValidatorBase
{
    string name;
    string surname;
    string email;
    public PasswordValidator(string name,string surname,string email,string errorMessage = null): base (errorMessage)
    {
        this.name = name;
        this.surname = surname;
        this.email = email;
    }
    /// <summary>
    /// Checkt of wachtwoord geen namen bevat.
    /// </summary>
    /// <param name="httpContext">http context</param>
    /// <param name="value">wachtwoord</param>
    /// <returns>geeft true terug wanneer het wachwoord geen namen bevat, anders false </returns>
    protected override bool IsValid(HttpContextBase httpContext, string value)
    {
        bool containsname = value.Contains(name);
        bool containssurname = false;
        string[]surnamesplit = surname.Split(' ');
        foreach(string s in surnamesplit)
        {
            if(s.Length > 3)
            {
                if(value.Contains(s))
                {
                    containssurname = true;
                }
            }
        }
        string emailname = email.Split('@')[0];
        bool containsemail = value.Contains(emailname);
        return !containsemail && !containsname && !containssurname;
    }

}
public class Regexval : RequestFieldValidatorBase
{
    string regex;
    public Regexval(string regex,string errorMessage = null): base(errorMessage)
    {
        this.regex = regex;
    }
    /// <summary>
    /// checkt of de regex matcht met de inkomende value
    /// </summary>
    /// <param name="httpContext">http context</param>
    /// <param name="value">inkomende waarde om te vergelijken</param>
    /// <returns>true als t matcht, anders false</returns>
    protected override bool IsValid(HttpContextBase httpContext, string value)
    {
        return Regex.IsMatch(value, regex);
    }

}

public class MyValidator
{
    public static IValidator Password(string name,string surname,string email,string errorMessage = null){
        if(string.IsNullOrEmpty(errorMessage)){
            errorMessage = "Er ging iets fout";
        }
        return new PasswordValidator(name,surname,email,errorMessage);
    }

    public static IValidator Regex(string regex, string errorMessage = null)
    {
        if (string.IsNullOrEmpty(errorMessage))
        {
            errorMessage = "Komt niet overeen";
        }
        return new Regexval(regex, errorMessage);
    }
}