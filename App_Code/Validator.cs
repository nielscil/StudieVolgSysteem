using System;
using System.Text.RegularExpressions;
using System.Web; 
using System.Web.WebPages;
/// <summary>
/// Validators
/// </summary>
public class PasswordValidator : RequestFieldValidatorBase
{
    string name;
    string surname;
    string email;

    /// <summary>
    /// Maakt wachtwoordvalidator aan
    /// </summary>
    /// <param name="name">naam van gebruiker</param>
    /// <param name="surname">achternaam van gebruiker</param>
    /// <param name="email">email van gebruiker</param>
    /// <param name="errorMessage">foutmelding</param>
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

    /// <summary>
    /// Regex validator
    /// </summary>
    /// <param name="regex">waarop gecontroleerd moet worden</param>
    /// <param name="errorMessage">foutmelding</param>
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
    /// <summary>
    /// Validator voor wachtwoord
    /// </summary>
    /// <param name="name">naam van gebruiker</param>
    /// <param name="surname">achternaam van gebruiker</param>
    /// <param name="email">email van gebruiker</param>
    /// <param name="errorMessage">foutmelding</param>
    /// <returns>Passwordvalidator</returns>
    public static IValidator Password(string name,string surname,string email,string errorMessage = null){
        if(string.IsNullOrEmpty(errorMessage)){
            errorMessage = "Er ging iets fout";
        }
        return new PasswordValidator(name,surname,email,errorMessage);
    }

    /// <summary>
    /// Validator voor regex
    /// </summary>
    /// <param name="regex">waarop gecontroleerd moet worden</param>
    /// <param name="errorMessage">foutmelding</param>
    /// <returns>Regexvalidator</returns>
    public static IValidator Regex(string regex, string errorMessage = null)
    {
        if (string.IsNullOrEmpty(errorMessage))
        {
            errorMessage = "Komt niet overeen";
        }
        return new Regexval(regex, errorMessage);
    }
}